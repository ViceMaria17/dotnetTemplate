using System;
using System.Security.Claims;
using AppProject.Core.Contracts;
using AppProject.Core.Infraestructure.Database;
using AppProject.Core.Infraestructure.Database.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace AppProject.Core.API.Auth;

public class UserContext (
    IHttpContextAccessor httpContextAccessor,
    ApplicationDbContext applicationDbContext,
    HybridCache hybridCache)
    : IUserContext
{
    private UserInfo? currentUser;

    public async Task<UserInfo> GetSystemAdminUserAsync(CancellationToken cancellationToken = default)
    {
        var cachedUser = await hybridCache.GetOrCreateAsync(
            CacheKeys.SytemAdminUserKey,
            async token => await GetSystemAdminUserFromDatabaseAsync(token),
            cancellationToken: cancellationToken);

        return cachedUser;

        async Task<UserInfo> GetSystemAdminUserFromDatabaseAsync(CancellationToken cancellationToken = default)
        {
            var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.IsSystemAdmin, cancellationToken);

            if (user is null)
            {
                throw new InvalidOperationException("System admin user not found");
            }

            return new UserInfo
            {
                UserID = user.Id,
                UserName = user.Name,
                Email = user.Email,
                IsSystemAdmin = true
            };
        }
    }

    public async Task<UserInfo> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        if (this.currentUser != null)
        {
            return this.currentUser;
        }

        var systemAdminUser = await this.GetSystemAdminUserAsync(cancellationToken);

        var claimsPrincipal = httpContextAccessor.HttpContext?.User;
        if (claimsPrincipal?.Identity?.IsAuthenticated == true)
        {
            var name = claimsPrincipal.FindFirstValue("name");
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? claimsPrincipal.FindFirstValue("email");

            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException();
            }

            if (email == systemAdminUser.Email)
            {
                this.currentUser = systemAdminUser;
                return this.currentUser;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = email;
            }

            var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (user == null)
            {
                user = new TbUser
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    IsSystemAdmin = false,
                    CreatedAt = DateTime.UtcNow,
                    CreatedByUserId = systemAdminUser.UserID,
                    CreatedByUserName = systemAdminUser.UserName
                };

                applicationDbContext.Users.Add(user);
                await applicationDbContext.SaveChangesAsync();
            }
            else if (user.Name != name)
            {
                user.Name = name;

                applicationDbContext.Users.Update(user);

                await applicationDbContext.SaveChangesAsync();
            }

            this.currentUser = new UserInfo
            {
                UserID = user.Id,
                UserName = user.Name,
                Email = user.Email,
                IsSystemAdmin = user.IsSystemAdmin
            };

            return this.currentUser;
        }
        else
        {
            // not authenticated, return system admin user
            this.currentUser = systemAdminUser;

            return this.currentUser;
        }
    }
}

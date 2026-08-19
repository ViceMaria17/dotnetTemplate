using System;
using AppProject.Core.Infraestructure.Database.Entities.Auth;
using AppProject.Core.Infraestructure.Database.Entities.General;
using Microsoft.EntityFrameworkCore;

namespace AppProject.Core.Infraestructure.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for your entities
    public DbSet<TbUser> Users { get; set; } = default!;

    public DbSet<TbCountry> Contries { get; set; } = default!;

    public DbSet<TbState> States { get; set; } = default!;

    public DbSet<TbCity> Cities { get; set; } = default!;

    public DbSet<TbNeighborhood> Neighborhoods { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

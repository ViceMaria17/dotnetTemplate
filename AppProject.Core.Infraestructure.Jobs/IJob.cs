using System;

namespace AppProject.Core.Infraestructure.Jobs;

public interface IJob
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}

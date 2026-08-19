using System;

namespace AppProject.Core.Infraestructure.AI;

public interface IChatClient
{
    Task<string> SendMessageAsync(
        string systemMessage,
        IEnumerable<string> userMessages,
        string model,
        CancellationToken cancellationToken = default);
}

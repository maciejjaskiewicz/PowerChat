using System;

namespace PowerChat.Common
{
    public interface IDateTime : IInfrastructureService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}

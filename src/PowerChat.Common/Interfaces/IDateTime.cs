using System;

namespace PowerChat.Common.Interfaces
{
    public interface IDateTime : IInfrastructureService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}

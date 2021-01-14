using System;
using PowerChat.Common.Interfaces;

namespace PowerChat.Services.Common.Infrastructure.Services
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

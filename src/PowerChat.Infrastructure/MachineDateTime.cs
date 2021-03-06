﻿using System;
using PowerChat.Common;
using PowerChat.Common.Interfaces;

namespace PowerChat.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

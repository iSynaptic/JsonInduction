using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    [Flags]
    public enum PrimativeTypes
    {
        String = 1 << 0,
        Integer = 1 << 1,
        Float = 1 << 2,
        Date = 1 << 3,
        Boolean = 1 << 4,
        Guid = 1 << 5,
        TimeSpan = 1 << 6,
        Uri = 1 << 7
    }
}

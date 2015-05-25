using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    [Flags]
    public enum VertexTypes : byte
    {
        Value = 1 << 0,
        Object = 1 << 1,
        Array = 1 << 2
    }
}

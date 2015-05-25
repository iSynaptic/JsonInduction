using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedArraySchema : InducedVertexSchema
    {
        public InducedEdgeSchema ItemSchema { get; set; }
    }
}

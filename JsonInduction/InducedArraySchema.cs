using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedArraySchema : InducedVertexSchema
    {
        public InducedEdgeSchema Item { get; set; }

        public int MinimumItems { get; set; }
        public float AverageItems { get; set; }

        public override string ToString()
            => $"min: {MinimumItems} avg: {AverageItems}";
    }
}

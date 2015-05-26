using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedArraySchema : InducedVertexSchema
    {
        public void AmendStats(int count)
        {
            MinimumItems = ArrayCount != 0
                ? Math.Min(MinimumItems, count)
                : count;

            MaximumItems = Math.Max(MaximumItems, count);

            float total = AverageItems * ArrayCount;
            total += count;

            ArrayCount++;
            AverageItems = total / ArrayCount;
        }

        public InducedEdgeSchema Item { get; set; }

        private int ArrayCount { get; set; }

        public int MinimumItems { get; set; }
        public int MaximumItems { get; set; }

        public float AverageItems { get; set; }

        public override string ToString()
            => $"min: {MinimumItems} max: {MaximumItems} avg: {AverageItems}";
    }
}

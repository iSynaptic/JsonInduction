using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedEdgeSchema : InducedComponentSchema
    {
        public InducedValueSchema Value { get; set; }
        public InducedObjectSchema Object { get; set; }
        public InducedArraySchema Array { get; set; }

        public InducedPropertySchema this[string name] => Object?[name];

        public VertexTypes AllowedTypes =>
            (Value != null ? VertexTypes.Value : 0) |
            (Object != null ? VertexTypes.Object : 0) |
            (Array != null ? VertexTypes.Array : 0);

        public override string ToString()
        {
            var types = 
                Enum.GetValues(typeof(VertexTypes))
                    .Cast<VertexTypes>()
                    .Where(x => (AllowedTypes & x) == x)
                    .Select(x => Enum.GetName(typeof(VertexTypes), x))
                    .ToArray();

            if (types.Length == 1)
                return (Value ?? Object ?? (InducedVertexSchema)Array)?.ToString() ?? "";

            return String.Join("|", types);
        } 
    }
}

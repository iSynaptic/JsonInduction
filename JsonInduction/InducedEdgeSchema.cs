using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedEdgeSchema : InducedComponentSchema
    {
        public InducedValueSchema ValueSchema { get; set; }
        public InducedObjectSchema ObjectSchema { get; set; }
        public InducedArraySchema ArraySchema { get; set; }

        public InducedPropertySchema this[string name] => ObjectSchema?[name];

        public VertexTypes AllowedVertexTypes =>
            (ValueSchema != null ? VertexTypes.Value : 0) |
            (ObjectSchema != null ? VertexTypes.Object : 0) |
            (ArraySchema != null ? VertexTypes.Array : 0);
    }
}

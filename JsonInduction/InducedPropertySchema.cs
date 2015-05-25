using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedPropertySchema : InducedEdgeSchema
    {
        public string Name { get; }
        public bool IsRequired { get; set; }

        public InducedPropertySchema(string name)
        {
            Name = name;
        }
    }
}

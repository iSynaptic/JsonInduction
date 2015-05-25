using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JsonInduction
{
    public class SchemaBuildingVisitor : SchemaSpiningVisitor
    {
        public SchemaBuildingVisitor(InducedSchema schema) : base(schema)
        {

        }

        protected InducedValueSchema Value => Vertex as InducedValueSchema;

        protected override JToken VisitValue(JValue val)
        {
            if (val.Type == JTokenType.Null)
                Value.CanBeNull = true;

            else if (val.Type == JTokenType.String)
            {
                //TODO: check for string represented types (eg Guid, TimeSpan, etc)
                Value.AllowedTypes |= ValueTypes.String;
            }

            else if (val.Type == JTokenType.Integer)
                Value.AllowedTypes |= ValueTypes.Integer;

            return base.VisitValue(val);
        }
    }
}

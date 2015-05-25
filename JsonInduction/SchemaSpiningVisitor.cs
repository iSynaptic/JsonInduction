using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JsonInduction
{
    public class SchemaSpiningVisitor : JsonVisitor
    {
        public SchemaSpiningVisitor(InducedSchema schema)
        {
            Schema = schema;
        }

        protected override JToken VisitProperty(JProperty property)
        {
            var obj = (InducedObjectSchema)Vertex;

            var prop = obj.GetProperty(property.Name);
            if(prop == null)
            {
                prop = new InducedPropertySchema(property.Name);
                obj.AddProperty(prop);
            }

            Schema = prop;
            var result = base.VisitProperty(property);
            Schema = obj;

            return result;
        }

        protected override JToken VisitObject(JObject obj)
        {
            var edge = Edge;

            if (edge.ObjectSchema == null)
                edge.ObjectSchema = new InducedObjectSchema();

            Schema = edge.ObjectSchema;
            var result = base.VisitObject(obj);
            Schema = edge;

            return result;
        }

        protected override JToken VisitArray(JArray array)
        {
            var edge = Edge;

            if (edge.ArraySchema == null)
                edge.ArraySchema = new InducedArraySchema();

            Schema = edge.ArraySchema;
            var result = base.VisitArray(array);
            Schema = edge;

            return result;
        }

        protected override JToken VisitValue(JValue val)
        {
            var edge = Edge;

            if (edge.ValueSchema == null)
                edge.ValueSchema = new InducedValueSchema();

            Schema = edge.ValueSchema;
            var result = base.VisitValue(val);
            Schema = edge;

            return result;
        }

        protected InducedComponentSchema Schema { get; private set; }
        protected InducedEdgeSchema Edge => Schema as InducedEdgeSchema;
        protected InducedVertexSchema Vertex => Schema as InducedVertexSchema;
    }
}

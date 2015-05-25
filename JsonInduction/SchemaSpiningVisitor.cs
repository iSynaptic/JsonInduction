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
            Current.Push(schema);
        }

        protected override void BeforeVisitProperty(JProperty property)
        {
            base.BeforeVisitProperty(property);

            var obj = (InducedObjectSchema)Vertex;

            var prop = obj.GetProperty(property.Name);
            if(prop == null)
            {
                prop = new InducedPropertySchema(property.Name);
                obj.AddProperty(prop);
            }

            Current.Push(prop);
        }

        protected override JToken AfterVisitProperty(JProperty property, JToken result)
        {
            Current.Pop();
            return base.AfterVisitProperty(property, result);
        }

        protected override void BeforeVisitObject(JObject obj)
        {
            base.BeforeVisitObject(obj);

            var edge = Edge;

            if (edge.Object == null)
                edge.Object = new InducedObjectSchema();

            Current.Push(edge.Object);
        }

        protected override JToken AfterVisitObject(JObject obj, JToken result)
        {
            Current.Pop();
            return base.AfterVisitObject(obj, result);
        }

        protected override void BeforeVisitArray(JArray array)
        {
            base.BeforeVisitArray(array);

            var edge = Edge;

            if (edge.Array == null)
                edge.Array = new InducedArraySchema();

            Current.Push(edge.Array);
        }

        protected override JToken AfterVisitArray(JArray array, JToken result)
        {
            Current.Pop();
            return base.AfterVisitArray(array, result);
        }

        protected override void BeforeVisitValue(JValue val)
        {
            base.BeforeVisitValue(val);

            var edge = Edge;

            if (edge.Value == null)
                edge.Value = new InducedValueSchema();

            Current.Push(edge.Value);
        }

        protected override JToken AfterVisitValue(JValue value, JToken result)
        {
            Current.Pop();
            return base.AfterVisitValue(value, result);
        }

        protected InducedSchema Schema { get; private set; }

        private Stack<InducedComponentSchema> Current { get; } = new Stack<InducedComponentSchema>();

        protected InducedEdgeSchema Edge => Current.Peek() as InducedEdgeSchema;
        protected InducedVertexSchema Vertex => Current.Peek() as InducedVertexSchema;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace JsonInduction
{
    public class SchemaBuildingVisitor : SchemaSpiningVisitor
    {
        public SchemaBuildingVisitor(InducedSchema schema) : base(schema)
        {
        }

        protected InducedValueSchema Value => Vertex as InducedValueSchema;
        protected InducedObjectSchema Object => Vertex as InducedObjectSchema;
        protected InducedArraySchema Array => Vertex as InducedArraySchema;

        protected override JToken VisitObject(JObject obj)
        {
            if(!obj.Properties().Any())
                Object.CanBeEmpty = true;

            bool isNewObject = !Object.CanBeEmpty && !Object.Properties.Any();

            var optional = new HashSet<string>();

            optional.UnionWith(Object.Properties.Select(x => x.Name));
            optional.SymmetricExceptWith(obj.Properties().Select(x => x.Name));

            var result = base.VisitObject(obj);

            if (!isNewObject)
            {
                foreach (var prop in Object.Properties)
                {
                    if (optional.Contains(prop.Name))
                        prop.IsRequired = false;
                }
            }

            return result;
        }

        protected override JToken VisitArray(JArray array)
        {
            Array.AmendStats(array.Count);
            return base.VisitArray(array);
        }

        protected override JToken VisitValue(JValue val)
        {
            if (val.Type == JTokenType.Null)
                Value.AddNull();

            else if (val.Type == JTokenType.String)
            {
                var value = (string)val.Value;

                decimal tmp;
                if(!decimal.TryParse(value, out tmp))
                {
                    DateTime dateTime;
                    Guid guid;
                    TimeSpan timeSpan;
                    Uri uri;

                    if (TimeSpan.TryParse(value, out timeSpan))
                        Value.AddTimeSpan(timeSpan);
                    else if (DateTime.TryParse(value, out dateTime))
                        Value.AddDate(dateTime);
                    else if (Guid.TryParse(value, out guid))
                        Value.AddGuid(guid);
                    else if (Uri.TryCreate(value, UriKind.Absolute, out uri))
                        Value.AddUri(uri);
                    else
                        Value.AddString(value);
                }
                else
                    Value.AddString(value);
            }

            else if (val.Type == JTokenType.Integer)
                Value.AddInteger((long)val.Value);

            else if (val.Type == JTokenType.Float)
                Value.AddFloat((double)val.Value);

            else if (val.Type == JTokenType.Boolean)
                Value.AddBool((bool)val.Value);

            else if (val.Type == JTokenType.Date)
                Value.AddDate((DateTime)val.Value);

            else if (val.Type == JTokenType.Guid)
                Value.AddGuid((Guid)val.Value);

            else if (val.Type == JTokenType.TimeSpan)
                Value.AddTimeSpan((TimeSpan)val.Value);

            else if (val.Type == JTokenType.Uri)
                Value.AddUri((Uri)val.Value);

            return base.VisitValue(val);
        }
    }
}

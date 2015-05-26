using System;
using Newtonsoft.Json.Linq;

namespace JsonInduction
{
    public abstract class JsonVisitor
    {
        public virtual JToken Visit(JToken token)
        {
            var clone = token.DeepClone();
            return Dispatch(clone);
        }

        protected virtual JToken Dispatch(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return DispatchObject((JObject)token);
                case JTokenType.Property:
                    return DispatchProperty((JProperty)token);
                case JTokenType.Array:
                    return DispatchArray((JArray)token);
                case JTokenType.String:
                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.Date:
                case JTokenType.Boolean:
                case JTokenType.Null:
                    return DispatchValue((JValue)token);
                default:
                    throw new InvalidOperationException();
            }
        }

        protected virtual JToken DispatchObject(JObject obj)
        {
            BeforeVisitObject(obj);
            return AfterVisitObject(obj, VisitObject(obj));
        }

        protected virtual void BeforeVisitObject(JObject obj) { }
        protected virtual JToken AfterVisitObject(JObject obj, JToken result) => result;

        protected virtual JToken VisitObject(JObject obj)
        {
            foreach (var property in obj.Properties())
                Dispatch(property);

            return obj;
        }

        protected virtual JToken DispatchProperty(JProperty property)
        {
            BeforeVisitProperty(property);
            return AfterVisitProperty(property, VisitProperty(property));
        }

        protected virtual void BeforeVisitProperty(JProperty property) { }
        protected virtual JToken AfterVisitProperty(JProperty property, JToken result) => result;

        protected virtual JToken VisitProperty(JProperty property)
        {
            Dispatch(property.Value);

            return property;
        }

        protected virtual JToken DispatchArray(JArray array)
        {
            BeforeVisitArray(array);
            return AfterVisitArray(array, VisitArray(array));
        }

        protected virtual void BeforeVisitArray(JArray array) { }
        protected virtual JToken AfterVisitArray(JArray array, JToken result) => result;

        protected virtual JToken VisitArray(JArray array)
        {
            DispatchArrayItems(array);
            return array;
        }

        protected virtual void DispatchArrayItems(JArray array)
        {
            BeforeVisitArrayItems(array);
            VisitArrayItems(array);
            AfterVisitArrayItems(array);
        }

        protected virtual void BeforeVisitArrayItems(JArray array) { }
        protected virtual void AfterVisitArrayItems(JArray array) { }

        protected virtual void VisitArrayItems(JArray array)
        {
            foreach (var item in array)
                Dispatch(item);
        }

        protected virtual JToken DispatchValue(JValue value)
        {
            BeforeVisitValue(value);
            return AfterVisitValue(value, VisitValue(value));
        }

        protected virtual void BeforeVisitValue(JValue value) { }
        protected virtual JToken AfterVisitValue(JValue value, JToken result) => result;

        protected virtual JToken VisitValue(JValue value) => value;
    }
}

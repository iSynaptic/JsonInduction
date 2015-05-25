using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedObjectSchema : InducedVertexSchema
    {
        private readonly Dictionary<string, InducedPropertySchema> _properties
            = new Dictionary<string, InducedPropertySchema>();

        public InducedPropertySchema GetProperty(string name)
        {
            InducedPropertySchema result;
            _properties.TryGetValue(name, out result);

            return result;
        }

        public void AddProperty(InducedPropertySchema property)
        {
            if (_properties.ContainsKey(property.Name))
                throw new ArgumentException("Property already exists.", nameof(property));

            _properties.Add(property.Name, property);
        }

        public InducedPropertySchema this[string name] => GetProperty(name);

        public IEnumerable<InducedPropertySchema> Properties => _properties.Values;

        public override string ToString()
            => $"required: {Properties.Count(x => x.IsRequired)} optional: {Properties.Count(x => !x.IsRequired)}";
    }
}

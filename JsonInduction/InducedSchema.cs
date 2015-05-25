using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedSchema : InducedEdgeSchema
    {
        public static InducedSchema Build(string name, params JToken[] exampleRoots)
            => Build(name, (IEnumerable<JToken>)exampleRoots);

        public static InducedSchema Build(string name, IEnumerable<JToken> exampleRoots)
        {
            var schema = new InducedSchema(name);

            return schema;
        }

        public InducedSchema(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}

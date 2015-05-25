using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonInduction
{
    [TestFixture]
    public class InducedSchemaConstructionTests
    {
        [Test]
        public void SimpleObjectSchema()
        {
            var json = JToken.Parse(@"
            {
                name:
                {
                    first: 'John',
                    last: 'Smith'
                },
                address:
                {
                    street: '123 Main Street',
                    city: 'Minneapolis',
                    state: 'MN',
                    postal: '55434'
                }
            }");

            var schema = InducedSchema.Build("test", json);

            schema.Should().NotBeNull();
        }
    }
}

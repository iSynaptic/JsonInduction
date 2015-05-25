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
        private static readonly JObject BasicObject =
            JObject.Parse(@"
            {
                name:
                {
                    firstName: 'John',
                    lastName: 'Smith'
                }
            }");

        private static readonly JObject AmendingObject =
            JObject.Parse(@"
            {
                name:
                {
                    salutation: 'Mr'
                },
                role: 'contact',
                address:
                {
                    street: '123 Main Street',
                    city: 'Minneapolis'
                }
            }");

        private static readonly JObject CompleteObject =
            JObject.Parse(@"
            {
                name:
                {
                    firstName: 'John',
                    lastName: 'Smith',
                    salutation: 'Mr'
                },
                role: 'contact',
                address:
                {
                    street: '123 Main Street',
                    city: 'Minneapolis'
                }
            }");

        [Test]
        public void AmendedSchema()
        {
            var schema = InducedSchema.Build("test", BasicObject, AmendingObject);

            schema.Should().NotBeNull();
        }

        [Test]
        public void SimpleObjectSchema()
        {
            var schema = InducedSchema.Build("test", CompleteObject);

            schema.Should().NotBeNull();
        }
    }
}

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
                id: 'CC2E5F37-86EE-419B-BB35-3F075CDCED9A',
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
                    city: 'Minneapolis',
                    state: 'MN',
                    postal: '55116',
                    plusFive: null
                },
                stats:
                {
                    outgoingCalls: 42,
                    averageCallLength: '00:04:32',
                    lastCallAt: '2012-04-23T18:25:43.511Z'
                },
                website: 'http://john.smith.com'
            }");

        [Test]
        public void AmendedSchema()
        {
            var schema = InducedSchema.Build("test", BasicObject, AmendingObject);
            
            schema.Should().NotBeNull();
            (schema["name"]["firstName"].Value.AllowedTypes & PrimativeTypes.String)
                .Should().Be(PrimativeTypes.String);
        }

        [Test]
        public void SimpleObjectSchema()
        {
            var schema = InducedSchema.Build("test", CompleteObject);

            schema.Should().NotBeNull();
        }
    }
}

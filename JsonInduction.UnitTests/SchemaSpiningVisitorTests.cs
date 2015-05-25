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
    public class SchemaSpiningVisitorTests
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

        [Test]
        public void CreatesSpine()
        {
            var schema = new InducedSchema("test");

            var visitor = new SchemaSpiningVisitor(schema);
            visitor.Visit(BasicObject);

            AssertBasicObjectSpine(schema);
        }

        [Test]
        public void UpdateSpine()
        {
            var schema = new InducedSchema("test");

            var visitor = new SchemaSpiningVisitor(schema);
            visitor.Visit(BasicObject);

            visitor.Visit(AmendingObject);

            AssertBasicObjectSpine(schema);

            var salutationSchema = schema["name"]["salutation"];
            salutationSchema.ValueSchema.Should().NotBeNull();

            schema["role"].ValueSchema.Should().NotBeNull();

            var addressSchema = schema["address"];
            addressSchema["street"].ValueSchema.Should().NotBeNull();
            addressSchema["city"].ValueSchema.Should().NotBeNull();
        }

        private static void AssertBasicObjectSpine(InducedSchema schema)
        {
            schema.AllowedVertexTypes.Should().Be(VertexTypes.Object);
            schema.ObjectSchema.Should().NotBeNull();

            var nameSchema = schema["name"];
            nameSchema.AllowedVertexTypes.Should().Be(VertexTypes.Object);

            var fnSchema = nameSchema["firstName"];
            fnSchema.AllowedVertexTypes.Should().Be(VertexTypes.Value);

            var lnSchema = nameSchema["lastName"];
            lnSchema.AllowedVertexTypes.Should().Be(VertexTypes.Value);
        }
    }
}

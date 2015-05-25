using FluentAssertions;
using Newtonsoft.Json.Linq;

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace JsonInduction
{
    [TestFixture]
    public class PathAwareJsonVisitorTests
    {
        private class Visitor : PathAwareJsonVisitor
        {
            private readonly List<string> _paths;
            private readonly List<string[]> _history;

            public Visitor(List<string> paths, List<string[]> history)
            {
                _paths = paths;
                _history = history;
            }

            private void Capture()
            {
                _paths.Add(Path);
                _history.Add(History.ToArray());
            }

            protected override JToken VisitArray(JArray array)
            {
                Capture();
                return base.VisitArray(array);
            }

            protected override JToken VisitObject(JObject obj)
            {
                Capture();
                return base.VisitObject(obj);
            }

            protected override JToken VisitValue(JValue value)
            {
                Capture();
                return base.VisitValue(value);
            }
        }

        [Test]
        public void PathIsCorrect()
        {
            var json = JObject.Parse(@"
            {
                name:
                {
                    firstName: 'John',
                    lastName: 'Smith'
                }
            }");

            var paths = new List<string>();
            var history = new List<string[]>();

            var visitor = new Visitor(paths, history);
            visitor.Visit(json);

            paths.ShouldBeEquivalentTo(new[]
            {
                "",
                "name",
                "name.firstName",
                "name.lastName"
            });
        }

        [Test]
        public void HistoryIsCorrect()
        {
            var json = JObject.Parse(@"
            {
                name:
                {
                    firstName: 'John',
                    lastName: 'Smith'
                }
            }");

            var paths = new List<string>();
            var history = new List<string[]>();

            var visitor = new Visitor(paths, history);
            visitor.Visit(json);

            var expected = new[]
            {
                new string[0],
                new [] {"name"},
                new [] {"name", "firstName"},
                new [] {"name", "lastName"},
            };

            history.ShouldBeEquivalentTo(expected);
        }
    }
}

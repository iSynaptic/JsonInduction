using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JsonInduction
{
    public abstract class PathAwareJsonVisitor : JsonVisitor
    {
        private readonly Stack<string> _history = new Stack<string>();

        public override JToken Visit(JToken token)
        {
            _history.Clear();
            return base.Visit(token);
        }

        protected override JToken VisitProperty(JProperty property)
        {
            _history.Push(property.Name);
            var token = base.VisitProperty(property);
            _history.Pop();

            return token;
        }

        protected IEnumerable<string> History => _history.Reverse();
        protected string Path => String.Join(".", History);
    }
}

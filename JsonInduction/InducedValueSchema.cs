using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonInduction
{
    public class InducedValueSchema : InducedVertexSchema
    {
        private Dictionary<string, int> _stringValues;

        public bool CanBeNull { get; set; }
        public PrimativeTypes AllowedTypes { get; set; }

        public void AddNull() => CanBeNull = true; 
        public void AddBool(bool value) => AllowedTypes |= PrimativeTypes.Boolean;
        public void AddFloat(float value) => AllowedTypes |= PrimativeTypes.Float;
        public void AddDate(DateTime value) => AllowedTypes |= PrimativeTypes.Date;
        public void AddGuid(Guid value) => AllowedTypes |= PrimativeTypes.Guid;
        public void AddTimeSpan(TimeSpan value) => AllowedTypes |= PrimativeTypes.TimeSpan;
        public void AddInteger(long value) => AllowedTypes |= PrimativeTypes.Integer;
        public void AddUri(Uri value) => AllowedTypes |= PrimativeTypes.Uri;

        public void AddString(string value)
        {
            AllowedTypes |= PrimativeTypes.String;

            if (_stringValues == null)
                _stringValues = new Dictionary<string, int>();

            int count = 0;
            _stringValues.TryGetValue(value, out count);

            _stringValues[value] = count + 1;
        }

        public override string ToString()
        {
            var types =
                Enum.GetValues(typeof(PrimativeTypes))
                    .Cast<PrimativeTypes>()
                    .Where(x => (AllowedTypes & x) == x)
                    .Select(x => Enum.GetName(typeof(PrimativeTypes), x))
                    .ToArray();

            return $"{(CanBeNull ? "? " : "")}{String.Join("|", types)}"; ;
        }
    }
}

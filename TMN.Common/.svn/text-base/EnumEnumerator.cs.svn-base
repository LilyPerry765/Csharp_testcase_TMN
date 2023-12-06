using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    public static class EnumEnumerator<T>
    {
        public static IEnumerable<EnumItem> Enumerator
        {
            get
            {
                foreach (var item in Enum.GetValues(typeof(T)))
                    yield return new EnumItem(Enum.GetName(typeof(T), item), item);
            }
        }
    }

    public struct EnumItem
    {
        public EnumItem(string name, object value)
            : this()
        {
            Name = name;
            Value = value;
        }

        public string Name
        {
            get;
            set;
        }

        public object Value
        {
            get;
            set;
        }
    }
}

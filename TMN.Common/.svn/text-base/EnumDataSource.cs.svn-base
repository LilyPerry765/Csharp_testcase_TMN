using System;
using System.Windows.Markup;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace TMN.Assets
{
    public class EnumDataSource : MarkupExtension
    {
        public EnumDataSource()
        {
        }

        public EnumDataSource(Type enumType)
        {
            EnumType = enumType;
        }

        public Type EnumType
        {
            get;
            set;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return GetItems();
        }

        public IEnumerable<EnumItem> GetItems()
        {
            foreach (var item in EnumType.GetFields())
            {
                if (!item.IsSpecialName)
                    yield return new EnumItem(EnumType, item);
            }
        }
    }

    public class EnumItem
    {
        private Type enumType;
        private FieldInfo enumField;

        public EnumItem(Type enumType, FieldInfo enumField)
        {
            this.enumType = enumType;
            this.enumField = enumField;
        }

        public object Value
        {
            get
            {
                return Convert.ChangeType(Enum, System.Enum.GetUnderlyingType(enumType));
            }
        }

        public object Enum
        {
            get
            {
                return enumField.GetValue(null);
            }
        }

        private string GetEnumCaption(FieldInfo fieldInfo)
        {
            DescriptionAttribute[] attribs = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attribs.Length == 0)
                return fieldInfo.GetValue(null).ToString();
            else
                return attribs[0].Description;
        }

        public override string ToString()
        {
            return GetEnumCaption(enumField);
        }
    }
}

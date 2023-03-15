using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class AttrbuteExtensions
    {
        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider) where TAttribute : Attribute
        {
            return attributeProvider.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() is TAttribute att;
        }
    }
}

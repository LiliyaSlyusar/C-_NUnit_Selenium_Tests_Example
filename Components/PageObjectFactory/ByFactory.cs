using Core.Helpers;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Components.PageObjectFactory
{
    public static class ByFactory
    {
        private static readonly Dictionary<Type, DefaultFindsByAttribute> CachedDefaultFindsAttributes = new Dictionary<Type, DefaultFindsByAttribute>();

        public static By Create(How how, string usingValue, Type customFinderType = null)
        {
            if (string.IsNullOrWhiteSpace(usingValue) && how != How.Custom)
            {
                throw new ArgumentException(string.Format("[Using] should not be empty when [How={0}]", how));
            }

            switch (how)
            {
                case How.Id:
                    return By.Id(usingValue);
                case How.Name:
                    return By.Name(usingValue);
                case How.TagName:
                    return By.TagName(usingValue);
                case How.ClassName:
                    return By.ClassName(usingValue);
                case How.CssSelector:
                    return By.CssSelector(usingValue);
                case How.LinkText:
                    return By.LinkText(usingValue);
                case How.PartialLinkText:
                    return By.PartialLinkText(usingValue);
                case How.XPath:
                    return By.XPath(usingValue);
                case How.Custom:
                    return Create(customFinderType, usingValue);
            }

            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                "Cannot construct [By] from [How={0}], [Using={1}]", how, usingValue));
        }

        public static By Create(Type customFinderType, string usingValue)
        {
            if (customFinderType == null)
            {
                throw new ArgumentException("Cannot use [How.Custom] without supplying a [CustomFinderType]");
            }

            if (!customFinderType.IsSubclassOf(typeof(By)))
            {
                throw new ArgumentException("[CustomFinderType] must be a descendant of [By] class");
            }

            return Activator.CreateInstance(customFinderType, usingValue) as By;
        }

        public static By Create(FindsByAttribute attribute)
        {
            return Create(attribute.How, attribute.Using, attribute.CustomFinderType);
        }

        public static By Create(DefaultFindsByAttribute attribute)
        {
            return Create(attribute.How, attribute.Using, attribute.CustomFinderType);
        }

        public static By Create(Type memberType, MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(FindsByAttribute), true).Cast<FindsByAttribute>().ToArray();

            // Use default selector for block behavior
            if (attributes.Length == 0)
            {
                var blockAttribute = GetCachedBlockAttribute(memberType);

                if (blockAttribute != null)
                {
                    return Create(blockAttribute);
                }

                return Create(How.Id, memberInfo.Name);
            }

            Array.Sort(attributes);
            var by = attributes.Select(Create).ToArray();

            if (DetectFindsByAllAttribute(memberInfo))
            {
                return new ByAll(by);
            }

            if (DetectFindsBySequenceAttribute(memberInfo))
            {
                return new ByChained(by);
            }

            return new ByAll(by);
        }

        private static bool DetectFindsByAllAttribute(MemberInfo memberInfo)
        {
            return memberInfo.HasAttribute<FindsByAllAttribute>();
        }

        private static bool DetectFindsBySequenceAttribute(MemberInfo memberInfo)
        {
            return memberInfo.HasAttribute<FindsBySequenceAttribute>();
        }

        private static DefaultFindsByAttribute GetCachedBlockAttribute(Type memberType)
        {
            Type type;
            if (memberType.IsGenericType && typeof(IList<>).IsAssignableFrom(memberType.GetGenericTypeDefinition()))
            {
                type = memberType.GenericTypeArguments[0];
            }
            else
            {
                type = memberType;
            }

            if (!CachedDefaultFindsAttributes.TryGetValue(type, out var attribute))
            {
                attribute = type.GetCustomAttribute<DefaultFindsByAttribute>(true);
                CachedDefaultFindsAttributes[type] = attribute;
            }

            return attribute;
        }
    }
}

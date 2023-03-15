using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.PageObjectFactory
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultFindsByAttribute : Attribute
    {
        /// <summary>Gets or sets the method used to look up the element</summary>
        [DefaultValue(How.Id)]
        public How How { get; set; }

        /// <summary>
        /// Gets or sets the value to lookup by (i.e. for How.Name, the actual name to look up)
        /// </summary>
        public string Using { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the <see cref="T:System.Type" /> of the custom finder. The custom finder must
        /// descend from the <see cref="T:OpenQA.Selenium.By" /> class, and expose a public constructor that takes a <see cref="T:System.String" />
        /// argument.
        /// </summary>
        public Type CustomFinderType { get; set; }
    }

}

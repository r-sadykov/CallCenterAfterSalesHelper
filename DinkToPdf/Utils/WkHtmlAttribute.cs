using System;

namespace DinkToPdf.Utils
{
    [AttributeUsage(AttributeTargets.Property)]
    public class WkHtmlAttribute : Attribute
    {
        public string Name { get; private set; }

        public WkHtmlAttribute(string name)
        {
            Name = name;
        }
    }
}

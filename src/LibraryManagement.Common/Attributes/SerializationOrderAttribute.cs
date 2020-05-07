using System;

namespace LibraryManagement.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class SerializationOrderAttribute : Attribute
    {
        public SerializationOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }
    }
}

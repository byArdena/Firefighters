using System;
using UnityEngine;

namespace Attributes
{
    public class SerializeInterfaceAttribute : PropertyAttribute
    {
        public Type Type { get; }

        public SerializeInterfaceAttribute(Type type)
        {
            Type = type;
        }
    }
}

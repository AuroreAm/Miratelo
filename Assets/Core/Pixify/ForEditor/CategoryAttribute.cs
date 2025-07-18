using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class CategoryAttribute : Attribute
    {
        public string Name { get; }

        public CategoryAttribute ( string Category )
        { Name = Category; }
    }
}

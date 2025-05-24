using System.Reflection;
using System;

namespace Pixify
{
    [Category("")]
    [Serializable]
    public abstract class node
    {
        public uint nodeId;

        /// <summary>
        /// called after the node is ready to be initialized
        /// </summary>
        public virtual void Create ()
        {  }
    }

    
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class CategoryAttribute : Attribute
    {
        public string Name { get; }

        public CategoryAttribute ( string Category )
        { Name = Category; }
    }

    [AttributeUsage (AttributeTargets.Field)]
    public sealed class DependAttribute : Attribute
    {
    }

    /// <summary>
    /// this attribute will let field to be serialized in paper // must be public
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ExportAttribute : Attribute { }
}
using UnityEngine;
using System;

namespace Pixify
{
    [Category("")]
    [Serializable]
    [NodeTint(1,1,1)]
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
    /// this attribute will let the node to be unique for a graph, it is unique inside the descendant of decoratorModel where CreateNode is called
    /// Does not work with Decorators
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class UniqueAttribute : Attribute
    {}
    
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class NodeTintAttribute : Attribute
    {
        public Color Tint;
        public NodeTintAttribute( float r = 1, float g = 1, float b = 1)
        {
            Tint = new Color(r,g,b);
        }
    }

    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class NodeDescriptionAttribute : Attribute
    {
        public string Description;
        public NodeDescriptionAttribute( string Description )
        { this.Description = Description; }
    }

    /// <summary>
    /// this attribute will let field to be serialized in paper // must be public
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ExportAttribute : Attribute { }
}
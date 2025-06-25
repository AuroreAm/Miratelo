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

        public node ()
        {
            treeBuilder.Write ( this );
        }
    }
    
    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public class CategoryAttribute : Attribute
    {
        public string Name { get; }

        public CategoryAttribute ( string Category )
        { Name = Category; }
    }

    /// <summary>
    /// tell the character that this node depend on this module or unique node
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class DependAttribute : Attribute
    {
    }

    /// <summary>
    /// tell the treebuilder that is node is unique for the character
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

    /// <summary>
    /// this attribute will let field to be serialized in paper // must be public
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ExportAttribute : Attribute { }
}
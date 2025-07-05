using UnityEngine;
using System;

namespace Pixify
{
    [Category("")]
    [Serializable]
    [NodeTint(1,1,1)]
    public class atom
    {
        public uint atomId;

        public static T Write <T> ( string AssemblyQualifiedType, string AtomData )  where T:atom
        {
            if ( !string.IsNullOrEmpty (AssemblyQualifiedType) && Type.GetType (AssemblyQualifiedType) != null )
            {
                atom n = (atom) Activator.CreateInstance (Type.GetType (AssemblyQualifiedType));
                JsonUtility.FromJsonOverwrite (AtomData, n);
                return (T) n;
            }
            else
            {
                throw new InvalidCastException ("Trying to create invalid type " + AssemblyQualifiedType);
            }
        }
    }

    public interface ICatomFactory
    {
        public Character character {get;}
        public void AfterCreateInstance ( catom catom );
    }

    // atom with a character, forced contract
    public abstract class catom : atom
    {
        public Character character { private set; get;}

        static Character _character;

        public static T New <T> ( ICatomFactory factory ) where T:catom, new ()
        {
            _character = factory.character;
            T c = new T ();
            factory.AfterCreateInstance (c);
            return c;
        }

        public static T New <T> ( string AssemblyQualifiedType, string AtomData, ICatomFactory factory )  where T:catom
        {
            _character = factory.character;
            if ( !string.IsNullOrEmpty (AssemblyQualifiedType) && Type.GetType (AssemblyQualifiedType) != null )
            {
                catom n = (catom) Activator.CreateInstance (Type.GetType (AssemblyQualifiedType));
                JsonUtility.FromJsonOverwrite (AtomData, n);
                factory.AfterCreateInstance (n);
                return (T) n;
            }
            else
            {
                _character = null;
                throw new InvalidCastException ("Trying to create invalid type " + AssemblyQualifiedType);
            }
        }

        public static T New <T> (Type t, ICatomFactory factory ) where T:catom
        {
            _character = factory.character;
            catom c = (catom) Activator.CreateInstance(t);
            factory.AfterCreateInstance (c);
            return (T) c;
        }

        public catom ()
        {
            character = _character;
            _character = null;
            
            if (!character)
            throw new InvalidOperationException ( "a catom instance was created without character, use New<T> to create an instance of cnode" );
        }

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

    /// <summary>
    /// tell the character that this catom depend on this module or action
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class DependAttribute : Attribute
    {
    }
    
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
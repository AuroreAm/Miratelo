using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public abstract class VirtusAuthor : ScriptableObject, IBlockAuthor, IVirtusAuthor
    {
        public virtus Instance ()
        {
            List <Type> PixTypes = new List<Type> ();
            PixTypes.A <virtus> ();
            RequiredPix ( in PixTypes );
            var b = new PreBlock ( PixTypes.ToArray (), this ).CreateBlock ();
            return b.GetPix <virtus> ();
        }

        protected abstract void RequiredPix ( in List <Type> a );

        public virtual void OnWriteBlock()
        {}
    }

    
    public interface IVirtusAuthor
    {
        public virtus Instance ();
    }
}

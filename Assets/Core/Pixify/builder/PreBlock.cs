using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public interface IBlockAuthor
    {
        /// <summary>
        /// write the data for the bricks in bloc before they are initialized with Create ();
        /// To write the data, use new() for an instance of PreBlock.Package
        /// PreBlock.Package can only be instancied within this method
        /// </summary>
        public void OnWriteBlock ();
    }

    public class PreBlock
    {
        static PreBlock _o;
        Dictionary <Type,pix> mains = new Dictionary<Type, pix> ();
        
        /// <summary>
        /// use the construction to transfer the Data
        /// target is o
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public abstract class Package <T> where T : pix, new ()
        {
            public Package ()
            {
                if (_o == null)
                throw new InvalidOperationException ( "PreBlock.package can only be instantied in OnWriteBlock ()" );

                o = _o.Querry <T> ();
            }
            protected T o;
        }

        T Querry <T> () where T:pix, new ()
        {
            return RequireBrick ( typeof (T) ) as T;
        }

        IBlockAuthor Author;
        public PreBlock ( Type[] bricks, IBlockAuthor author )
        {
            foreach (var t in bricks)
            if (!t.IsSubclassOf (typeof (pix)))
            throw new InvalidOperationException ( "only pix type can be brick" );

            Author = author;
            for (int i = 0; i < bricks.Length; i++)
                RequireBrick ( bricks [i] );
        }

        public block CreateBlock ()
        {
            _o = this;
            Author.OnWriteBlock ();
            _o = null;

            return new block ( mains );
        }

        void IntegrateBrick ( pix brick )
        {
            if ( !mains.ContainsKey (brick.GetType ()) )
            mains.Add ( brick.GetType (), brick );

            Type current = brick.GetType ();
            
            while ( current != typeof ( pix ) )
            {
                var fis = current.GetFields( BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                foreach (var fi in fis)
                {
                if (fi.GetCustomAttribute<DependAttribute>() != null)
                    fi.SetValue ( brick, RequireBrick (fi.FieldType) );
                }
                current = current.BaseType;
            } 
        }

        pix RequireBrick ( Type PixType )
        {
            if ( mains.ContainsKey ( PixType ) )
                return mains [PixType];
            else
            {
                var c = Activator.CreateInstance (PixType) as pix;
                IntegrateBrick ( c );
                return c;
            }
        }

    }

}

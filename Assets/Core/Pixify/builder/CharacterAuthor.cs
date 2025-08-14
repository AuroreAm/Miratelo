using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public class CharacterAuthor : MonoBehaviour, IBlockAuthor
    {
        public void Awake()
        {
            Spawn ( transform.position, transform.rotation );
            Destroy (gameObject);
        }

        GameObject Spawned;
        public block Spawn ( Vector3 position, Quaternion rotation )
        {
            Spawned = new GameObject (gameObject.name);
            Spawned.transform.position = transform.position = position;

            var writers = GetComponents<Writer>();
            List <Type> PixTypes = new List<Type> ();

            foreach ( var a in writers )
            a.RequiredPix( in PixTypes );
            PixTypes.Add ( typeof (character) );

            var Constructor = new PreBlock ( PixTypes.ToArray(), this );
            var b = Constructor.CreateBlock ();

            foreach (var a in writers)
            a.AfterWrite (b);

            foreach ( var a in writers )
            a.AfterSpawn (position, rotation , b);

            return b;
        }

        public void OnWriteBlock()
        {
            var writers = GetComponents<Writer>();
            new character.package ( Spawned );
            foreach (var a in writers)
            a.OnWriteBlock ();
        }
    }
}
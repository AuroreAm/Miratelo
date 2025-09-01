using System;
using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class a_slash_attack : virtus.pixi
    {
        Mesh trail;
        Material TrailMaterial;
        int FrameNumber;

        s_skin performer;
        SlashPath path;
        Sword sword;

        float duration;

        Vector3 position;
        float time;

        protected override void Create1()
        {
            trail = new Mesh ();
        }

        public class package : PreBlock.Package <a_slash_attack>
        {
            public package ( Material material, int frameNumber )
            {
                o.TrailMaterial = material;
                o.FrameNumber = frameNumber;
            }
        }

        static SlashPath _path;
        static Sword _sword;
        static float _duration;
        public static void Fire ( int name, Sword sword, SlashPath path, float duration )
        {
            _path = path;
            _sword = sword;
            _duration = duration;
            VirtualPoolMaster.RentVirtus (name);
        }

        protected override void Start()
        {
            path = _path;
            sword = _sword;
            performer = sword.Owner.ss;

            duration = _duration;

            position = performer.position;
            time = 0;
            rayPtr = 0;
            Hitted.Clear ();

            CreateTrail ();
        }

        protected override void Step()
        {
            int pathCap = Mathf.Clamp(Mathf.RoundToInt(time / SlashPath.Delta), 0, path.Orig.Length);
            time += Time.deltaTime;
            
            while (pathPtr < pathCap)
            {
                RemeshTrail();
                pathPtr ++;
            }

            TrailUV ();

            if ( rayPtr != pathPtr && pathPtr < PathCount )
            {
                Raycast ();
                rayPtr = pathPtr;
            }

            Graphics.DrawMesh(trail, position, Quaternion.identity, TrailMaterial, 0);

            if (time > duration)
                v.Return_ ();
        }

        Vector3[] vertices;
        int[] triangles;
        int pathPtr;
        int PathCount;
        Vector2[] uvs;

        void CreateTrail ()
        {
            PathCount = path.Orig.Length;
            vertices = new Vector3[PathCount * 2];
            triangles = new int[ (PathCount - 1) * 6];
            uvs = new Vector2[PathCount * 2];

            pathPtr = 0;
            RemeshTrail ();
        }
        
        void RemeshTrail()
        {
            trail.Clear();

            // Update vertices of current path
            vertices[pathPtr * 2] = Vecteur.LDir ( performer.rotY, path.Orig[pathPtr] ) + ( performer.position - position );
            vertices[pathPtr * 2 + 1] = Vecteur.LDir ( performer.rotY, path.Dir[pathPtr] * sword.Length ) + vertices[pathPtr * 2];

            // Set future path to current path to discard them
            for (int i = pathPtr + 1; i < PathCount; i++)
            {
                vertices[i * 2] = vertices[pathPtr * 2];
                vertices[i * 2 + 1] = vertices[pathPtr * 2 + 1];
            }

            

            // Update triangles
            int quadCount = PathCount - 1;
            int t = 0;

            for (int i = 0; i < quadCount; i++)
            {
                int i0 = i * 2;
                int i1 = i0 + 1;
                int i2 = (i + 1) * 2;
                int i3 = i2 + 1;

                // first triangle
                triangles[t++] = i0;
                triangles[t++] = i2;
                triangles[t++] = i1;

                // second triangle
                triangles[t++] = i1;
                triangles[t++] = i2;
                triangles[t++] = i3;
            }

            // asign to mesh
            trail.vertices = vertices;
            trail.triangles = triangles;

            trail.RecalculateBounds();
            trail.RecalculateNormals();
        }

        void TrailUV ()
        {
            float normalizedTime = time / duration;
            int frameIndex = Mathf.FloorToInt ( normalizedTime * (FrameNumber - 1) );

            float frameWidth = 1f / FrameNumber;
            float frameOffset = frameIndex * frameWidth;

            for (int i = 0; i < PathCount; i++)
            {
                float v = ( (float) i ) / (PathCount - 1);

                uvs[i * 2] = new Vector2( frameOffset + v * frameWidth, 0 );
                uvs[i * 2 + 1] = new Vector2( frameOffset + v * frameWidth, 1 );
            }

            trail.uv = uvs;
        }

        RaycastHit hit;
        int rayPtr;
        void Raycast ()
        {
            // raycast Z pattern using the paths
            Vector3 A = vertices[rayPtr * 2] + position;
            Vector3 B = vertices[rayPtr * 2 + 1] + position;
            Vector3 D = vertices[pathPtr * 2 + 1] + position;
            Vector3 ABHalf = ( A + B ) / 2;
            Vector3 CDHalf = ( vertices[pathPtr * 2] + position + D ) / 2;

            if ( Physics.Linecast ( A , B, out hit, Vecteur.SolidCharacterAttack ) || Physics.Linecast ( B, D, out hit, Vecteur.SolidCharacterAttack ) || Physics.Linecast ( ABHalf, D, out hit, Vecteur.SolidCharacterAttack  ) || Physics.Linecast ( A, CDHalf, out hit, Vecteur.SolidCharacterAttack ) )
                Hit ();
        }

        List <int> Hitted = new List<int> ();

        void Hit ()
        {
            if ( Hitted.Contains ( hit.collider.id () ) ) return;

            if ( hit.collider.gameObject.layer == Vecteur.ATTACK )
            {
                sword.Owner.se.SendMessage ( new parried () );
                v.Return_ ();
                return;
            }
            
            if (  Element.Contains ( hit.collider.id () ) && Element.ElementActorIsNotAlly ( hit.collider.id (), sword.Owner.faction ) )
            {
                Element.SendMessage ( hit.collider.id (), new Slash ( 2 ) );
                Hitted.Add ( hit.collider.id () );
            }
        }
    }
}
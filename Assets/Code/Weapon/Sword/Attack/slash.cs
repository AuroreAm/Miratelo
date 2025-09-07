using System.Collections.Generic;
using Lyra;
using UnityEngine;
using static Triheroes.Code.Sword.Combat.slay;

namespace Triheroes.Code.Sword
{
    [star (order.slash)]
    public class slash : virtus.star
    {
        skin performer;
        path path;
        sword sword;

        Mesh trail = new Mesh ();
        Material material;
        int frame_number;
        float duration;
        Vector3 position;
        float time;

        public class ink : ink <slash>
        {
            public ink ( Material material, int frame_number )
            {
                o.material = material;
                o.frame_number = frame_number;
            }
        }

        static path _path;
        static sword _sword;
        static float _duration;
        public static void fire ( int name, sword sword, path path, float duration )
        {
            _path = path;
            _sword = sword;
            _duration = duration;
            orion.rent (name);
        }

        protected override void _start ()
        {
            path = _path;
            sword = _sword;
            performer = ((actor)sword.owner).skin;

            duration = _duration;
            position = performer.position;
            time = 0;
            ray_ptr = 0;
            hitted.Clear ();

            CreateTrail ();
        }

        protected override void _step ()
        {
            int path_cap = Mathf.Clamp ( Mathf.RoundToInt ( time / path.delta ), 0, path.orig.Length );
            time += Time.deltaTime;
            
            while (path_ptr < path_cap)
            {
                remesh ();
                path_ptr ++;
            }

            form_uv ();

            if ( ray_ptr != path_ptr && path_ptr < path_count )
            {
                raycast ();
                ray_ptr = path_ptr;
            }

            Graphics.DrawMesh (trail, position, Quaternion.identity, material, 0);

            if (time > duration)
                virtus.return_ ();
        }

        Vector3[] vertices;
        int[] triangles;
        int path_ptr;
        int path_count;
        Vector2[] uvs;

        void CreateTrail ()
        {
            path_count = path.orig.Length;
            vertices = new Vector3[path_count * 2];
            triangles = new int[ (path_count - 1) * 6];
            uvs = new Vector2[path_count * 2];

            path_ptr = 0;
            remesh ();
        }
        
        void remesh()
        {
            trail.Clear();

            // Update vertices of current path
            vertices[path_ptr * 2] = vecteur.ldir ( performer.roty, path.orig [path_ptr] ) + ( performer.position - position );
            vertices[path_ptr * 2 + 1] = vecteur.ldir ( performer.roty, path.dir [path_ptr] * sword.length ) + vertices[path_ptr * 2];

            // Set future path to current path to discard them
            for (int i = path_ptr + 1; i < path_count; i++)
            {
                vertices[i * 2] = vertices[path_ptr * 2];
                vertices[i * 2 + 1] = vertices[path_ptr * 2 + 1];
            }

            // Update triangles
            int quadCount = path_count - 1;
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

        void form_uv ()
        {
            float normalized_time = time / duration;
            int frame_index = Mathf.FloorToInt ( normalized_time * (frame_number - 1) );

            float frame_width = 1f / frame_number;
            float frame_offset = frame_index * frame_width;

            for (int i = 0; i < path_count; i++)
            {
                float v = ( (float) i ) / (path_count - 1);

                uvs[i * 2] = new Vector2( frame_offset + v * frame_width, 0 );
                uvs[i * 2 + 1] = new Vector2( frame_offset + v * frame_width, 1 );
            }

            trail.uv = uvs;
        }

        RaycastHit ray_hit;
        int ray_ptr;
        void raycast ()
        {
            // raycast Z pattern using the paths
            Vector3 A = vertices[ray_ptr * 2] + position;
            Vector3 B = vertices[ray_ptr * 2 + 1] + position;
            Vector3 D = vertices[path_ptr * 2 + 1] + position;
            Vector3 ABHalf = ( A + B ) / 2;
            Vector3 CDHalf = ( vertices[path_ptr * 2] + position + D ) / 2;

            if ( Physics.Linecast ( A , B, out ray_hit, vecteur.SolidCharacter ) )
                hit ();
            
            if ( Physics.Linecast ( B, D, out ray_hit, vecteur.SolidCharacter ) )
                hit ();
            
            if ( Physics.Linecast ( ABHalf, D, out ray_hit, vecteur.SolidCharacter ) )
                hit ();

            if ( Physics.Linecast ( A, CDHalf, out ray_hit, vecteur.SolidCharacter ) )
                hit ();
        }

        List <int> hitted = new List<int> ();
        void hit ()
        {
            if ( hitted.Contains ( ray_hit.collider.id () ) ) return;
            
            if (  pallas.contains ( ray_hit.collider.id () ) && pallas.is_enemy ( ray_hit.collider.id (), sword.owner.faction ) )
            {
                pallas.radiate ( ray_hit.collider.id (), new Code.slash ( 2 ) );
                hitted.Add ( ray_hit.collider.id () );
            }
        }
    }
}
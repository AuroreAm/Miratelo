using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    // billboard sprite sheet
    [inked]
    public class stellar : spectre {
        Mesh[] frames;
        Material material;
        int frame_number;
        float frame_id;

        const float speed = 12;
        Vector3 position;
        bool loop;

        public class ink : ink<stellar> {
            public ink(Material material, int frame_number, bool loop) {
                o.material = material;
                o.frame_number = frame_number;
                o.loop = loop;
            }
        }

        #region fire
        static Vector3 _position;
        public class w : bridge {
            public int fire ( Vector3 position ) {
                _position = position;
                return orion.rent(name);
            }

            public void stop (int id) {
                orion.get <stellar> (name, id).stop ();
            }

            public void set_position ( int id, Vector3 position ) {
                orion.get <stellar> (name, id).position = position;
            }
        }
        #endregion

        protected override void __ready() {
            compute_mesh_frame(frame_number);
        }

        protected override void _start() {
            position = _position;
            frame_id = 0;
        }

        void stop (){
            if ( !loop )
            {
                Debug.LogWarning ("non looping stellar can't be stopped");
                return;
            }
            virtus.return_ ();
        }

        protected override void _step() {
            Graphics.DrawMesh(frames[Mathf.FloorToInt(frame_id)], position, camera.get_billboard_rotation(position), material, 0);

            frame_id += Time.deltaTime * speed;
            if ( !loop && frame_id > frame_number ) {
                virtus.return_();
            }
        }

        // create all quad meshes for each frame
        void compute_mesh_frame(int frame_number) {
            frames = new Mesh[frame_number];
            
            float frame_width = 1f / frame_number;

            for (int i = 0; i < frame_number; i++)
            {
                Mesh m = new Mesh();

                // Standard quad vertices
                Vector3[] vertices = new Vector3[]
                {
                    new Vector3(-0.5f, -0.5f, 0), // Bottom Left
                    new Vector3( 0.5f, -0.5f, 0), // Bottom Right
                    new Vector3(-0.5f,  0.5f, 0), // Top Left
                    new Vector3( 0.5f,  0.5f, 0)  // Top Right
                };

                // UV mapping for this frame
                float uMin = i * frame_width;
                float uMax = (i + 1) * frame_width;

                Vector2[] uvs = new Vector2[]
                {
                    new Vector2(uMin, 0), // Bottom Left
                    new Vector2(uMax, 0), // Bottom Right
                    new Vector2(uMin, 1), // Top Left
                    new Vector2(uMax, 1)  // Top Right
                };

                int[] triangles = new int[]
                {
                    0, 2, 1, // First triangle
                    2, 3, 1  // Second triangle
                };

                m.vertices = vertices;
                m.uv = uvs;
                m.triangles = triangles;
                m.RecalculateNormals();

                frames [i] = m;
            }
        }
    }
}
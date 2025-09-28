using Lyra;
using UnityEngine;

namespace Triheroes.Code {
    // billboard sprite sheet
    [inked]
    [need_ready]
    public class stellar : spectre {
        Mesh[] frame;
        Material material;
        int frame_number;
        float frame_id;

        const float speed = 4;
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
        }
        #endregion

        protected override void _ready() {
            compute_mesh_frame(frame_number);
        }

        protected override void _start() {
            position = _position;
            frame_id = 0;
        }

        void stop (){
            virtus.return_ ();
        }

        protected override void _step() {
            Graphics.DrawMesh(frame[Mathf.FloorToInt(frame_id)], position, camera.o.get_billboard_rotation(position), material, 0);

            frame_id += Time.deltaTime * speed;
            if ( !loop && frame_id > frame_number ) {
                virtus.return_();
            }
        }

        // create all quad meshes for each frame
        void compute_mesh_frame(int frame_number) {
            frame = new Mesh[frame_number];

            for (int i = 0; i < frame_number; i++) {
                frame[i] = new Mesh();
                frame[i].vertices = new Vector3[] { new Vector3(-.5f, -.5f, 0), new Vector3(.5f, -.5f, 0), new Vector3(.5f, .5f, 0), new Vector3(-.5f, .5f, 0) };
                frame[i].triangles = new int[] { 0, 1, 2, 0, 2, 3 };

                // strip uv to frame number, only horizontal sprite sheet
                float frame_width = 1f / frame_number;
                float frame_offset = i * frame_width;
                frame[i].uv = new Vector2[] { new Vector2(frame_offset, 0), new Vector2(frame_offset + frame_width, 0), new Vector2(frame_offset + frame_width, 1), new Vector2(frame_offset, 1) };

                frame[i].RecalculateBounds();
                frame[i].RecalculateNormals();
            }
        }
    }
}
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class after_image : spectre {
        static Mesh quad;
        Material material;

        RenderTexture texture;

        Vector3 position;
        float t;
        float duration;

        MaterialPropertyBlock block;

        static Vector3 _position;
        static float _duration;

        const int tw = 128; const int th = 128;

        public class w : bridge {
            public int fire ( Vector3 position, float duration ) {
                _position = position;
                _duration = duration;
                return orion.rent (name);
            }
        }

        public class ink : ink <after_image> {
            public ink ( Material material ) {
                o.material = material;
            }
        }
        
        const float quad_size = 4;
        const string main_texture = "_MainTex";
        const string color = "_Color";
        protected override void __ready() {
            if ( quad == null )
            create_quad ( quad_size );

            texture = new RenderTexture ( tw, th, 0, RenderTextureFormat.ARGB32 );
            texture.filterMode = FilterMode.Point;
            
            block = new MaterialPropertyBlock ();
            block.SetTexture ( main_texture, texture );
        }

        protected override void _start() {
            position = _position;
            duration = _duration;
            t = duration;
            direct_capture.o.capture ( position, quad_size, texture );
        }

        protected override void _step() {
            if ( t <= 0 ) {
                virtus.return_ ();
                return;
            }

            var a = t / duration;
            a = Mathf.Floor(a * 5) / 5;

            t -= Time.deltaTime;
            
            // Get the current color from the material
            Color current_color = material.GetColor (color);
            
            // Create new color with the computed alpha
            Color new_color = new Color(current_color.r, current_color.g, current_color.b, a);
            
            // Apply the color to the property block
            block.SetColor(color, new_color);

            Graphics.DrawMesh ( quad, position, camera.get_billboard_rotation (position), material, 0, null, 0, block, false );
        }

        protected override void _stop() {
            // Clear the texture for reuse
            Graphics.SetRenderTarget(texture);
            GL.Clear(false, true, Color.clear);
            Graphics.SetRenderTarget(null);
        }

        static void create_quad ( float size ) {
            quad = new Mesh ();
            float halfsize = size / 2;

            // Standard quad vertices
            Vector3[] vertices = new Vector3[]
            {
                new Vector3(-halfsize, -halfsize, 0), // Bottom Left
                new Vector3( halfsize, -halfsize, 0), // Bottom Right
                new Vector3(-halfsize,  halfsize, 0), // Top Left
                new Vector3( halfsize,  halfsize, 0)  // Top Right
            };

            float uMin = 0;
            float uMax = 1;

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

            quad.vertices = vertices;
            quad.uv = uvs;
            quad.triangles = triangles;
            quad.RecalculateNormals();
        }
    }
}
using System;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    [RequireComponent (typeof (CanvasRenderer))]
    public sealed partial class UILayer : MaskableGraphic {

        [SerializeField]
        tile [] _Tiles;

        public Texture texture;

        [SerializeField]
        Vector2Int tile_count;
        Dictionary <term, Vector2Int> tiles;

        List <UIVertex> vs = new List <UIVertex> ();
        List <Vector3Int> ts = new List <Vector3Int> (); 

        static Vector2 offset;

        public Vector2Int tile_of ( term name ) {
            if ( tiles == null )
            init_tiles ();
            return tiles [name];
        }

        void init_tiles () {
            tiles = new Dictionary<term, Vector2Int> ();
            foreach ( var t in _Tiles ) {
                tiles.Add ( new term (t.name), t.position );
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh) {
            vh.Clear ();
            foreach ( var v in vs )
            vh.AddVert (v);

            foreach ( var t in ts )
            vh.AddTriangle ( t.x, t.y, t.z );

            vs.Clear (); ts.Clear ();
        }
        
        public sealed override Texture mainTexture => texture;

        UIVertex [] v4 = new UIVertex [4];
        /// <param name="xys"> bottom left of square </param>
        /// <param name="tile"> tile id </param>
        /// <param name="tile_offset_scale"> offset and scale, relative to tile </param>
        public void draw_square ( Vector3 xys, Vector2Int tile, Color color, Vector4 tile_offset_scale ) {
            // Calculate UV coordinates based on tile grid
            Vector2 tex_size = new Vector2(1f / tile_count.x, 1f / tile_count.y);
            Vector2 uvmin;
            Vector2 uvmax;
            // get min
            uvmin = new Vector2(tile.x * tex_size.x, tile.y * tex_size.y); // tiling pos
            uvmin += tex_size * new Vector2 ( tile_offset_scale.x, tile_offset_scale.y ); // offset
            uvmax = uvmin + tex_size * new Vector2 ( tile_offset_scale.z, tile_offset_scale.w ); // scaled tex size max

            // Create vertices for a quad
            float x = xys.x + offset.x; float y = xys.y + offset.y; float size = xys.z;

            // Bottom-left vertex
            v4[0] = new UIVertex();
            v4[0].position = new Vector3(x, y, 0);
            v4[0].color = color;
            v4[0].uv0 = new Vector2(uvmin.x, uvmin.y);
            
            // Top-left vertex
            v4[1] = new UIVertex();
            v4[1].position = new Vector3(x, y + size, 0);
            v4[1].color = color;
            v4[1].uv0 = new Vector2(uvmin.x, uvmax.y);
            
            // Top-right vertex
            v4[2] = new UIVertex();
            v4[2].position = new Vector3(x + size, y + size, 0);
            v4[2].color = color;
            v4[2].uv0 = new Vector2(uvmax.x, uvmax.y);
            
            // Bottom-right vertex
            v4[3] = new UIVertex();
            v4[3].position = new Vector3(x + size, y, 0);
            v4[3].color = color;
            v4[3].uv0 = new Vector2(uvmax.x, uvmin.y);

             // Add vertices to the mesh
            int current_count = vs.Count;
            for (int i = 0; i < 4; i++) {
                vs.Add ( v4 [i] );
            }

            // Add two triangles to form a quad
            ts.Add ( new Vector3Int (current_count, current_count + 1, current_count + 2) );
            ts.Add ( new Vector3Int (current_count + 2, current_count + 3, current_count) );
        }

        // NOTE: Duplicate method to skip uv offset calculation for performance
        /// <param name="xys"> bottom left of square </param>
        /// <param name="tile"> tile id </param>
        public void draw_square ( Vector3 xys, Vector2Int tile, Color color ) {
             // Calculate UV coordinates based on tile grid
            Vector2 tex_size = new Vector2(1f / tile_count.x, 1f / tile_count.y);
            Vector2 uvmin;
            Vector2 uvmax;
            // tiling
            uvmin = new Vector2(tile.x * tex_size.x, tile.y * tex_size.y);
            uvmax = new Vector2((tile.x + 1) * tex_size.x, (tile.y + 1) * tex_size.y);

            // Create vertices for a quad
            float x = xys.x; float y = xys.y; float size = xys.z;

            // Bottom-left vertex
            v4[0] = new UIVertex();
            v4[0].position = new Vector3(x, y, 0);
            v4[0].color = color;
            v4[0].uv0 = new Vector2(uvmin.x, uvmin.y);
            
            // Top-left vertex
            v4[1] = new UIVertex();
            v4[1].position = new Vector3(x, y + size, 0);
            v4[1].color = color;
            v4[1].uv0 = new Vector2(uvmin.x, uvmax.y);
            
            // Top-right vertex
            v4[2] = new UIVertex();
            v4[2].position = new Vector3(x + size, y + size, 0);
            v4[2].color = color;
            v4[2].uv0 = new Vector2(uvmax.x, uvmax.y);
            
            // Bottom-right vertex
            v4[3] = new UIVertex();
            v4[3].position = new Vector3(x + size, y, 0);
            v4[3].color = color;
            v4[3].uv0 = new Vector2(uvmax.x, uvmin.y);

             // Add vertices to the stack
            int current_count = vs.Count;
            for (int i = 0; i < 4; i++) {
                vs.Add ( v4 [i] );
            }

            // Add two triangles to form a quad
            ts.Add ( new Vector3Int (current_count, current_count + 1, current_count + 2) );
            ts.Add ( new Vector3Int (current_count + 2, current_count + 3, current_count) );
        }

        public static Vector3 xyscenter ( float x, float y, float size ) {
            float half = size / 2;
            return new Vector3 ( x - half, y - half, size );
        }

        public abstract class base_art {
            public List <UILayer> layers { private set; get; } = new List<UILayer> ();

            public void draw () {
                offset = Vector2.zero;
                _draw ();
                
                foreach ( var l in layers )
                    l.SetVerticesDirty ();
            }

            protected void shift ( Vector2 _offset ) {
                offset += offset;
            }

            protected abstract void _draw ();
        }
    }

    [Serializable]
    struct tile {
        public string name;
        public Vector2Int position;
    }

    /*[RequireComponent(typeof(CanvasRenderer))]
    public abstract class HudGraphic : MaskableGraphic {
        public Texture2D tex;
        public sealed override Texture mainTexture => tex;

        public Action <VertexHelper> _vh;

        protected sealed override void OnPopulateMesh(VertexHelper vh) {
            vh.Clear ();
            _vh?.Invoke ( vh );
        }
    }*/
}
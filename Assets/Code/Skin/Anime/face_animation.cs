using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [inked]
    public class face_animation : moon {
        public class ink : ink <face_animation> {
            public ink ( Material material, Vector2Int tiles ) {
                o.material = material;
                o.tiles = tiles;
            }
        }

        Material material;
        Vector2Int tiles;

        protected override void _ready() {
            set_image_index (0);
        }

        public void set_image_index ( int n ) {
            // column and row from the index
            int column = n % tiles.x;
            int row = tiles.y - 1 - n / tiles.x;

            //  size of each tile
            Vector2 size = new Vector2(1f / tiles.x, 1f / tiles.y);
            
            // offset for this tile
            Vector2 offset = new Vector2(column * size.x, row * size.y);
            
            material.mainTextureOffset = offset;
            material.mainTextureScale = size;
        }
    }
}
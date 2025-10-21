using Lyra;/*
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class ArmorGraphic : HudGraphic {
        public Vector2Int TileNumbers = new Vector2Int (1,1);
        public int capacity = 10;

        public float FrameSize;
        public float SliceLenght = 16;
        public float SliceOffsetPx = 2;
        public Vector2Int FrameTile;
        public Vector2Int CoverTile;
        public Vector2Int NormalSliceTile;
        public Vector2Int AltSliceTile;

        // blur
        public int layers = 5;
        public float spread = 2f;
        
        public void DrawSliceBlur ( VertexHelper vh, int u, Vector2Int tile, Color color ) {
            for (int i = 0; i < layers; i++)
            {
                Vector3 offset =  new Vector3(
                    Mathf.Cos( 360 * i / layers ) * spread,
                    Mathf.Sin( 360 * i / layers ) * spread,
                    0
                );
                float alpha = Mathf.Lerp(0.1f, 0.5f, 1 / layers );
                
                // Add your mesh geometry with offset and decreasing alpha
                DrawSlice ( vh, u, tile, new Color ( color.r, color.g, color.b, color.a * alpha ), offset );
            }
        }

        public void DrawTexture(VertexHelper vh, Vector2Int tile, float size, Color color) {
            // Calculate UV coordinates based on tile grid
            Vector2 textureSize = new Vector2(1f / TileNumbers.x, 1f / TileNumbers.y);
            Vector2 uvMin = new Vector2(tile.x * textureSize.x, tile.y * textureSize.y);
            Vector2 uvMax = new Vector2((tile.x + 1) * textureSize.x, (tile.y + 1) * textureSize.y);
            
            // Get half size
            float halfSize = size / 2;

            // Create vertices for a quad
            UIVertex[] vertices = new UIVertex[4];
            
            // Bottom-left vertex
            vertices[0] = new UIVertex();
            vertices[0].position = new Vector3(-size, -size, 0);
            vertices[0].color = color;
            vertices[0].uv0 = new Vector2(uvMin.x, uvMin.y);
            
            // Top-left vertex
            vertices[1] = new UIVertex();
            vertices[1].position = new Vector3(-size, size, 0);
            vertices[1].color = color;
            vertices[1].uv0 = new Vector2(uvMin.x, uvMax.y);
            
            // Top-right vertex
            vertices[2] = new UIVertex();
            vertices[2].position = new Vector3(size, size, 0);
            vertices[2].color = color;
            vertices[2].uv0 = new Vector2(uvMax.x, uvMax.y);
            
            // Bottom-right vertex
            vertices[3] = new UIVertex();
            vertices[3].position = new Vector3(size, -size, 0);
            vertices[3].color = color;
            vertices[3].uv0 = new Vector2(uvMax.x, uvMin.y);
            
            // Add vertices to the mesh
            int currentVertCount = vh.currentVertCount;
            for (int i = 0; i < 4; i++) {
                vh.AddVert(vertices[i]);
            }
            
            // Add two triangles to form a quad
            vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
            vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
        }

        public void DrawSlice ( VertexHelper vh, int i, Vector2Int tile, Color color, Vector3 off ) {
            // Calculate angle for this slice
            float anglePerSlice = 360f / capacity;
            float startAngle = i * anglePerSlice;
            float endAngle = (i + 1) * anglePerSlice;

            // Convert to radians
            float startRad = startAngle * Mathf.Deg2Rad;
            float endRad = endAngle * Mathf.Deg2Rad;

             // Create vertices for the slice (triangle fan with center + 2 edge points)
            UIVertex[] vertices = new UIVertex[3];
            
            // Center point (vertex 0)
            vertices[0] = new UIVertex();
            vertices[0].position = Vector3.zero;
            vertices[0].color = color;
            
            // First edge point (vertex 1)
            vertices[1] = new UIVertex();
            vertices[1].position = new Vector3(Mathf.Cos(startRad), Mathf.Sin(startRad), 0) * SliceLenght;
            vertices[1].color = color;
            
            // Second edge point (vertex 2)
            vertices[2] = new UIVertex();
            vertices[2].position = new Vector3(Mathf.Cos(endRad), Mathf.Sin(endRad), 0) * SliceLenght;
            vertices[2].color = color;

             // Calculate UV coordinates based on tile grid
            Vector2 textureSize = new Vector2(1f / TileNumbers.x, 1f / TileNumbers.y);
            Vector2 uvMin = new Vector2(tile.x * textureSize.x, tile.y * textureSize.y);
            Vector2 uvMax = new Vector2((tile.x + 1) * textureSize.x, (tile.y + 1) * textureSize.y);

            // Assign UVs - map the slice to the appropriate tile region
            // Center UV
            vertices[0].uv0 = new Vector2((uvMin.x + uvMax.x) * 0.5f, (uvMin.y + uvMax.y) * 0.5f);

            // Edge UVs - map to the edges of the tile while maintaining the circular shape
            Vector2 uvCenter = new Vector2(0.5f, 0.5f);
            Vector2 toStart = new Vector2(Mathf.Cos(startRad), Mathf.Sin(startRad));
            Vector2 toEnd = new Vector2(Mathf.Cos(endRad), Mathf.Sin(endRad));
            
            vertices[1].uv0 = uvCenter + toStart * 0.5f;
            vertices[2].uv0 = uvCenter + toEnd * 0.5f;

            // Remap from unit circle to tile UV space
            vertices[0].uv0 = new Vector2(
                Mathf.Lerp(uvMin.x, uvMax.x, vertices[0].uv0.x),
                Mathf.Lerp(uvMin.y, uvMax.y, vertices[0].uv0.y)
            );
            vertices[1].uv0 = new Vector2(
                Mathf.Lerp(uvMin.x, uvMax.x, vertices[1].uv0.x),
                Mathf.Lerp(uvMin.y, uvMax.y, vertices[1].uv0.y)
            );
            vertices[2].uv0 = new Vector2(
                Mathf.Lerp(uvMin.x, uvMax.x, vertices[2].uv0.x),
                Mathf.Lerp(uvMin.y, uvMax.y, vertices[2].uv0.y)
            );

            // Calculate offset direction (middle of the slice)
            float midAngle = (startAngle + endAngle) * 0.5f * Mathf.Deg2Rad;
            Vector2 offsetDirection = new Vector2(Mathf.Cos(midAngle), Mathf.Sin(midAngle));
            Vector2 offset = offsetDirection * SliceOffsetPx;

            // Apply position offset
            vertices[0].position += (Vector3)offset;
            vertices[1].position += (Vector3)offset;
            vertices[2].position += (Vector3)offset;

            // Apply position real offset
            vertices[0].position += off;
            vertices[1].position += off;
            vertices[2].position += off;

            // Add vertices and triangle to the mesh
            int currentVertCount = vh.currentVertCount;
            vh.AddVert(vertices[0]);
            vh.AddVert(vertices[1]);
            vh.AddVert(vertices[2]);
            
            vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
        }

        public void DrawFrame ( VertexHelper vh ) {
            DrawTexture ( vh, FrameTile, FrameSize, Color.white );
        }

        public void DrawCover ( VertexHelper vh ) {
            DrawTexture ( vh, CoverTile, FrameSize, Color.white );
        }
    }
}*/
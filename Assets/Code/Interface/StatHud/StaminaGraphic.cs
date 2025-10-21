using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{/*
    public class StaminaGraphic : HudGraphic {

        public static void DrawStamina ( VertexHelper vh, float angle, float distance, Color color, float size ) {
            // center position
            Vector2 center = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
                Mathf.Sin(angle * Mathf.Deg2Rad) * distance
            );

            // half size
            float halfSize = size / 2f;

            // order of vertices : Bottom left - Bottom right - Top right - Top left
            Vector2 [] vertices = new Vector2 [4];
            vertices[0] = new Vector2(center.x - halfSize, center.y - halfSize);
            vertices[1] = new Vector2(center.x + halfSize, center.y - halfSize);
            vertices[2] = new Vector2(center.x + halfSize, center.y + halfSize);
            vertices[3] = new Vector2(center.x - halfSize, center.y + halfSize);

            // UV coordinates for full texture (0,0 to 1,1)
            Vector2[] uvs = new Vector2[4];
            uvs[0] = new Vector2(0f, 0f); // Bottom left
            uvs[1] = new Vector2(1f, 0f); // Bottom right
            uvs[2] = new Vector2(1f, 1f); // Top right
            uvs[3] = new Vector2(0f, 1f); // Top left

            // get current vertices
            int currentVertexCount = vh.currentVertCount;

            // Add vertices
            for (int i = 0; i < 4; i++)
                vh.AddVert(vertices[i], color, uvs[i]);
            
            // Add triangles
            vh.AddTriangle(currentVertexCount, currentVertexCount + 1, currentVertexCount + 2);
            vh.AddTriangle(currentVertexCount + 2, currentVertexCount + 3, currentVertexCount);
        }
    }*/
}
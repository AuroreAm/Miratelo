using Lyra;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code
{
    public class HeartGraphic : HudGraphic {
        public static void DrawHeartQuarter ( VertexHelper vh, int quarterId, Color color, float x, float size ) {
            // get center of heart
            float centerX = x + size / 2f;

            // Quarter dimension, each quarter is half size in both direction
            float h = size / 2f;

            // vertices
            Vector2 [] vertices = new Vector2 [4];
            Vector2 [] uvs = new Vector2 [4];
            // order of vertices : Bottom left - Bottom right - Top right - Top left

            // determine the quarter for drawing
            switch (quarterId) {
                    case 0: // top left

                    vertices [0] = new Vector2 ( x, 0 );
                    vertices [1] = new Vector2 ( centerX, 0 );
                    vertices [2] = new Vector2 ( centerX, h );
                    vertices [3] = new Vector2 ( x,  h );

                    uvs[0] = new Vector2(0f, 0.5f);
                    uvs[1] = new Vector2(0.5f, 0.5f);
                    uvs[2] = new Vector2(0.5f, 1f);
                    uvs[3] = new Vector2(0f, 1f);

                    break;

                    case 1: // bottom left

                    vertices[0] = new Vector2(x, -h);
                    vertices[1] = new Vector2(centerX, -h);
                    vertices[2] = new Vector2(centerX, 0 ); 
                    vertices[3] = new Vector2(x, 0 ); 
                    
                    uvs[0] = new Vector2(0f, 0f);  
                    uvs[1] = new Vector2(0.5f, 0f);
                    uvs[2] = new Vector2(0.5f, 0.5f);
                    uvs[3] = new Vector2(0f, 0.5f);

                    break;

                    case 2: // Bottom Right
                    vertices[0] = new Vector2(centerX, -h);
                    vertices[1] = new Vector2(x + size, -h);
                    vertices[2] = new Vector2(x + size, 0);
                    vertices[3] = new Vector2(centerX, 0);
                    
                    uvs[0] = new Vector2(0.5f, 0f);
                    uvs[1] = new Vector2(1f, 0f);
                    uvs[2] = new Vector2(1f, 0.5f);
                    uvs[3] = new Vector2(0.5f, 0.5f);

                    break;

                    case 3: // Top Right
                    vertices[0] = new Vector2(centerX, 0);
                    vertices[1] = new Vector2(x + size, 0);
                    vertices[2] = new Vector2(x + size, h);
                    vertices[3] = new Vector2(centerX, h);
                    
                    uvs[0] = new Vector2(0.5f, 0.5f);
                    uvs[1] = new Vector2(1f, 0.5f);
                    uvs[2] = new Vector2(1f, 1f);
                    uvs[3] = new Vector2(0.5f, 1f);

                    break;
            }

            // get current vertices
            int currentVertexCount = vh.currentVertCount;

            // add vertices
            for (int i = 0; i < 4; i++)
                vh.AddVert(vertices[i], color, uvs[i]);
            
            // add triangles
            vh.AddTriangle(currentVertexCount, currentVertexCount + 1, currentVertexCount + 2);
            vh.AddTriangle(currentVertexCount + 2, currentVertexCount + 3, currentVertexCount);
        }
    }
}
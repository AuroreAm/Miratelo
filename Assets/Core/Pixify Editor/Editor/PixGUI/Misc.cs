using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Editor
{

    namespace PixGUI
    {
        public class IMGUI : Element
        {
            public Action <Vector2> GUI;

            public IMGUI ( Action<Vector2> gui )
            {
                GUI = gui;
            }

            public override void Draw()
            {
                GUILayout.BeginArea ( Transform );
                GUI (Transform.size);
                GUILayout.EndArea();
            }
        }
    }

}
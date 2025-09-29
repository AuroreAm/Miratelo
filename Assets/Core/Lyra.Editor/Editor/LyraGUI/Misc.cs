using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra.Editor
{

    namespace GUI
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

        public class Scroll : Element
        {
            Element Content;
            public Vector2 spos;

            public Scroll ( Element content )
            {
                Content = content;
            }

            override sealed public void ResetRect ()
            { base.ResetRect (); Content.ResetRect (); }

            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                Vector2 size = base.GetInitSize(ParentSize, ParentDefTransform);
                Content.InitRect(new Vector2(size.x-16, size.y),DefTransform);
                return size;
            }

            public override void Draw()
            {
                GUILayout.BeginArea ( Transform );
                spos = UnityEngine.GUI.BeginScrollView (Transform, spos, Content.Transform );
                Content.Draw ();
                UnityEngine.GUI.EndScrollView ();
                GUILayout.EndArea();
            }
        }

    }

}
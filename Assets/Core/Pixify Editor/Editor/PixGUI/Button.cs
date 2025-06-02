using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Editor
{
    namespace PixGUI
    {
        public class Button : Element
        {
            public Element Content;
            public Action OnClick;

            
            override sealed public void ResetRect ()
            { base.ResetRect (); Content.ResetRect (); }

            public Button ( Action onClick )
            {
                Content = new Area();
                OnClick = onClick;
            }

            public Button ( Element content, Action onClick )
            {
                Content = content;
                OnClick = onClick;
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                Vector2 size = base.GetInitSize(ParentSize, ParentDefTransform);
                Content.InitRect(size, DefTransform);
                return size;
            }

            public override void Draw()
            {
                GUILayout.BeginArea ( Transform );
                Content.Draw();
                GUILayout.EndArea();
                
                if (GUI.Button(Transform, GUIContent.none, GUIStyle.none))
                OnClick?.Invoke();
            }
        }
    }
}

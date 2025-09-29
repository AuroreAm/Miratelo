using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace Lyra.Editor
{
    namespace GUI
    {
        public class Toggle : Element
        {
            public bool state
            {get { return _state; } set { 
                if (value == _state) return;
                _state = value; if (value) OnEnable?.Invoke(); else OnDisable?.Invoke(); 
                }}
            bool _state;

            public Element EnabledArea;
            public Element DisabledArea;
            public Action OnEnable;
            public Action OnDisable;

            
            override sealed public void ResetRect ()
            { base.ResetRect (); EnabledArea.ResetRect (); DisabledArea.ResetRect (); }

            public Toggle(Element EnabledArea, Element DisabledArea)
            {
                this.EnabledArea = EnabledArea;
                this.DisabledArea = DisabledArea;
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                Vector2 size = base.GetInitSize(ParentSize, ParentDefTransform);
                EnabledArea.InitRect(size, DefTransform);
                DisabledArea.InitRect(size, DefTransform);
                return size;
            }

            public override void Draw()
            {
                GUILayout.BeginArea ( Transform );

                if (state == true)
                    EnabledArea.Draw();
                else
                    DisabledArea.Draw();
                    
                GUILayout.EndArea();
                
                if (UnityEngine.GUI.Button(Transform, GUIContent.none, GUIStyle.none))
                    state = !state;
            }
        }

        public class SelectionRow : Element
        {
            public int Column = 1;
            public int selected { get; private set; } = -1;

            List<Toggle> Toggles = new List<Toggle>();
            public int TogglesCount => Toggles.Count;

            public void Add (params Toggle[] toggle)
            {
                int currentToggleLength = Toggles.Count;
                for (int i = 0; i < toggle.Length; i++)
                {
                    Toggles.Add (toggle[i]);
                    var index = i + currentToggleLength;
                    toggle[i].OnEnable += () => Select (index);
                }
            }

            public Toggle this[int index]
            {
                get { return Toggles[index]; }
            }

            public void Select (int index)
            {
                this.selected = index;
                for (int i = 0; i < Toggles.Count; i++)
                {
                    Toggles[i].state = i == index;
                }
            }

            protected override Vector2 GetInitPos(Vector2 ParentSize)
            {
                return base.GetInitPos ( ParentSize );
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                Vector2 size = base.GetInitSize(ParentSize, ParentDefTransform);
                int defRow = (int) Mathf.Ceil (Toggles.Count / (float) Column);

                for (float i = 0, row = 0, column = 0; i < Toggles.Count; i++, column++)
                {
                    if (column >= Column)
                    {
                        column = 0;
                        row++;
                    }

                    Toggles[(int) i].DefTransform = new DefTransform()
                    {
                        RelativeTransform = new Rect( column / (float) Column, row / (float) defRow, 1 / (float) Column, 1 / (float) defRow),
                        Position = new Vector2(0, 0),
                        Size = new Vector2(0, 0)
                    };
                    Toggles[(int) i].InitRect(size, DefTransform);
                }

                return size;
            }
            

            public override void Draw()
            {
                GUILayout.BeginArea ( Transform );
                int ToggleCount = Toggles.Count;
                for (int i = 0; i < ToggleCount; i++)
                {
                    if (Toggles [i].on)
                    Toggles [i].Draw ();

                    if (ToggleCount != Toggles.Count)
                        break;
                }
                GUILayout.EndArea();
            }
        }

    }
}
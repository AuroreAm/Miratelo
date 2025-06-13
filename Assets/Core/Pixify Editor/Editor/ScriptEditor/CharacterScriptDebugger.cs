using System.Reflection;
using System.Collections.Generic;
using Pixify.Editor.PixGUI;
using UnityEditor;
using UnityEngine;
using static Pixify.Editor.ScriptEditorStyles;

namespace Pixify.Editor
{
    class actionDebuggerBase : AreaAutoHeight
    {
        public action Debugged { get; private set; }
        protected Label label;
        protected string Description;

        public actionDebuggerBase(action a)
        {
            Debugged = a;
            label = new Label("", o.TextMiddleLeftX);
        }

        override public void Draw()
        {
            string hexNodeTintColor = ColorUtility.ToHtmlStringRGB(Debugged.GetType().GetCustomAttribute<NodeTintAttribute>().Tint);

            label.text = string.Concat($"<color=#{hexNodeTintColor}>{Debugged.GetType().Name}</color> -", Debugged.Tag, "-", $"<color=#aaaaaa> {Description} </color>");

            if ( Debugged.on == true )
            EditorGUI.DrawRect(Transform, new Color (0,0.7f,0));
            else
            EditorGUI.DrawRect(Transform, new Color (0.7f,0,0));

            base.Draw();
        }

        public static actionDebuggerBase CreateDebugger(action a)
        {
            if (a is decorator d)
                return new decoratorDebugger(d);
            else
                return new actionDebugger(a);
        }
    }

    class actionDebugger : actionDebuggerBase
    {
        public actionDebugger(action a) : base(a)
        {
            Add(
                label.Size(new Vector2(0, 16)).RelativeTransform(new Rect(0, 0, 1, 0))
            );
        }
    }

    class decoratorDebugger : actionDebuggerBase
    {
        public decorator Special { get; private set; }
        public Area SubContent { get; private set; }

        public decoratorDebugger(decorator d) : base(d)
        {
            SubContent = new AreaList();
            SubContent.Padding(new Vector4(8, 40, 8, 8));
            Special = d;

            for (int i = 0; i < d.o.Length; i++)
                SubContent.Add(CreateDebugger(d.o[i]));

            CreateVisual();
        }

        void CreateVisual()
        {
            Color BorderColor = Special.GetType().GetCustomAttribute<NodeTintAttribute>().Tint;

            Add(
                new Area(
                        new Area(label.Padding(new Vector4(2, 2, 2, 2)))

                        ).RelativeTransform(new Rect(0, 0, 1, 0)).Size(new Vector2(0, 32)),

                new Box() { ColorA = Color.clear, ColorB = BorderColor, BorderWidth = 2 },
                SubContent
                );
        }
    }
}
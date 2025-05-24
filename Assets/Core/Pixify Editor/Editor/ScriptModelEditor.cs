using System.Reflection;
using System;
using UnityEngine;
using XNodeEditor;
using Pixify;
using UnityEditor;

namespace Pixify.Editor
{
    [CustomNodeGraphEditor (typeof (ScriptModel))]
    public class ScriptModelEditor : NodeGraphEditor
    {
        public static ScriptModelEditor o;
        public ScriptModel scriptModel;
        public Cursor <action> ActionCursor;
        Vector2 CursorPosition;

        public override void OnOpen()
        {
            o = this;
            scriptModel = target as ScriptModel;

            if (scriptModel.Root == null)
                scriptModel.Root = CreateNode(typeof(ScriptRoot), new Vector2(0, 0)) as ScriptRoot;

            NodeEditorWindow.current.panOffset = scriptModel.pan;

            ActionCursor = new Cursor<action>(AddActionModel);
            CursorPosition = window.position.size;

            EditorUtility.SetDirty(scriptModel);
            base.OnOpen ();
        }

        public override void OnGUI()
        {
            if ( o == null ) OnOpen ();
            scriptModel.pan = NodeEditorWindow.current.panOffset;

            // cursor event position
            var e = Event.current;

            if (e.alt && e.type == EventType.MouseUp)
            {
                CursorPosition = e.mousePosition;
                ActionCursor.Search.SetFocus ();
            }

            DrawActionCursor();
        }

        void DrawActionCursor()
        {
            GUILayout.BeginArea(new Rect(CursorPosition, new Vector2(200, 200)));
            ActionCursor.GUI();
            GUILayout.EndArea();
        }

        void AddActionModel (Type t)
        {
            Type model = typeof ( ActionModel );
            if ( t.IsSubclassOf ( typeof ( decorator ) ) )
            model = typeof ( DecoratorModel );

            var newActionModel = CreateNode ( model, NodeEditorWindow.current.WindowToGridPosition(CursorPosition) ) as ActionModel;
            newActionModel.BluePrintPaper.Set (t);

            EditorUtility.SetDirty(scriptModel);
            CursorPosition += new Vector2(250, 0);
        }
    }
}
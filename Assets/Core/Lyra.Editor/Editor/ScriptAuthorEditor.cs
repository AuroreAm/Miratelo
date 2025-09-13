using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

namespace Lyra.Editor
{
    public class ScriptDock : EditorWindow
    {
        [MenuItem ("GameObject/Script")]
        static void CreateActionPaper ()
        {
            var a = new GameObject ("---");
            a.AddComponent <ScriptAuthor> ();

            if (Selection.activeGameObject)
            a.transform.SetParent (Selection.activeGameObject.transform);

            Selection.activeGameObject = a;
            Undo.RegisterCreatedObjectUndo (a, "Create Script");
        }

        [MenuItem ("GameObject/Index")]
        static void CreateIndexPaper ()
        {
            var a = new GameObject ("---");
            a.AddComponent <IndexPaper> ();

            if (Selection.activeGameObject)
            a.transform.SetParent (Selection.activeGameObject.transform);

            Selection.activeGameObject = a;
            Undo.RegisterCreatedObjectUndo (a, "Create Index");
        }

        [MenuItem ("Window/Script Dock")]
        public static void ShowWindow ()
        {
            EditorWindow.GetWindow(typeof(ScriptDock));
        }

        void OnEnable ()
        {
            minSize = new Vector2 (0, 32);
        }

        void OnGUI ()
        {
            var go = Selection.activeGameObject;

            bool can = true;

            if (!go)
            can = false;

            if ( go && !( go.GetComponent <IndexPaper> () || go.GetComponent <ActionPaper> () ) )
            can = false;

            GUILayout.BeginHorizontal ();

            if (GUILayout.Button ("+",GUILayout.Width (64)) && can)
                ActionCursor.Show ( go );

            if ( GUILayout.Button ("New Index") )
                CreateIndexPaper ();

            if (GUILayout.Button ("-",GUILayout.Width (32)) && can)
                Undo.DestroyObjectImmediate ( go );

            GUILayout.EndHorizontal ();
        }
    }
}

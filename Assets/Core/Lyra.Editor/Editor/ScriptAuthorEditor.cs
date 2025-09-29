using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;

namespace Lyra.Editor
{
    public class ScriptAuthorEditor : EditorWindow
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

        [MenuItem ("Window/Lyra/Script Dock")]
        public static void ShowWindow ()
        {
            GetWindow(typeof(ScriptAuthorEditor));
        }

        void OnEnable ()
        {
            minSize = new Vector2 (0, 32);
            titleContent = new GUIContent ("Script");
        }

        void OnGUI ()
        {
            var go = Selection.activeGameObject;

            bool can = true;

            if (!go)
            can = false;

            if ( go && !( go.GetComponent<ScriptAuthor> () || go.GetComponent <ActionPaper> ()) )
            can = false;

            GUILayout.BeginHorizontal ();

            if (GUILayout.Button ("+",GUILayout.Width (64)) && can)
                ActionCursor.Show ( go );

            if (GUILayout.Button ("-",GUILayout.Width (32)) && can)
                Undo.DestroyObjectImmediate ( go );

            GUILayout.EndHorizontal ();
        }
    }

    [InitializeOnLoad]
    class ScriptHierarchyColors
    {
        static ScriptHierarchyColors ()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        static Dictionary <type_paper, Type> TypeCache = new Dictionary <type_paper, Type> ();

        static void OnHierarchyWindowItemOnGUI ( int instanceID, Rect selectionRect )
        {
            var go = EditorUtility.InstanceIDToObject ( instanceID ) as GameObject;

            if ( go == null ) return;

            ActionPaper actionPaper = go.GetComponent <ActionPaper> ();
            if ( actionPaper )
            {
                EditorGUI.DrawRect ( selectionRect, new Color (.5f,.5f,.6f) );
                if (  actionPaper.IsDecoratorKind () )
                EditorGUI.DrawRect ( selectionRect, new Color (.7f,.5f,.6f) );

                string Label = "---";
                if (TypeCache.ContainsKey (actionPaper.Paper.type))
                    Label = TypeCache[actionPaper.Paper.type].Name;
                else if ( actionPaper.Paper.type.valid () )
                {
                    Type t = actionPaper.Paper.type.write ();
                    Label =  t.Name;
                    TypeCache.Add (actionPaper.Paper.type, t);
                }
                Label += $"({actionPaper.gameObject.name})";

                EditorGUI.LabelField ( selectionRect, Label, Styles.o.Action );
            }

            if ( go.GetComponent <ScriptAuthor> () )
            EditorGUI.DrawRect ( selectionRect, new Color (.7f,.4f,.4f,.4f) );
        }
    }
}

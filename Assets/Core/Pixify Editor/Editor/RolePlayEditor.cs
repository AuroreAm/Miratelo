using System;
using Pixify.Spirit;
using UnityEditor;
using UnityEngine;

namespace Pixify.Editor
{

    public class ThoughtPaperCursor : EditorWindow
    {
        Cursor <thought.package> cursor;
        GameObject target;

        void OnGUI ()
        {
            SpecialThoughtGUI ();
            CursorGUI ();
        }

        public static void Init ( GameObject Go )
        {
            GetWindow <ThoughtPaperCursor> ().target = Go;
        }

        void SpecialThoughtGUI ()
        {
            if ( GUILayout.Button ("Flow") )
            Add <FlowPaper> ();
            
            if ( GUILayout.Button ("Pointer") )
            Add <PointerPaper> ();

            if (GUILayout.Button ("Reflexion"))
            Add <ReactionPaper> ();

            if (GUILayout.Button ("Condition"))
            Add <ConditionPaper> ();

            GUILayout.Label ("------");

            void Add <T> () where T : ThoughtAuthor
            {
                var a = new GameObject ().AddComponent <T> ();
                a.name = typeof (T).Name;
                a.transform.SetParent ( target.transform );

                Selection.activeGameObject = a.gameObject;

                Close ();
            }
        }

        void CursorGUI ()
        {
            if (cursor == null)
            cursor = new Cursor<thought.package> ( AddThought );

            cursor.GUI ();
        }

        void AddThought ( Type t )
        {
            var a = new GameObject ().AddComponent <ThoughtPaper> ();

            a.paper.StrNodeType = t.AssemblyQualifiedName;
            a.paper.StrNodeData = JsonUtility.ToJson ( Activator.CreateInstance ( t ));
            a.name = t.Name;

            a.transform.SetParent ( target.transform );
            EditorUtility.SetDirty ( a );

            Selection.activeGameObject = a.gameObject;

            Close ();
        }

        void OnLostFocus ()
        {
            Close ();
        }
    }

    [InitializeOnLoad]
    class ThoughtPaperHierarchyIcon
    {
        static ThoughtPaperHierarchyIcon()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            
            if ( obj == null ) return;

            if ( obj.GetComponent <RolePlay> () || obj.GetComponent <FlowPaper> () || obj.GetComponent <PointerPaper> () || obj.GetComponent <ReactionPaper> () || obj.GetComponent <ConditionPaper> () )
            {
                Rect r = new Rect(new Vector2(selectionRect.x + selectionRect.width - 32, selectionRect.y), new Vector2(38, selectionRect.height));

                GUI.backgroundColor = Color.green;
                if (GUI.Button (r, "+"))
                ThoughtPaperCursor.Init ( obj );
                GUI.backgroundColor = Color.white;
            }
        }
    }

}
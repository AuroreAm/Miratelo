using System;
using UnityEditor;
using Pixify.Spirit;
using UnityEngine;

namespace Pixify.Editor
{

    public class ActionPaperCursor : EditorWindow
    {
        Cursor <action> cursor;
        GameObject target;

        void OnGUI ()
        {
            CursorGUI ();
        }
        
        public static void Init ( GameObject Go )
        {
            GetWindow <ActionPaperCursor> ().target = Go;
        }

        void CursorGUI()
        {
            if (cursor == null)
            cursor = new Cursor<action> ( AddAction );

            cursor.GUI ();
        }

        void AddAction (Type t)
        {
            var a = new GameObject ().AddComponent <ActionPaper> ();

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
    class ActionPaperHierarchyIcon
    {
        static ActionPaperHierarchyIcon()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
        }

        static void OnHierarchyWindowItemOnGUI (int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            
            if ( obj == null ) return;

            if ( obj.GetComponent <ActionPaper> () && obj.GetComponent <ActionPaper> ().IsDecorator () )
            {
                Rect r = new Rect(new Vector2(selectionRect.x + selectionRect.width - 32, selectionRect.y), new Vector2(38, selectionRect.height));

                GUI.backgroundColor = Color.red;
                if (GUI.Button (r, "+"))
                ActionPaperCursor.Init ( obj );
                GUI.backgroundColor = Color.white;
            }
        }
    }

    static class ActionPaperExtension
    {
        public static bool IsDecorator ( this ActionPaper paper )
        {
            return !string.IsNullOrEmpty ( paper.paper.StrNodeType ) && Type.GetType ( paper.paper.StrNodeType ) != null &&  Type.GetType (paper.paper.StrNodeType).IsSubclassOf (typeof (decorator) );
        }
    }
}

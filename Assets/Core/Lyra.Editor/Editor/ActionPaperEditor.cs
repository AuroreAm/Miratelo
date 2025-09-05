using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

namespace Lyra.Editor
{
    [CustomEditor (typeof(ActionPaper),true)]
    public class ActionPaperEditor : UnityEditor.Editor
    {
        ActionPaper _target;
        MoonPaperEditor _editor;

        const string PaperField = "Paper";

        void OnEnable ()
        {
            _target = target as ActionPaper;
            _editor = ScriptableObject.CreateInstance <MoonPaperEditor> ();

            var paper = serializedObject.FindProperty ( PaperField );
            _editor.Load ( paper , _target.GetType ().GetField ( PaperField, BindingFlags.Instance | BindingFlags.Public ) );
        }

        public override void OnInspectorGUI()
        {
            _editor.OnGUI ();
        }

        [MenuItem ("GameObject/Action")]
        static void CreateActionPaper ()
        {
            ActionCursor.Show ( Selection.activeGameObject );
        }
    }

    [InitializeOnLoad]
    class ScriptHierarchyIcon
    {
        static ScriptHierarchyIcon ()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchy;
        }

        static void OnHierarchy ( int instanceId, Rect selectionRect )
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId) as GameObject;

            if ( obj == null ) return;

            if ( obj.GetComponent <ActionPaper> () && obj.GetComponent <ActionPaper> ().Paper.type.valid () && obj.GetComponent <ActionPaper> ().Paper.type.write ().IsSubclassOf (typeof(decorator)) )
            {
                Rect r = new Rect(new Vector2(selectionRect.x + selectionRect.width - 32, selectionRect.y), new Vector2(38, selectionRect.height));

                GUI.backgroundColor = Color.green;
                if (GUI.Button (r, "+"))
                ActionCursor.Show ( obj );
                GUI.backgroundColor = Color.white;

                return;
            }
        }
    }
}
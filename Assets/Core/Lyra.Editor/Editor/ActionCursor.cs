using UnityEngine;
using UnityEditor;
using System;

namespace Lyra.Editor
{
    public class ActionCursor : EditorWindow
    {
        CursorGUI < action > _cursor;
        GameObject _target;

        public static void Show ( GameObject target )
        {
            GetWindow < ActionCursor > ()._target = target;
        }

        void OnGUI ()
        {
            if (_cursor == null)
            _cursor = new CursorGUI < action > ( AddPackage );

            _cursor.GUI ();
        }

        // add actionpaper to the selected target
        void AddPackage (  Type t )
        {
            ActionPaper a;
            a = new GameObject ().AddComponent < ActionPaper > ();

            a.Paper.type.content = t.AssemblyQualifiedName;
            a.Paper.data = JsonUtility.ToJson ( Activator.CreateInstance ( t ));

            if ( _target )
            {
                if ( HierarchyFoldoutUtility.IsExpanded ( _target ) || _target.GetComponent <IndexPaper> () || GOIsDecoratorWith0Childs ( _target ) )
                {
                    a.transform.SetParent ( _target.transform );
                    a.transform.SetAsFirstSibling ();
                }
                else if ( _target.transform.parent )
                {
                    a.transform.SetParent ( _target.transform.parent );
                    if ( _target.transform.GetSiblingIndex () < _target.transform.parent.childCount - 1 )
                    a.transform.SetSiblingIndex ( _target.transform.GetSiblingIndex () +  1 );
                }
            }

            a.gameObject.name = t.Name;

            EditorUtility.SetDirty (a);
            Selection.activeGameObject = a.gameObject;

            Undo.RegisterCreatedObjectUndo (a.gameObject, "Create Action");
            Close ();
        }

        public static bool GOIsDecoratorWith0Childs ( GameObject go )
        {
            return
            go.GetComponent <ActionPaper> () &&
            go.GetComponent <ActionPaper> ().IsDecorator () &&
            go.transform.childCount == 0;
        }

        void OnLostFocus ()
        {
            Close ();
        }
    }
}
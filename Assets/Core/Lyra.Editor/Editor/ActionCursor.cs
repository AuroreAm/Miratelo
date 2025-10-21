using UnityEngine;
using UnityEditor;
using System;

namespace Lyra.Editor
{
    public class ActionCursor : EditorWindow
    {
        cursorgui < action > _cursor;
        GameObject _target;

        public static void Show ( GameObject target )
        {
            GetWindow < ActionCursor > ()._target = target;
        }

        void OnGUI ()
        {
            if (_cursor == null)
            _cursor = new cursorgui < action > ( AddPackage );

            _cursor.GUI ();
        }

        // add actionpaper to the selected target
        void AddPackage (  Type t )
        {
            ActionPaperBase a;
            a = new GameObject ().AddComponent < ActionPaperBase > ();
            a.name = "-";

            a.Paper = new moon_paper<action> ( t );

            if ( _target )
            {
                if ( HierarchyFoldoutUtility.IsExpanded ( _target ) || GOIsDecoratorWith0Childs ( _target ) )
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
            
            EditorUtility.SetDirty (a);
            Selection.activeGameObject = a.gameObject;

            Undo.RegisterCreatedObjectUndo (a.gameObject, "Create Action");
            Close ();
        }

        public static bool GOIsDecoratorWith0Childs ( GameObject go )
        {
            return
            go.GetComponent <ActionPaper> () &&
            go.GetComponent <ActionPaper> ().IsDecoratorKind () &&
            go.transform.childCount == 0;
        }

        void OnLostFocus ()
        {
            Close ();
        }
    }
}
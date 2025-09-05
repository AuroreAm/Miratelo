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

        void AddPackage (  Type t )
        {
            ActionPaper a;
            a = new GameObject ().AddComponent < ActionPaper > ();

            a.Paper.type.content = t.AssemblyQualifiedName;
            a.Paper.data = JsonUtility.ToJson ( Activator.CreateInstance ( t ));

            if ( _target )
            a.transform.SetParent ( _target.transform );
            a.gameObject.name = t.Name;
            
            EditorUtility.SetDirty (a);
            Selection.activeGameObject = a.gameObject;
            Close ();
        }

        void OnLostFocus ()
        {
            Close ();
        }
    }
}
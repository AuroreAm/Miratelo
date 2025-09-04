using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Lyra.Editor
{
    public class TypeSelectionWindow : EditorWindow
    {
        SerializedProperty _target;
        CursorGUI _cursor;

        public static void Show ( SerializedProperty target )
        {
            GetWindow <TypeSelectionWindow> ().Load ( target );
        }

        void Load ( SerializedProperty target )
        {
            _target = target;
        }

        void OnEnable ()
        {
            _cursor = new CursorGUI ( typeof (dat) , OnSelect );
        }

        void OnGUI ()
        {
            _cursor.GUI ();
        }

        void OnSelect ( Type t )
        {
            _target.FindPropertyRelative ("Content").stringValue = t.AssemblyQualifiedName;
            _target.serializedObject.ApplyModifiedProperties ();
            Close ();
        }

        void OnLostFocus ()
        {
            Close ();
        }
    }
}

using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

namespace Lyra.Editor
{
    public class MoonPaperEditor : EditorWindow
    {
        public string TypeName { private set; get; }

        moon_paper <moon> target;
        FieldInfo fi;
        SerializedProperty property;

        cursorgui cursor;
        bool isWindow;

        moon_editor dE;

        public static void Show ( SerializedProperty target, FieldInfo targetFi ) {
            GetWindow <MoonPaperEditor> ().Load (target,targetFi);
            GetWindow <MoonPaperEditor> ().isWindow = true;
        }

        public void Load ( SerializedProperty _property, FieldInfo _fi ) {
            property = _property;
            fi = _fi;
            target = new moon_paper<moon> ( _property.FindPropertyRelative ("data").stringValue, _property.FindPropertyRelative ("type").stringValue );
        }

        public void OnGUI ()
        {
            MoonSelectionGUI ();
            MoonEditorGUI ();
        }

        void MoonSelectionGUI ()
        {
            if ( target.valid () ) return;

            if (cursor == null)
            {
                if (fi.FieldType.IsArray)
                cursor = new cursorgui ( fi.FieldType.GetElementType ().GetGenericArguments ()[0], SetDat );
                else
                cursor = new cursorgui ( fi.FieldType.GetGenericArguments ()[0], SetDat );

                if (isWindow) cursor.focus ();
            }

            cursor.GUI ();

            void SetDat (Type t) {
                target = new moon_paper<moon> (t);
            }
        }

        void MoonEditorGUI ()
        {
            if ( !target.valid () ) return;

            if (dE == null)
            dE = moon_editor.create_editor ( target.paper );

            dE._gui ();

            if ( isWindow && GUILayout.Button ("Save")) {
                Save ();
                Close ();
            }

            if (!isWindow) {
                Save ();
            }

            if (GUILayout.Button ("Break")) {
                target = new moon_paper<moon> ();
            }

            void Save () {
                target.stream_to_SerializedProperty ( property );
            }
        }
    }

    
    [CustomPropertyDrawer( typeof (moon_paper<>) )]
    public class moon_paperDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;

            UnityEngine.GUI.Label ( position, property.FindPropertyRelative ("type").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;
            UnityEngine.GUI.Label ( position, property.FindPropertyRelative ("data").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;

            if (UnityEngine.GUI.Button (position, "Edit") )
                MoonPaperEditor.Show ( property, fieldInfo);

            EditorGUI.EndProperty ();
        }
    }
}

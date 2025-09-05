using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

namespace Lyra.Editor
{
    public class MoonPaperEditor : EditorWindow
    {
        SerializedProperty target;
        SerializedProperty type;
        SerializedProperty data;
        FieldInfo field;

        moon paper;
        moon_editor dE;
        CursorGUI cursor;

        bool isWindow;

        public static void Show ( SerializedProperty target, FieldInfo targetFi )
        {
            GetWindow <MoonPaperEditor> ().Load (target,targetFi);
            GetWindow <MoonPaperEditor> ().isWindow = true;
        }

        public void Load ( SerializedProperty target, FieldInfo targetFi )
        {
            this.target = target;
            field = targetFi;

            type = target.FindPropertyRelative ("type").FindPropertyRelative("content");
            type_paper t = new type_paper ( type.stringValue );

            data = target.FindPropertyRelative ("data");

            if ( t.valid () )
            {
                paper = (moon) Activator.CreateInstance ( t.write() );
                JsonUtility.FromJsonOverwrite ( data.stringValue, paper );
            }
        }

        public void OnGUI ()
        {
            MoonSelectionGUI ();
            MoonEditorGUI ();
        }

        void MoonSelectionGUI ()
        {
            if (paper!=null) return;

            if (cursor == null)
            {
                if (field.FieldType.IsArray)
                cursor = new CursorGUI ( field.FieldType.GetElementType ().GetGenericArguments ()[0], SetDat );
                else
                cursor = new CursorGUI ( field.FieldType.GetGenericArguments ()[0], SetDat );
            }

            cursor.GUI ();

            void SetDat (Type t)
            {
                paper = (moon) Activator.CreateInstance ( t );
            }
        }

        void MoonEditorGUI ()
        {
            if (paper == null) return;

            if (dE == null)
            dE = moon_editor.create_editor ( paper );

            dE._gui ();

            if ( isWindow && GUILayout.Button ("Save"))
            {
                Save ();
                Close ();
            }

            if (!isWindow)
            {
                Save ();
            }

            if (GUILayout.Button ("Break"))
            {
                type.stringValue = "";
                data.stringValue = "";
                target.serializedObject.ApplyModifiedProperties ();
                cursor = null;
                paper = null;
                if (isWindow)
                Close ();
            }

            void Save ()
            {
                type.stringValue = paper.GetType ().AssemblyQualifiedName;
                data.stringValue = JsonUtility.ToJson (paper);
                target.serializedObject.ApplyModifiedProperties ();
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

            GUI.Label ( position, property.FindPropertyRelative ("type").FindPropertyRelative("Content").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;
            GUI.Label ( position, property.FindPropertyRelative ("data").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;

            if ( GUI.Button (position,"Edit") )
            MoonPaperEditor.Show ( property, fieldInfo );

            EditorGUI.EndProperty ();
        }
    }
}

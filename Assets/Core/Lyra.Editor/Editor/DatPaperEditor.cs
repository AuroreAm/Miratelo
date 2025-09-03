using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

namespace Lyra.Editor
{
    public class DatPaperEditor : EditorWindow
    {
        SerializedProperty _target;
        SerializedProperty _typeContent;
        SerializedProperty _data;
        FieldInfo _targetMeta;

        dat _paper;
        DatEditor dE;
        CursorGUI _cursor;

        bool IsWindow;

        public static void Show ( SerializedProperty target, FieldInfo targetFi )
        {
            GetWindow <DatPaperEditor> ().Load (target,targetFi);
            GetWindow <DatPaperEditor> ().IsWindow = true;
        }

        public void Load ( SerializedProperty target, FieldInfo targetFi )
        {
            _target = target;
            _targetMeta = targetFi;

            _typeContent = target.FindPropertyRelative ("Type").FindPropertyRelative("Content");
            TypePaper t = new TypePaper ( _typeContent.stringValue );

            _data = target.FindPropertyRelative ("Data");

            if ( t.IsValid () )
            {
                _paper = (dat) Activator.CreateInstance ( t.ExtractType() );
                JsonUtility.FromJsonOverwrite ( _data.stringValue, _paper );
            }
        }

        public void OnGUI ()
        {
            DatSelectionGUI ();
            DatEditorGUI ();
        }

        void DatSelectionGUI ()
        {
            if (_paper!=null) return;

            if (_cursor == null)
            {
                if (_targetMeta.FieldType.IsArray)
                _cursor = new CursorGUI ( _targetMeta.FieldType.GetElementType ().GetGenericArguments ()[0], SetDat );
                else
                _cursor = new CursorGUI ( _targetMeta.FieldType.GetGenericArguments ()[0], SetDat );
            }

            _cursor.GUI ();

            void SetDat (Type t)
            {
                _paper = (dat) Activator.CreateInstance ( t );
            }
        }

        void DatEditorGUI ()
        {
            if (_paper == null) return;

            if (dE == null)
            dE = DatEditor.CreateEditor ( _paper );

            dE.GUI ();

            if ( IsWindow && GUILayout.Button ("Save"))
            {
                Save ();
                Close ();
            }

            if (!IsWindow)
            {
                Save ();
            }

            if (GUILayout.Button ("Break"))
            {
                _typeContent.stringValue = "";
                _data.stringValue = "";
                _target.serializedObject.ApplyModifiedProperties ();
                _cursor = null;
                _paper = null;
                if (IsWindow)
                Close ();
            }

            void Save ()
            {
                _typeContent.stringValue = _paper.GetType ().AssemblyQualifiedName;
                _data.stringValue = JsonUtility.ToJson (_paper);
                _target.serializedObject.ApplyModifiedProperties ();
            }
        }
    }

    
    [CustomPropertyDrawer( typeof (DatPaper<>) )]
    public class DatPaperDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;

            GUI.Label ( position, property.FindPropertyRelative ("Type").FindPropertyRelative("Content").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;
            GUI.Label ( position, property.FindPropertyRelative ("Data").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;

            if ( GUI.Button (position,"Edit") )
            DatPaperEditor.Show ( property, fieldInfo );

            EditorGUI.EndProperty ();
        }
    }
}

using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

namespace Lyra.Editor
{
    public class ShardPaperEditor : EditorWindow
    {
        SerializedProperty _target;
        SerializedProperty _typeContent;
        SerializedProperty _data;
        FieldInfo _targetMeta;

        shard _paper;
        ShardEditor dE;
        CursorGUI _cursor;

        bool isWindow;

        public static void Show ( SerializedProperty target, FieldInfo targetFi )
        {
            GetWindow <ShardPaperEditor> ().Load (target,targetFi);
            GetWindow <ShardPaperEditor> ().isWindow = true;
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
                _paper = (shard) Activator.CreateInstance ( t.radiate() );
                JsonUtility.FromJsonOverwrite ( _data.stringValue, _paper );
            }
        }

        public void OnGUI ()
        {
            DatSelectionGUI ();
            ShardEditorGUI ();
        }

        void DatSelectionGUI ()
        {
            if (_paper!=null) return;

            if (_cursor == null)
            {
                if (_targetMeta.FieldType.IsArray)
                _cursor = new CursorGUI ( _targetMeta.FieldType.GetElementType ().GetGenericArguments ()[0], SetShard );
                else
                _cursor = new CursorGUI ( _targetMeta.FieldType.GetGenericArguments ()[0], SetShard );
            }

            _cursor.GUI ();

            void SetShard (Type t)
            {
                _paper = (shard) Activator.CreateInstance ( t );
            }
        }

        void ShardEditorGUI ()
        {
            if (_paper == null) return;

            if (dE == null)
            dE = ShardEditor.CreateEditor ( _paper );

            dE.GUI ();

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
                _typeContent.stringValue = "";
                _data.stringValue = "";
                _target.serializedObject.ApplyModifiedProperties ();
                _cursor = null;
                _paper = null;
                if (isWindow)
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

    
    [CustomPropertyDrawer( typeof (ShardPaper<>) )]
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
            ShardPaperEditor.Show ( property, fieldInfo );

            EditorGUI.EndProperty ();
        }
    }
}

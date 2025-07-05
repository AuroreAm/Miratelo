using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;
using System.Runtime.Serialization;

namespace Pixify.Editor
{
    [CustomPropertyDrawer(typeof (AtomPaper<>))]
    public class AtomPaperDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;

            GUI.Label ( position, property.FindPropertyRelative ("StrNodeType").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;
            GUI.Label ( position, property.FindPropertyRelative ("StrNodeData").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;

            if ( GUI.Button (position,"Edit") )
            AtomPaperEditor.Init ( property, fieldInfo );

            EditorGUI.EndProperty ();
        }
    }

    [CustomPropertyDrawer(typeof (CatomPaper<>))]
    public class CatomPaperDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;

            GUI.Label ( position, property.FindPropertyRelative ("StrNodeType").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;
            GUI.Label ( position, property.FindPropertyRelative ("StrNodeData").stringValue );
            position.y += EditorGUIUtility.singleLineHeight;

            if ( GUI.Button (position,"Edit") )
            AtomPaperEditor.Init ( property, fieldInfo );

            EditorGUI.EndProperty ();
        }
    }


    public class AtomPaperEditor : EditorWindow
    {
        SerializedProperty Target;
        FieldInfo TargetMeta;
        atom paper;
        AtomEditor nE;
        Cursor cursor;

        public static void Init ( SerializedProperty Target, FieldInfo TargetMeta )
        {
            GetWindow <AtomPaperEditor> ().Load (Target,TargetMeta);
        }

        void Load ( SerializedProperty Target , FieldInfo TargetMeta)
        {
            this.Target = Target;
            this.TargetMeta = TargetMeta;

            string NodeTypeName = Target.FindPropertyRelative ("StrNodeType").stringValue;

            if ( !string.IsNullOrEmpty(NodeTypeName) && Type.GetType ( NodeTypeName ) != null )
            {
                paper = (atom) FormatterServices.GetUninitializedObject ( Type.GetType (NodeTypeName) );
                JsonUtility.FromJsonOverwrite ( Target.FindPropertyRelative ("StrNodeData").stringValue, paper );
            }
        }

        void OnGUI ()
        {
            if (Target == null) return;
            AtomSelectionGUI ();
            AtomEditorGUI ();
        }

        void AtomSelectionGUI ()
        {
            if (paper!=null) return;

            if (cursor == null)
            {
                if (TargetMeta.FieldType.IsArray)
                cursor = new Cursor ( SetAtom, TargetMeta.FieldType.GetElementType ().GetGenericArguments ()[0] );
                else
                cursor = new Cursor ( SetAtom, TargetMeta.FieldType.GetGenericArguments ()[0] );
            }

            cursor.GUI ();

            void SetAtom (Type t)
            {
                paper = (atom) FormatterServices.GetUninitializedObject ( t );
            }
        }

        void AtomEditorGUI ()
        {
            if (paper == null) return;

            if (nE == null)
            nE = AtomEditor.CreateEditor ( paper );

            nE.GUI ();

            if (GUILayout.Button ("Save"))
            {
                Target.FindPropertyRelative ("StrNodeType").stringValue = paper.GetType ().AssemblyQualifiedName;
                Target.FindPropertyRelative ("StrNodeData").stringValue = JsonUtility.ToJson (paper);
                Target.serializedObject.ApplyModifiedProperties ();
                Close ();
            }

            if (GUILayout.Button ("Break"))
            {
                paper = null;
                Target.FindPropertyRelative ("StrNodeType").stringValue = "";
                Target.FindPropertyRelative ("StrNodeData").stringValue = "";
                Target.serializedObject.ApplyModifiedProperties ();
                Close ();
            }
        }
    }
}
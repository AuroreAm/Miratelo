using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;


namespace Pixify.Editor
{
    public class PixPaperEditor : EditorWindow
    {
        SerializedProperty Target;
        FieldInfo TargetMeta;
        pix paper;
        PixEditor nE;
        Cursor cursor;

        public static void Init ( SerializedProperty Target, FieldInfo TargetMeta )
        {
            GetWindow <PixPaperEditor> ().Load (Target,TargetMeta);
        }

        void Load ( SerializedProperty Target , FieldInfo TargetMeta)
        {
            this.Target = Target;
            this.TargetMeta = TargetMeta;

            string NodeTypeName = Target.FindPropertyRelative ("StrNodeType").stringValue;

            if ( !string.IsNullOrEmpty(NodeTypeName) && Type.GetType ( NodeTypeName ) != null )
            {
                paper = (pix) Activator.CreateInstance ( Type.GetType (NodeTypeName) );
                JsonUtility.FromJsonOverwrite ( Target.FindPropertyRelative ("StrNodeData").stringValue, paper );
            }
        }

        void OnGUI ()
        {
            if (Target == null) return;
            PixSelectionGUI ();
            PixEditorGUI ();
        }

        void PixSelectionGUI ()
        {
            if (paper!=null) return;

            if (cursor == null)
            {
                if (TargetMeta.FieldType.IsArray)
                cursor = new Cursor ( SetPix, TargetMeta.FieldType.GetElementType ().GetGenericArguments ()[0] );
                else
                cursor = new Cursor ( SetPix, TargetMeta.FieldType.GetGenericArguments ()[0] );
            }

            cursor.GUI ();

            void SetPix (Type t)
            {
                paper = (pix) Activator.CreateInstance ( t );
            }
        }

        void PixEditorGUI ()
        {
            if (paper == null) return;

            if (nE == null)
            nE = PixEditor.CreateEditor ( paper );

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


    [CustomPropertyDrawer(typeof (PixPaper<>))]
    public class PixPaperDrawer : PropertyDrawer
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
            PixPaperEditor.Init ( property, fieldInfo );

            EditorGUI.EndProperty ();
        }

    }
}

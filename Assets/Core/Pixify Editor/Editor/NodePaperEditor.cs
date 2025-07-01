using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;

namespace Pixify.Editor
{

    [CustomPropertyDrawer(typeof (NodePaper<>))]
    public class NodePaperDrawer : PropertyDrawer
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
            NodePaperEditor.Init ( property, fieldInfo );


            EditorGUI.EndProperty ();
        }
    }


    public class NodePaperEditor : EditorWindow
    {
        SerializedProperty Target;
        FieldInfo TargetMeta;
        node paper;
        NodeEditor nE;
        Cursor cursor;

        public static void Init ( SerializedProperty Target, FieldInfo TargetMeta )
        {
            GetWindow <NodePaperEditor> ().Load (Target,TargetMeta);
        }

        void Load ( SerializedProperty Target , FieldInfo TargetMeta)
        {
            this.Target = Target;
            this.TargetMeta = TargetMeta;

            string NodeTypeName = Target.FindPropertyRelative ("StrNodeType").stringValue;

            if ( Type.GetType ( NodeTypeName ) != null )
            {
                paper = (node) Activator.CreateInstance ( Type.GetType (NodeTypeName) );
                JsonUtility.FromJsonOverwrite ( Target.FindPropertyRelative ("StrNodeData").stringValue, paper );
            }
        }

        void OnGUI ()
        {
            if (Target == null) return;
            NodeSelectionGUI ();
            NodeEditorGUI ();
        }

        void NodeSelectionGUI ()
        {
            if (paper!=null) return;

            if (cursor == null)
            {
                if (TargetMeta.FieldType.IsArray)
                cursor = new Cursor ( SetNode, TargetMeta.FieldType.GetElementType ().GetGenericArguments ()[0] );
                else
                cursor = new Cursor ( SetNode, TargetMeta.FieldType.GetGenericArguments ()[0] );
            }

            cursor.GUI ();

            void SetNode (Type t)
            {
                paper = Activator.CreateInstance (t) as node;
            }
        }

        void NodeEditorGUI ()
        {
            if (paper == null) return;

            if (nE == null)
            nE = NodeEditor.CreateEditor ( paper );

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
            }
        }
    }
}
using System.Reflection;
using System;
using UnityEditor;
using UnityEngine;

namespace Lyra.Editor
{
    /*[CustomPropertyDrawer(typeof (type_paper))]
    public class type_paperDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty (position, label, property);

            position.height = EditorGUIUtility.singleLineHeight;

            if (UnityEngine.GUI.Button ( position, TypeNameByString( property.FindPropertyRelative ("Content").stringValue ) ) )
                TypeSelectionWindow.Show ( property );

            EditorGUI.EndProperty ();
        }

        static string TypeNameByString ( string FullAssemblyName )
        => ( ( !string.IsNullOrEmpty ( FullAssemblyName ) ) && Type.GetType ( FullAssemblyName ) != null ) ? Type.GetType ( FullAssemblyName ).Name : FullAssemblyName;
    }*/
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Triheroes.Code;
using GluonGui.Dialog;

namespace Triheroes.Editor
{
    public class CameraShotCreator : EditorWindow
    {
        static CameraShotCreator o => _o ?? (_o = GetWindow<CameraShotCreator>());
        static CameraShotCreator _o;

        [MenuItem("Window/Triheroes/Camera Shot Creator")]
        static void Init()
        {
            _o = GetWindow<CameraShotCreator>();
            _o.Show();
        }

        void OnEnable()
        {
            Selection.selectionChanged += SelectCamera;
            if (Selection.activeGameObject)
            {
                var cam = Selection.activeGameObject.GetComponent<Camera>();
                if (cam)
                Reference = cam;
            }

            result.FieldOfView = 60;
        }

        void SelectCamera()
        {
            if (!Selection.activeGameObject) return;
            var cam = Selection.activeGameObject.GetComponent<Camera>();
            if (cam)
            Reference = cam;
            Repaint();
        }

        Camera Reference;
        Transform focalPointTransform;
        cs_subject_data result;

        public static cs_subject_data GetResult () => o.result;

        void OnGUI ()
        {
            if (Reference == null)
                return;

            GUILayout.Label("Camera Shot Creator Editing: " + Reference.gameObject.name);
            GUILayout.Space(10);

            focalPointTransform = EditorGUILayout.ObjectField ("Focal Point", focalPointTransform, typeof(Transform), true) as Transform;

            if (focalPointTransform)
            {
                result.FocalPoint = focalPointTransform.position;
                result.Distance = Vector3.Distance(result.FocalPoint, Reference.transform.position);
                result.RotY = Quaternion.LookRotation(result.FocalPoint - Reference.transform.position).eulerAngles;
                focalPointTransform = null;
            }
            result.FocalPoint = EditorGUILayout.Vector3Field("Focal Point", result.FocalPoint);
            result.RotY = EditorGUILayout.Vector3Field("RotY", result.RotY);
            GUILayout.Space(10);
            
            result.FieldOfView = EditorGUILayout.FloatField("Field of View", result.FieldOfView);
            GUILayout.Space(10);

            result.RotYOffset = EditorGUILayout.Vector3Field("RotY Offset", result.RotYOffset);
            result.RotYOffset.y = EditorGUILayout.Slider ( "Yaw", result.RotYOffset.y, -45, 45);
            result.RotYOffset.x = EditorGUILayout.Slider ( "Pitch", result.RotYOffset.x, -45, 45);

            result.Roll = EditorGUILayout.FloatField("Roll", result.Roll);
            result.Roll = EditorGUILayout.Slider ( "Roll", result.Roll, -90, 90);

            GUILayout.Space(10);
            result.Offset = EditorGUILayout.Vector3Field("Offset", result.Offset);

            // preview result
            Reference.transform.position = result.GetPos;
            Reference.transform.rotation = result.GetRot;
            Reference.fieldOfView = result.FieldOfView;
        }
    }

    [CustomPropertyDrawer(typeof(cs_subject_data))]
    public class cs_subject_dataDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 8;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position.height = EditorGUIUtility.singleLineHeight * 7;
            GUI.Label(position, string.Concat("Focal Point: ", property.FindPropertyRelative("focalPoint").vector3Value.ToString(), "\nDistance: ", property.FindPropertyRelative("distance").floatValue.ToString(), "\nField of View: ", property.FindPropertyRelative("fieldOfView").floatValue.ToString(), "\nRoll: ", property.FindPropertyRelative("roll").floatValue.ToString(), "\nRotY: ", property.FindPropertyRelative("RotY").vector3Value.ToString(), "\nOffset: ", property.FindPropertyRelative("offset").vector3Value.ToString(), "\nRotY Offset: ", property.FindPropertyRelative("rotYOffset").vector3Value.ToString()));

            position.height = EditorGUIUtility.singleLineHeight;
            position.y+=EditorGUIUtility.singleLineHeight * 7;
            if (GUI.Button(position, "paste from camera shot creator"))
            {
                var Result = CameraShotCreator.GetResult ();
                property.FindPropertyRelative("focalPoint").vector3Value = Result.FocalPoint;
                property.FindPropertyRelative("distance").floatValue = Result.Distance;
                property.FindPropertyRelative("fieldOfView").floatValue = Result.FieldOfView;
                property.FindPropertyRelative("roll").floatValue = Result.Roll;
                property.FindPropertyRelative("RotY").vector3Value = Result.RotY;
                property.FindPropertyRelative("offset").vector3Value = Result.Offset;
                property.FindPropertyRelative("rotYOffset").vector3Value = Result.RotYOffset;
                property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.EndProperty();
        }
    }
}

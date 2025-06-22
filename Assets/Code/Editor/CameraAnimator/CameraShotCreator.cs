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

            result.fieldOfView = 60;
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
                result.focalPoint = focalPointTransform.position;
                result.distance = Vector3.Distance(result.focalPoint, Reference.transform.position);
                result.RotY = Quaternion.LookRotation(result.focalPoint - Reference.transform.position).eulerAngles;
                focalPointTransform = null;
            }
            result.focalPoint = EditorGUILayout.Vector3Field("Focal Point", result.focalPoint);
            result.RotY = EditorGUILayout.Vector3Field("RotY", result.RotY);
            GUILayout.Space(10);
            
            result.fieldOfView = EditorGUILayout.FloatField("Field of View", result.fieldOfView);
            GUILayout.Space(10);

            result.rotYOffset = EditorGUILayout.Vector3Field("RotY Offset", result.rotYOffset);
            result.rotYOffset.y = EditorGUILayout.Slider ( "Yaw", result.rotYOffset.y, -45, 45);
            result.rotYOffset.x = EditorGUILayout.Slider ( "Pitch", result.rotYOffset.x, -45, 45);

            result.roll = EditorGUILayout.FloatField("Roll", result.roll);
            result.roll = EditorGUILayout.Slider ( "Roll", result.roll, -90, 90);

            GUILayout.Space(10);
            result.offset = EditorGUILayout.Vector3Field("Offset", result.offset);

            // preview result
            Reference.transform.position = result.GetPos;
            Reference.transform.rotation = result.GetRot;
            Reference.fieldOfView = result.fieldOfView;
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
                property.FindPropertyRelative("focalPoint").vector3Value = Result.focalPoint;
                property.FindPropertyRelative("distance").floatValue = Result.distance;
                property.FindPropertyRelative("fieldOfView").floatValue = Result.fieldOfView;
                property.FindPropertyRelative("roll").floatValue = Result.roll;
                property.FindPropertyRelative("RotY").vector3Value = Result.RotY;
                property.FindPropertyRelative("offset").vector3Value = Result.offset;
                property.FindPropertyRelative("rotYOffset").vector3Value = Result.rotYOffset;
                property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.EndProperty();
        }
    }
}

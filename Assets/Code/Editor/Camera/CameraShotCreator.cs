using UnityEngine;
using UnityEditor;
using Triheroes.Code;
using Triheroes.Code.CameraShot;

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
                reference = cam;
            }

            result.fov = 60;
        }

        void SelectCamera()
        {
            if (!Selection.activeGameObject) return;
            var cam = Selection.activeGameObject.GetComponent<Camera>();
            if (cam)
            reference = cam;
            Repaint();
        }

        Camera reference;
        Transform focal_point_transform;
        subject.data result;

        public static subject.data GetResult () => o.result;

        void OnGUI ()
        {
            if (reference == null)
                return;

            GUILayout.Label("Camera Shot Creator Editing: " + reference.gameObject.name);
            GUILayout.Space(10);

            focal_point_transform = EditorGUILayout.ObjectField ("Focal Point", focal_point_transform, typeof(Transform), true) as Transform;

            if (focal_point_transform)
            {
                result.focal_point = focal_point_transform.position;
                result.distance = Vector3.Distance(result.focal_point, reference.transform.position);
                result.rot = Quaternion.LookRotation(result.focal_point - reference.transform.position).eulerAngles;
                focal_point_transform = null;
            }
            result.focal_point = EditorGUILayout.Vector3Field("Focal Point", result.focal_point);
            result.rot = EditorGUILayout.Vector3Field("RotY", result.rot);
            GUILayout.Space(10);
            
            result.fov = EditorGUILayout.FloatField("Field of View", result.fov);
            GUILayout.Space(10);

            result.roty_offset = EditorGUILayout.Vector3Field("RotY Offset", result.roty_offset);
            result.roty_offset.y = EditorGUILayout.Slider ( "Yaw", result.roty_offset.y, -45, 45);
            result.roty_offset.x = EditorGUILayout.Slider ( "Pitch", result.roty_offset.x, -45, 45);

            result.roll = EditorGUILayout.FloatField("Roll", result.roll);
            result.roll = EditorGUILayout.Slider ( "Roll", result.roll, -90, 90);

            GUILayout.Space(10);
            result.offset = EditorGUILayout.Vector3Field("Offset", result.offset);

            // preview result
            reference.transform.position = result.GetPos;
            reference.transform.rotation = result.GetRot;
            reference.fieldOfView = result.fov;
        }
    }

    [CustomPropertyDrawer(typeof(subject.data))]
    public class dataDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 8;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position.height = EditorGUIUtility.singleLineHeight * 7;
            GUI.Label(position, string.Concat("Focal Point: ", property.FindPropertyRelative("focal_point").vector3Value.ToString(), "\nDistance: ", property.FindPropertyRelative("distance").floatValue.ToString(), "\nField of View: ", property.FindPropertyRelative("fov").floatValue.ToString(), "\nRoll: ", property.FindPropertyRelative("roll").floatValue.ToString(), "\nRotY: ", property.FindPropertyRelative("rot").vector3Value.ToString(), "\nOffset: ", property.FindPropertyRelative("offset").vector3Value.ToString(), "\nRotY Offset: ", property.FindPropertyRelative("roty_offset").vector3Value.ToString()));

            position.height = EditorGUIUtility.singleLineHeight;
            position.y+=EditorGUIUtility.singleLineHeight * 7;
            if (GUI.Button(position, "paste from camera shot creator"))
            {
                var Result = CameraShotCreator.GetResult ();
                property.FindPropertyRelative("focal_point").vector3Value = Result.focal_point;
                property.FindPropertyRelative("distance").floatValue = Result.distance;
                property.FindPropertyRelative("fov").floatValue = Result.fov;
                property.FindPropertyRelative("roll").floatValue = Result.roll;
                property.FindPropertyRelative("rot").vector3Value = Result.rot;
                property.FindPropertyRelative("offset").vector3Value = Result.offset;
                property.FindPropertyRelative("roty_offset").vector3Value = Result.roty_offset;
                property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.EndProperty();
        }
    }
}

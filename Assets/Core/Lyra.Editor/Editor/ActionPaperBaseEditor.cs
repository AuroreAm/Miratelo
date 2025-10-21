using UnityEditor;
using System.Reflection;
using UnityEngine;

namespace Lyra.Editor
{
    [CustomEditor (typeof(ActionPaperBase),true)]
    public class ActionPaperBaseEditor : UnityEditor.Editor
    {
        ActionPaperBase _target;
        MoonPaperEditor _editor;

        const string PaperField = "Paper";

        void OnEnable ()
        {
            _target = target as ActionPaperBase;
            _editor = CreateInstance <MoonPaperEditor> ();

            var paper = serializedObject.FindProperty ( PaperField );
            _editor.Load ( paper , _target.GetType ().GetField ( PaperField, BindingFlags.Instance | BindingFlags.Public ) );
        }

        public override void OnInspectorGUI()
        {
            if ( _target.Paper.have_type () ) {
                var _current = _target.Paper.get_type ();

                var p = _current.GetCustomAttribute <paperAttribute> ();
                if ( p != null && p.PaperType != _target.GetType () ) {
                    GameObject g = _target.gameObject;
                    _target.gameObject.AddComponent ( p.PaperType );
                    DestroyImmediate (_target);
                    EditorUtility.SetDirty (g);
                }
            }

            _editor.OnGUI ();
        }
    }
}
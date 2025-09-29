using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

namespace Lyra.Editor
{
    [CustomEditor (typeof(ActionPaper),true)]
    public class ActionPaperEditor : UnityEditor.Editor
    {
        ActionPaper _target;
        MoonPaperEditor _editor;

        const string PaperField = "Paper";

        void OnEnable ()
        {
            _target = target as ActionPaper;
            _editor = CreateInstance <MoonPaperEditor> ();

            var paper = serializedObject.FindProperty ( PaperField );
            _editor.Load ( paper , _target.GetType ().GetField ( PaperField, BindingFlags.Instance | BindingFlags.Public ) );
        }

        public override void OnInspectorGUI()
        {
            _editor.OnGUI ();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNodeEditor;
using XNode;
using System;

namespace Pixify.Editor
{
    [CustomNodeEditor (typeof (ActionModel))]
    public class ActionModelEditor : XNodeEditor.NodeEditor
    {
        public ActionModel Target;
        NodeEditor nE;

        void Awake ()
        {
            Target = target as ActionModel;
            // create an editor for the blueprint
            nE = NodeEditor.CreateEditor (Target.BluePrintPaper.blueprint,Target);
        }

        public override void OnHeaderGUI()
        {
            if (Target == null) Awake ();
            GUILayout.Label (string.IsNullOrEmpty(Target.Tag)? Target.BluePrintPaper.blueprint.GetType().Name : Target.Tag, NodeEditorResources.styles.nodeHeader,GUILayout.Height(30));
        }

        public override void OnBodyGUI()
        {
            NodeEditorGUILayout.PortField(new Vector2(8, 12), Target.GetInputPort("In"));
            nE.GUI();

            if (Selection.activeObject == Target)
            ToolGUI ();
        }

        void ToolGUI()
        {
            Target.Tag = EditorGUILayout.TextField("Tag", Target.Tag);
            EditorUtility.SetDirty(Target);
        }
    }
}
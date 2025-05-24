using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
namespace Pixify.Editor
{
    [CustomNodeEditor (typeof (DecoratorModel))]
    public class DecoratorModelEditor : ActionModelEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            NodeEditorGUILayout.PortField(new Vector2(GetWidth() - 24, 12), Target.GetOutputPort("Out"));
        }
    }
}
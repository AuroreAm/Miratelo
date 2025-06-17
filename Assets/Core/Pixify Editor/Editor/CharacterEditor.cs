using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pixify.Editor.PixGUI;

namespace Pixify.Editor
{
    /*
    [CustomEditor(typeof(Character))]
    public class CharacterEditor : UnityEditor.Editor
    {
        AreaScreen Debugger;
        Area mainDebugger;
        AreaAutoHeight Content;
        Character Target;

        void OnEnable()
        {
            Target = target as Character;
        }

        void Load ()
        {
            var mcc = Target.GetModule<m_character_controller>();
            
            mainDebugger = new Area(); mainDebugger.Padding(new Vector4(8, 8, 8, 8));

            if (mcc!=null)
            {
                Content = actionDebuggerBase.CreateDebugger ( mcc.root );
                mainDebugger.Add ( Content );
            }

            Debugger = new AreaScreen( mainDebugger );
        }

        override public void OnInspectorGUI()
        {
            if (!EditorApplication.isPlaying) return;

            if (Debugger == null) Load();
            Debugger.Draw();

            GUILayout.Space (Content.Transform.height);
        }
    }*/
}
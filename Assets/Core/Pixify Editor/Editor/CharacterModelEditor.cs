using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pixify.Editor
{
    [CustomEditor(typeof(CharacterModel))]
    public class CharacterModelEditor : UnityEditor.Editor
    {

        CharacterModel Target;
        Cursor <ModuleWriter> cursor;

        void OnEnable()
        {
            Target = (CharacterModel)target;
            if (Target.Parameters == null)
                Target.Parameters = new List<ModuleWriter>();

            cursor = new Cursor <ModuleWriter> (AddModule);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            cursor.GUI();
        }

        void AddModule(Type t)
        {
            if (t.IsSubclassOf(typeof(ModuleWriter)) && Target.Parameters.Find(x => x.GetType() == t) == null)
            {
                var newWriter = Activator.CreateInstance(t) as ModuleWriter;
                Target.Parameters.Add(newWriter);
                EditorUtility.SetDirty(Target);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEditor;

namespace Pixify.Editor
{
    // TODO: design a pretty UI for this
    [CustomEditor(typeof(CharacterWriter))]
    public class CharacterWriterEditor : UnityEditor.Editor
    {
        CharacterWriter Target;
        Cursor <ModuleWriter> cursor;

        void OnEnable()
        {
            Target = (CharacterWriter)target;
            if (Target.OverrideParameters == null)
                Target.OverrideParameters = new List<ModuleWriter>();

            cursor = new Cursor <ModuleWriter> (AddModule);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            cursor.GUI();
        }

        void AddModule(Type t)
        {
            if (t.IsSubclassOf(typeof(ModuleWriter)) && Target.OverrideParameters.Find(x => x.GetType() == t) == null)
            {
                var newWriter = Activator.CreateInstance(t) as ModuleWriter;
                Target.OverrideParameters.Add(newWriter);
                EditorUtility.SetDirty(Target);
            }
        }
    }
}

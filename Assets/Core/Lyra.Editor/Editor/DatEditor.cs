using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Lyra.Editor
{
    [DatEditorOf (typeof (dat))]
    public class DatEditor
    {
        protected dat Target;

        public virtual void Create () {}
        public virtual void GUI () => DatGUI ( Target );

        public static DatEditor CreateEditor ( dat target )
        {
            var A = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> allDatEditor = new List<Type>();

            foreach (var y in A)
            foreach (Type x in y.GetTypes())
            {
                if (x.IsSubclassOf(typeof(DatEditor)))
                    allDatEditor.Add(x);
            }
            
            Type Current = typeof(DatEditor);
            foreach (Type t in allDatEditor)
            {
                Type SupportedNode = t.GetCustomAttribute<DatEditorOfAttribute>().PixType;

                if ((target.GetType().IsSubclassOf(SupportedNode) || target.GetType() == SupportedNode) && SupportedNode.IsSubclassOf(Current.GetCustomAttribute<DatEditorOfAttribute>().PixType))
                    Current = t;
            }

            DatEditor dE = (DatEditor) Activator.CreateInstance(Current);
            dE.Target = target;
            dE.Create();

            return dE;
        }

        public static void DatGUI (dat node)
        {
            EditorGUI.BeginChangeCheck();

            // Y is the class that is being inspected
            foreach (FieldInfo fi in node.GetType().GetFields())
            {
                if (fi.IsPublic && fi.GetCustomAttribute<ExportAttribute>() != null)
                    LyraGUILayout.FieldGUI(fi, node);
            }

            if (EditorGUI.EndChangeCheck())
            {}
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DatEditorOfAttribute : Attribute
    {
        public Type PixType;
        public DatEditorOfAttribute(Type Class)
        { PixType = Class; }
    }

    public static class LyraGUILayout
    {
        public static void FieldGUI(FieldInfo fi, object o)
        {
            if (fi.FieldType == typeof(bool))
            {
                fi.SetValue(o, GUILayout.Toggle((bool)fi.GetValue(o), fi.Name));
                return;
            }
            
            if (fi.FieldType == typeof(int))
            {
                fi.SetValue(o, EditorGUILayout.IntField(fi.Name, (int)fi.GetValue(o)));
                return;
            }

            if (fi.FieldType == typeof(string))
            {
                fi.SetValue(o, EditorGUILayout.TextField(fi.Name, (string)fi.GetValue(o)));
                return;
            }

            if (fi.FieldType == typeof(string[]))
            {
                List<string> vars = new List<string>();
                vars.AddRange((string[])fi.GetValue(o));
                if (GUILayout.Button("++"))
                    vars.Add("");
                for (int i = 0; i < vars.Count; i++)
                {
                    GUILayout.BeginHorizontal();
                    vars[i] = EditorGUILayout.TextField(vars[i]);
                    if (GUILayout.Button("X", GUILayout.Width(16), GUILayout.Height(16)))
                    {
                        vars.RemoveAt(i); break;
                    }
                    GUILayout.EndHorizontal();
                }
                fi.SetValue(o, vars.ToArray());
                return;
            }

            if (fi.FieldType == typeof(float))
            {
                fi.SetValue(o, EditorGUILayout.FloatField(fi.Name, (float)fi.GetValue(o)));
                return;
            }

            if (fi.FieldType == typeof(Vector3))
            {
                fi.SetValue(o, EditorGUILayout.Vector3Field(fi.Name, (Vector3)fi.GetValue(o)));
                return;
            }

            if (fi.FieldType == typeof(Color))
            {
                fi.SetValue(o, EditorGUILayout.ColorField(fi.Name, (Color)fi.GetValue(o)));
                return;
            }

            if (fi.FieldType.IsEnum)
            {
                fi.SetValue(o, EditorGUILayout.EnumPopup(fi.Name, (Enum)fi.GetValue(o)));
                return;
            }
        }
    }
}

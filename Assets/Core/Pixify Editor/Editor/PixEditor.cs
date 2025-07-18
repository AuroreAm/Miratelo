using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Pixify.Editor
{
    [PixEditorOf (typeof (pix))]
    public class PixEditor
    {
        protected pix target;

        public virtual void Create () {}
        public virtual void GUI () => NodeGUI ( target );

        public static PixEditor CreateEditor ( pix target, UnityEditor.Editor editorHost =  null )
        {
            var A = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> allNodeEditor = new List<Type>();

            foreach (var y in A)
            foreach (Type x in y.GetTypes())
            {
                if (x.IsSubclassOf(typeof(PixEditor)))
                    allNodeEditor.Add(x);
            }
            
            Type Current = typeof(PixEditor);
            foreach (Type t in allNodeEditor)
            {
                Type SupportedNode = t.GetCustomAttribute<PixEditorOfAttribute>().PixType;

                if ((target.GetType().IsSubclassOf(SupportedNode) || target.GetType() == SupportedNode) && SupportedNode.IsSubclassOf(Current.GetCustomAttribute<PixEditorOfAttribute>().PixType))
                    Current = t;
            }

            PixEditor nE = (PixEditor) Activator.CreateInstance(Current);
            nE.target = target;
            nE.Create();

            return nE;
        }

        public static void NodeGUI (pix node)
        {
            EditorGUI.BeginChangeCheck();
            // Y is the class that is being inspected
            foreach (FieldInfo fi in node.GetType().GetFields())
            {
                if (fi.IsPublic && fi.GetCustomAttribute<ExportAttribute>() != null)
                    NGUILayout.FieldGUI(fi, node);
            }
            if (EditorGUI.EndChangeCheck())
            {}
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PixEditorOfAttribute : Attribute
    {
        public Type PixType;
        public PixEditorOfAttribute(Type Class)
        { PixType = Class; }
    }

    public static class NGUILayout
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

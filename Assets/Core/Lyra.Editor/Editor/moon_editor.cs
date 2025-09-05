using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Lyra.Editor
{
    [moon_editor_of (typeof (moon))]
    public class moon_editor
    {
        protected moon Target;

        public virtual void _ready () {}
        public virtual void _gui () => moon_gui ( Target );

        public static moon_editor create_editor ( moon target )
        {
            var A = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> all_editor = new List<Type>();

            foreach (var y in A)
            foreach (Type x in y.GetTypes())
            {
                if (x.IsSubclassOf(typeof(moon_editor)))
                    all_editor.Add(x);
            }
            
            Type current = typeof(moon_editor);
            foreach (Type t in all_editor)
            {
                Type SupportedNode = t.GetCustomAttribute<moon_editor_ofAttribute>().PixType;

                if ((target.GetType().IsSubclassOf(SupportedNode) || target.GetType() == SupportedNode) && SupportedNode.IsSubclassOf(current.GetCustomAttribute<moon_editor_ofAttribute>().PixType))
                    current = t;
            }

            moon_editor me = (moon_editor) Activator.CreateInstance(current);
            me.Target = target;
            me._ready();

            return me;
        }

        public static void moon_gui (moon item)
        {
            EditorGUI.BeginChangeCheck();

            // Y is the class that is being inspected
            foreach (FieldInfo fi in item.GetType().GetFields())
            {
                if (fi.IsPublic && fi.GetCustomAttribute<exportAttribute>() != null)
                    LyraGUILayout.FieldGUI(fi, item);
            }

            if (EditorGUI.EndChangeCheck())
            {}
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class moon_editor_ofAttribute : Attribute
    {
        public Type PixType;
        public moon_editor_ofAttribute(Type Class)
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

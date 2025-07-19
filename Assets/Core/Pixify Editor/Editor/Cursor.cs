using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Linq;
using System.Reflection;

namespace Pixify.Editor
{

    public class Cursor<T> : Cursor where T:pix
    {
        public Cursor(Action<Type> Ev) : base(Ev, typeof (T))
        {}
    }

    /// <summary>
    /// Editor cursor to find all types specified in filter
    /// </summary>
    public class Cursor
    {
        public Dictionary <string, Type[]> Types;
        Type Filter;
        public SearchField Search;
        string searchQuerry = "";
        Vector2 scroll;
        public Action<Type> Add;

        public Cursor(Action<Type> Ev, Type Filter)
        {
            Search = new SearchField();
            this.Filter = Filter;
            if (Types == null)
                FetchTypes();
            Add += Ev;
        }

        public void GUI()
        {
            GUILayout.Label(Filter.Name, PixStyle.o.Title1);

            searchQuerry = Search.OnGUI(searchQuerry);

            scroll = GUILayout.BeginScrollView(scroll, GUILayout.Height(512));

            Rect Section;
            for (int i = 0; i < Types.Count; i++)
            {
                var Key = Types.ElementAt(i).Key;

                Section = EditorGUILayout.BeginVertical();
                EditorGUI.DrawRect(Section, new Color(.2f, .2f, .2f));

                List<Type> TypeList = new List<Type>();
                for (int j = 0; j < Types[Key].Length; j++)
                if ( Types[Key][j].Name.ToLower().Contains ( searchQuerry.ToLower() ) )
                    TypeList.Add ( Types[Key][j] );

                if ( TypeList.Count > 0 )
                {
                    GUILayout.Label ( Key, PixStyle.o.Title2 );
                    for (int j = 0; j < TypeList.Count; j++)
                    if (TypeList[j].IsSubclassOf(Filter) && !TypeList[j].IsAbstract && GUILayout.Button(TypeList[j].Name))
                    {
                        Add?.Invoke(TypeList[j]);
                    }
                }
                EditorGUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
        }

        void FetchTypes()
        {
            List<Type> TypeList = new List<Type>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                TypeList.AddRange(a.GetTypes().Where(type => type.IsSubclassOf(Filter)));

            Types = TypeList.GroupBy( x=> x.GetCustomAttributes (typeof (CategoryAttribute), true).OfType<CategoryAttribute>().FirstOrDefault()?.Name).ToDictionary ( x => x.Key, x => x.ToArray() );

            Types.Remove (string.Empty);
        }

        
        public static Dictionary<string, Type[]> FetchTypesByCategory (Type filter)
        {
            List<Type> TypeList = new List<Type>();
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                TypeList.AddRange(a.GetTypes().Where(type => type.IsSubclassOf(filter) && !type.IsAbstract));

            Dictionary <string, Type[]>  Types = TypeList.GroupBy( x=> x.GetCustomAttributes (typeof (CategoryAttribute), true).OfType<CategoryAttribute>().FirstOrDefault()?.Name).ToDictionary ( x => x.Key, x => x.ToArray() );

            Types.Remove (string.Empty);
            return Types;
        }
    }
}

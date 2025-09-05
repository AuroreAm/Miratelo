using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.Linq;
using System.Reflection;

namespace Lyra.Editor
{
    public class CursorGUI<T> : CursorGUI where T : moon
    {
        public CursorGUI(Action<Type> Ev) : base(typeof (T), Ev)
        {}
    }

    public class CursorGUI
    {
        public SearchField SearchField { private set; get; }
        
        Dictionary <string, Type[]> _types;
        Type _filter;
        string _searchQuerry = "";
        Vector2 _scroll;
        Action<Type> _onSelect;

        public CursorGUI ( Type filter, Action <Type> onSelect )
        {
            _filter = filter;
            _onSelect = onSelect;
            _types = GetTypesByPath ( filter );

            SearchField = new SearchField ();
        }

        public void GUI()
        {
            GUILayout.Label(_filter.Name);

            _searchQuerry = SearchField.OnGUI(_searchQuerry);

            _scroll = GUILayout.BeginScrollView(_scroll, GUILayout.Height(512));

            Rect Section;
            for (int i = 0; i < _types.Count; i++)
            {
                var Key = _types.ElementAt(i).Key;

                Section = EditorGUILayout.BeginVertical();
                EditorGUI.DrawRect(Section, new Color(.2f, .2f, .2f));

                List<Type> TypeList = new List<Type>();
                for (int j = 0; j < _types[Key].Length; j++)
                if ( _types[Key][j].Name.ToLower().Contains ( _searchQuerry.ToLower() ) )
                    TypeList.Add ( _types[Key][j] );

                if ( TypeList.Count > 0 )
                {
                    GUILayout.Label ( Key );
                    for (int j = 0; j < TypeList.Count; j++)
                    if (TypeList[j].IsSubclassOf(_filter) && !TypeList[j].IsAbstract && GUILayout.Button(TypeList[j].Name))
                        _onSelect?.Invoke(TypeList[j]);
                }
                EditorGUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
        }

        public static Dictionary <string, Type[]> GetTypesByPath ( Type Filter )
        {
            List<Type> TypeList = new List<Type>();

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                TypeList.AddRange(a.GetTypes().Where(type => type.IsSubclassOf(Filter)));

            var Types = TypeList.GroupBy( x=> x.GetCustomAttributes (typeof (pathAttribute), true).OfType<pathAttribute>().FirstOrDefault()?.name).ToDictionary ( x => x.Key, x => x.ToArray() );
            Types.Remove (string.Empty);

            return Types;
        }

    }
}

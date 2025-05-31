using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Linq;
using Pixify.Editor.PixGUI;
using static Pixify.Editor.ScriptEditorStyles;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;

namespace Pixify.Editor
{

    // new version of script editor with advanced features
    public class ScriptEditor : EditorWindow
    {
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (!(obj is ScriptModel s)) return false;

            GetWindow<ScriptEditor>();
            return true;
        }

        AreaTree Visual;
        void OnEnable()
        {
            var test = new DecoratorModel();
            test.BluePrintPaper.Set(typeof(sequence));

            var test2 = new ActionModel();
            test2.BluePrintPaper.Set(typeof(Skip));

            var test3 = new ActionModel();
            test3.BluePrintPaper.Set(typeof(Log));

            var test4 = new DecoratorModel();
            test4.BluePrintPaper.Set(typeof(sequence));

            test.Child.AddRange(new ActionModel[] { test2, test3, test4 });

            Visual = new AreaTree(
                new Area(
                    new Box() { ColorA = o.BackgroundColor },

                    new Area(
                        new Box() { ColorA = o.NormalColor },
                        new Label("Library", o.h1).FitContent().Position(new Vector2(2, 2)),
                        new ActionLibrary().Padding(new Vector4(0, 32, 0, 0))
                    ).RelativeTransform(new Rect(0, 0, .25f, 1)).Padding(new Vector4(8, 8 + 16, 8, 8)),

                    new Area(
                        new Box() { ColorA = o.NormalColor },
                        new Label("Commands", o.h1).FitContent().Position(new Vector2(2, 2)),
                        new Area(
                            new Box() { ColorA = o.ContentColor, ColorB = o.BorderColor, BorderWidth = 1 },
                            new Area(
                                new DecoratorElement (test)
                            ).Padding (new Vector4(4,4,4,4))
                        ).RelativeTransform(new Rect(0, 0, 1, 1)).Padding(new Vector4(8, 8 + 16, 8, 8))
                    ).RelativeTransform(new Rect(.25f, 0, .5f, 1)).Padding(new Vector4(8, 8 + 16, 8, 8)),

                    new Area(
                        new Box() { ColorA = o.NormalColor },
                        new Label("Properties", o.h1).FitContent().Position(new Vector2(2, 2)),
                        new Area(
                            new Box() { ColorA = o.ContentColor, ColorB = o.BorderColor, BorderWidth = 1 }
                        ).RelativeTransform(new Rect(0, 0, 1, 1)).Padding(new Vector4(8, 8 + 16, 8, 8))
                    ).RelativeTransform(new Rect(.75f, 0, .25f, .5f)).Padding(new Vector4(8, 8 + 16, 8, 8)),

                    new Area(
                        new Box() { ColorA = o.NormalColor },
                        new Label("Description", o.h1).FitContent().Position(new Vector2(2, 2))
                    ).RelativeTransform(new Rect(.75f, .5f, .25f, .5f)).Padding(new Vector4(8, 8, 8, 8 + 32))
                )
            );
        }

        void OnGUI()
        {
            Visual.Draw();
        }
    }

    #region Library
    class ActionLibrary : Area
    {
        Dictionary<string, LibraryTab> Tabs;
        SelectionRow Tab;
        SearchField Search;
        public string searchQuerry = "";

        public ActionLibrary()
        {
            CreateTabsData();
            CreateVisual();
        }

        void CreateTabsData()
        {
            Tabs = new Dictionary<string, LibraryTab>();

            var Types = Cursor.FetchTypesByCategory(typeof(action));

            Tabs = new Dictionary<string, LibraryTab>
                {
                    { "decorator", new ActionLibrary1(Types["decorator"]) },
                    { "blackboard", new BlackboardLibrary() }
                };

            for (int i = 0; i < Types.Count; i++)
            {
                var Key = Types.ElementAt(i).Key;
                if (!Tabs.ContainsKey(Key))
                    Tabs.Add(Key, new ActionLibrary1(Types[Key]));
            }
        }

        Element SearchBar;

        void CreateVisual()
        {
            Search = new SearchField();

            SearchBar = new Area().RelativeTransform(new Rect(0, 0, 1, 0)).Size(new Vector2(0, 32));

            Tab = new SelectionRow() { Column = 3 };
            Tab.Position(new Vector2(0, 32));
            Tab.Size(new Vector2(0, 48)).RelativeTransform(new Rect(0, 0, 1, 0));

            var Content = new Area();
            Content.Padding(new Vector4(0, 80, 0, 0));

            for (int i = 0; i < Tabs.Count; i++)
            {
                Tab.Add(new Toggle(
                    new Area(
                        new Box() { ColorA = o.NormalColor },
                        new Label(Tabs.ElementAt(i).Key, o.TextMiddleLeft)
                     ),
                    new Area(
                        new Box() { ColorA = o.ContentColor },
                        new Label(Tabs.ElementAt(i).Key, o.TextMiddleLeft)
                    )
                ));
                Content.Add(Tabs.ElementAt(i).Value);
            }

            Add(SearchBar, Tab, Content);
        }

        void SearchGUI(Vector2 size)
        {
            searchQuerry = Search.OnGUI(new Rect(Vector2.zero,size), searchQuerry);
        }

        public override void Draw()
        {
            GUILayout.BeginArea (Transform);
            searchQuerry = Search.OnGUI ( SearchBar.Transform, searchQuerry );
            GUILayout.EndArea();

            for (int i = 0; i < Tabs.Count; i++)
                {
                    Tabs.ElementAt(i).Value.on = false;
                    Tabs.ElementAt(i).Value.Filter(searchQuerry);
                }

            if (Tab.selected >= 0)
                Tabs.ElementAt(Tab.selected).Value.on = true;

            base.Draw();
        }

        public abstract class LibraryTab : AreaList
        {
            public abstract void Filter (string search);
        }
    }

    class ActionLibrary1 : ActionLibrary.LibraryTab
    {
        List<Element> ActionDefinitions;
        List<string> ActionNames;

        public ActionLibrary1(Type[] ActionTypes)
        {
            ActionDefinitions = new List<Element>();
            ActionNames = new List<string>();

            foreach (var t in ActionTypes)
            {
                var a = new Area(
                    new Box() { ColorA = o.NormalColor, ColorB = o.BorderColor, BorderWidth = 1 },
                    new Label(t.Name, o.TextMiddleLeft).Padding(new Vector4(2, 2, 0, 0))
                 ).RelativeTransform(new Rect(0, 0, 1, 0)).Size(new Vector2(0, 32));

                ActionDefinitions.Add(a);
                ActionNames.Add(t.Name);
                Add(a);
            }
        }

        public override void Filter(string search)
        {
            for (int i = 0; i < ActionNames.Count; i++)
            {
                if (ActionNames[i].ToLower().Contains(search.ToLower()))
                    ActionDefinitions[i].on = true;
                else
                    ActionDefinitions[i].on = false;
            }
        }
    }

    class BlackboardLibrary : ActionLibrary.LibraryTab
    {
        public override void Filter(string search)
        {
        }
    }
    #endregion

    #region  commands
    abstract class ActionElementBase : AreaAutoHeight
    {
        protected ActionModel Model;
        public ActionElementBase(ActionModel model)
        {
            Model = model;
        }
    }

    class ActionElement : ActionElementBase
    {
        public ActionElement(ActionModel model) : base(model)
        {
            Add (
                new Label (Model.BluePrintPaper.blueprint.GetType().Name, o.TextMiddleLeft).Size(new Vector2(0, 16)).RelativeTransform(new Rect(0, 0, 1, 0))
            );
        }
    }

    class DecoratorElement : ActionElementBase
    {
        DecoratorModel SpecialModel;
        Element SubContent;

        public DecoratorElement(DecoratorModel model) : base(model)
        {
            SpecialModel = model;

            List<Element> Elements = new List<Element>();
            for (int i = 0; i < SpecialModel.Child.Count; i++)
            {
                if (SpecialModel.Child[i] is DecoratorModel d)
                Elements.Add(new DecoratorElement(d));
                else
                Elements.Add(new ActionElement(SpecialModel.Child[i]));
            }

            SubContent = new AreaList(Elements.ToArray()).Padding(new Vector4(4, 36, 4, 4));

            Add ( 
                new Area (
                        new Box() { ColorA = o.NormalColor, ColorB = o.BorderColor, BorderWidth = 2 },
                        new AreaRow (
                                new Toggle (
                                    new Area (
                                        new Box() { ColorA = Color.clear, ColorB = o.BorderColor, BorderWidth = 1 },
                                        new Label ( "+", o.TextMiddle )
                                        ),
                                    new Area (
                                        new Box() { ColorA = Color.clear, ColorB = o.BorderColor, BorderWidth = 1 },
                                        new Label ( "-", o.TextMiddle )
                                        )
                                    ) { OnEnable=Fold, OnDisable=Unfold }.Size(new Vector2(32, 0)).RelativeTransform(new Rect(0, 0, 0, 1)).Padding(new Vector4(2, 2, 2, 2)),
                                new Label(Model.BluePrintPaper.blueprint.GetType().Name, o.TextMiddleLeft).FitContentW()
                                ).Padding (new Vector4(2, 2, 2, 2))
                    ).RelativeTransform(new Rect(0, 0, 1, 0)).Size(new Vector2(0, 32)),
                new Box() { ColorA = Color.clear, ColorB = o.BorderColor, BorderWidth = 2 },
                SubContent
                 );
        }

        void Unfold()
        {
            SubContent.on = true;
        }

        void Fold()
        {
            SubContent.on = false;
        }
    }
    #endregion

}
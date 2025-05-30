using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;
using Pixify.Editor.NGUI;
using static Pixify.Editor.ScriptEditorStyles;
using UnityEngine.UI;

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

        Area Library;
        IMGUI nodeProperties;
        void OnEnable()
        {
            Library = new AreaRelativePadded (  ) { Pad = new Vector4(4, 4, 32, 4) };
            new ActionLibrary(Library);

            nodeProperties = new IMGUI(NodePropertiesGUI);

            Visual = new AreaTree(
                new AreaFull ( 
                    new ColorFull(o.BackgroundColor),

                    new AreaRelativePadded(
                        new ColorFullA(o.NormalColor),
                        new Label(new Vector2(4, 4), "Library", o.h1),
                        Library
                    ) { RelativeFactor = new Rect(0, 0, .25f, 1), Pad = new Vector4(8, 8, 8+16, 8) },

                    new AreaRelativePadded(
                        new ColorFullA(o.NormalColor),
                        new Label(new Vector2(4, 4), "Commands", o.h1)
                    ) { RelativeFactor = new Rect ( .25f, 0, .5f , 1 ), Pad = new Vector4(8, 8, 8+16, 8) },

                    new AreaRelativePadded(
                        new ColorFullA(o.NormalColor),
                        new Label(new Vector2(4, 4), "Properties", o.h1),
                        new AreaRelativePadded (
                            new ColorFullB(o.ContentColor, o.BorderColor),
                            new Scroll ( nodeProperties )
                        ) { RelativeFactor = new Rect ( 0, 0, 1, 1 ), Pad = new Vector4(8, 8, 8+16, 8) }
                    ) { RelativeFactor = new Rect ( .75f, 0, .25f , .5f ), Pad = new Vector4(8, 8, 8+16, 8) },

                    new AreaRelativePadded(
                        new ColorFullA(o.NormalColor),
                        new Label(new Vector2(4, 4), "Description", o.h1)
                    ) { RelativeFactor = new Rect ( .75f, .5f, .25f , .5f ), Pad = new Vector4(8, 8, 8, 8+32) }
                )
            );
        }

        void OnGUI()
        {
            Visual.Draw();
        }

        void NodePropertiesGUI()
        {}
    }

    #region Library
    class ActionLibrary
    {
        Area Visual;
        Dictionary <string, LibraryTab> Tabs;
        SearchField Search;
        LibraryTab SelectedTab;
        Area TabContent;

        public ActionLibrary (Area visual)
        {
            Visual = visual;
            CreateTabsData ();
            CreateVisual ();
        }

        void CreateTabsData ()
        {
            Tabs = new Dictionary<string, LibraryTab>();

            var Types = Cursor.FetchTypesByCategory(typeof(action));

            Tabs = new Dictionary<string, LibraryTab>
            {
                { "decorator", new ActionLibrary1(Types["decorator"]) },
                { "blackboard", new BlackboardLibrary(this) }
            };

            for (int i = 0; i < Types.Count; i++)
            {
                var Key = Types.ElementAt(i).Key;
                if ( !Tabs.ContainsKey(Key) )
                Tabs.Add(Key, new ActionLibrary1(Types[Key]));
            }
        }

        void CreateVisual ()
        {
            Search = new SearchField(this) {height = 24};
            
            TabButton = new List<ButtonToggle>();
            var TabBar = new AreaList(0, 0, 32);

            float h = 32;
            for (int i = 0; i < (Tabs.Count / 3f) ; i++)
            {
                var TabBarRow = new AreaPaddedHor() { y = i * 16, height = 16 };
                for (int j = 0; j < 3; j++)
                {
                    if (j + (i*3) < Tabs.Count)
                    {
                        var button = new ButtonToggle(Tabs.ElementAt(j + (i*3)).Key, o.h2, o.BorderColor, o.ContentColor);
                        button.OnClick = () => ToogleTabButton(button);
                        TabButton.Add(button);

                        TabBarRow.Add(
                            new AreaRelative( button )
                            { RelativeFactor = new Rect(j / 3f, 0, 1 / 3f, 1) }
                        );
                    }
                }
                TabBar.Add(TabBarRow);
                h += 16;
            }

            TabContent = new AreaRelativePadded() {  Pad = new Vector4(0,0,h,0) };

            Visual.Add ( Search, TabBar, TabContent );
        }

        List <ButtonToggle> TabButton;
        void ToogleTabButton ( ButtonToggle button )
        {
            for (int i = 0; i < TabButton.Count; i++)
            {
                TabButton[i].on = false;
            }
            button.on = true;
            SelectedTab = Tabs [button.text];

            TabContent.Clear();
            TabContent.Add(SelectedTab.Visual);
        }

        class SearchField : ElementPaddedHor
        {
            ActionLibrary Main;
            UnityEditor.IMGUI.Controls.SearchField Search;
            public string searchQuerry = "";

            public SearchField  (ActionLibrary Main)
            {
                this.Main = Main;
                Search = new UnityEditor.IMGUI.Controls.SearchField();
            }

            public override void Draw()
            {
                searchQuerry = Search.OnGUI( Rect, searchQuerry);
                if (Main.SelectedTab != null)
                Main.SelectedTab.Filter (searchQuerry);
            }
        }

        public abstract class LibraryTab 
        {
            public Area Visual { get; protected set; }

            public abstract void Filter (string search);
        }
    }

    class ActionLibrary1 : ActionLibrary.LibraryTab
    {
        List<ActionDefinition> actionDefinitions;
        public ActionLibrary1 (Type[] ActionTypes)
        {
            actionDefinitions = new List<ActionDefinition> ();

            var content = new AreaList(0, 0, 0);
            Visual = new AreaFull(new Scroll (content));
            foreach (var t in ActionTypes)
            {
                var a = new ActionDefinition(t);
                content.Add(a);
                actionDefinitions.Add(a);
            }
        }

        public sealed override void Filter (string search)
        {
            foreach (var a in actionDefinitions)
            {
                if (a.ActionType.Name.ToLower().Contains(search.ToLower()))
                a.height = 32;
                else
                a.height = 0;
            }
        }

        class ActionDefinition : ElementPaddedHor
        {
            public Type ActionType;
            public ActionDefinition (Type ActionType)
            {
                this.ActionType = ActionType;
            }

            public override void Draw()
            {
                DrawBorder (o.BorderColor, 1);
                GUI.Label (new Rect (Rect.x + 2, Rect.y + 2, Rect.width, Rect.height - 2), ActionType.Name, o.TextMiddleLeft);
            }
        }
    }

    class BlackboardLibrary : ActionLibrary.LibraryTab
    {
        public BlackboardLibrary(ActionLibrary Main)
        {
            Visual = new AreaList(0, 0, 0);
        }

        public override void Filter(string search)
        {
        }
    }
    #endregion

    #region  commands
    abstract class ActionElementBase : AreaPaddedHor
    {
        Area Visual;
    }

    class DecoratorElement : ActionElementBase
    {
        DecoratorModel Model;

        public DecoratorElement (  )
        {
            
        }
    }
    #endregion
}
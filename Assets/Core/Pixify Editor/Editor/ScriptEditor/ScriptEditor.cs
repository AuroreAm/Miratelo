using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Linq;
using Pixify.Editor.PixGUI;
using static Pixify.Editor.ScriptEditorStyles;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;
using NUnit.Framework;
using log4net.Core;
using System.Reflection;

namespace Pixify.Editor
{

    // new version of script editor with advanced features
    public class ScriptEditor : EditorWindow
    {
        public static ScriptEditor CurrentWindow;
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (!(obj is ScriptModel s)) return false;

            Load(s);
            return true;
        }

        public static void Load(ScriptModel s)
        {
            var o = GetWindow<ScriptEditor>();
            CurrentWindow = o;
            o.Target = s;
            o.Main = new CommandList(Instantiate(s));
            o.CreateVisual();
        }

        ScriptModel Target;
        AreaTree Visual;
        CommandList Main;

        void CreateVisual()
        {
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
                            new Area(Main).Padding(new Vector4(4, 4, 4, 4))
                        ).RelativeTransform(new Rect(0, 0, 1, 1)).Padding(new Vector4(8, 8 + 16, 8, 8))
                    ).RelativeTransform(new Rect(.25f, 0, .5f, 1)).Padding(new Vector4(8, 8 + 16, 8, 8)),

                    new Area(
                        new Box() { ColorA = o.NormalColor },
                        new Label("Properties", o.h1).FitContent().Position(new Vector2(2, 2)),
                        new Area(
                            new Box() { ColorA = o.ContentColor, ColorB = o.BorderColor, BorderWidth = 1 },
                            new Properties().Padding(new Vector4(8, 8, 8, 8))
                        ).RelativeTransform(new Rect(0, 0, 1, 1)).Padding(new Vector4(8, 8 + 16, 8, 8))
                    ).RelativeTransform(new Rect(.75f, 0, .25f, .5f)).Padding(new Vector4(8, 8 + 16, 8, 8)),

                    new Area(
                        new Box() { ColorA = o.NormalColor },
                        new Label("Description", o.h1).FitContent().Position(new Vector2(2, 2))
                    ).RelativeTransform(new Rect(.75f, .5f, .25f, .5f)).Padding(new Vector4(8, 8, 8, 8 + 32)),

                    new Button ( 
                        new Area (
                            new Box() { ColorA = o.NormalColor, ColorB = o.BorderColor, BorderWidth = 1 },
                            new Label("Save", o.TextMiddle) )
                        , Save 
                    ).RelativeTransform ( new Rect (.875f, 1, .125f, 0)). Position ( new Vector2 (0, -32)).Size ( new Vector2 (0, 32)).Padding ( new Vector4 (1, 1, 1, 1))
                )
            );
        }

        void Save ()
        {
            Target.Root = Main.script.Root.Copy ();
            EditorUtility.SetDirty(Target);
            Close ();
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
            searchQuerry = Search.OnGUI(new Rect(Vector2.zero, size), searchQuerry);
        }

        public override void Draw()
        {
            GUILayout.BeginArea(Transform);
            searchQuerry = Search.OnGUI(SearchBar.Transform, searchQuerry);
            GUILayout.EndArea();

            base.Draw();

            for (int i = 0; i < Tabs.Count; i++)
            {
                Tabs.ElementAt(i).Value.on = false;
                Tabs.ElementAt(i).Value.Filter(searchQuerry);
            }

            if (Tab.selected >= 0)
                Tabs.ElementAt(Tab.selected).Value.on = true;
        }

        public abstract class LibraryTab : AreaList
        {
            public abstract void Filter(string search);
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
                Type ActionType = t;
                var a = new Button(new Area(
                    new Box() { ColorA = o.NormalColor, ColorB = o.BorderColor, BorderWidth = 1 },
                    new Label(t.Name, o.TextMiddleLeft).Padding(new Vector4(2, 2, 0, 0))
                 ), () => InsertAction(ActionType)
                ).RelativeTransform(new Rect(0, 0, 1, 0)).Size(new Vector2(0, 32));

                ActionDefinitions.Add(a);
                ActionNames.Add(t.Name);
                Add(a);
            }
        }

        void InsertAction(Type t)
        {
            CommandList.o.InsertAction(CreateActionModel(t));
        }

        ActionModel CreateActionModel(Type t)
        {
            if (t.IsSubclassOf(typeof(decorator)))
            {
                var d = new DecoratorModel();
                d.BluePrintPaper.Set(t);
                return d;
            }
            else
            {
                var a = new ActionModel();
                a.BluePrintPaper.Set(t);
                return a;
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
    class CommandList : Area
    {
        
        public ScriptModel script { get; private set; }
        public static CommandList o;
        
        bool IsDragging;
        
        Scroll Scroll;
        RenameSquare RenameSquare;
        
        ActionModel Clipboard;
        public ActionElementBase Context { get; private set; }
        public ActionElementBase Root { get; private set; }

        public CommandList(ScriptModel script)
        {
            o = this;
            this.script = script;

            if (script.Root != null)
                CreateRoot();
        }

        public void InsertAction(ActionModel model)
        {
            if (Context == null && script.Root == null)
            {
                script.Root = model;
                CreateRoot();
            }
            else if (Context != null)
            {
            UndoRecord ( "Insert Action" );
            if (Context is DecoratorElement d && d.IsExpanded)
            SelectContext ( d.InsertNewActionElementFromModel ( model ) );
            else
            SelectContext ( Context.parent.InsertNewActionElementFromModel ( model, Context.IndexInParent () + 1 ) );
            ScriptEditor.CurrentWindow.Repaint ();
            }
        }

        void CreateRoot()
        {
            Root = ActionElementBase.CreateActionElementFromModel(script.Root);
            Scroll = new Scroll(Root);
            Add(Scroll);
        }

        public void SelectContext(ActionElementBase element)
        {
            Context = element;
        }

        public override void Draw()
        {
            // get control for inside and outside the editor windows event
            int controlId = GUIUtility.GetControlID(FocusType.Passive);
            Event e;
            e = Event.current;

            base.Draw();

            Vector2 pos = GUIUtility.GUIToScreenPoint(e.mousePosition);

            // select and start dragging action element
            if (e.type == EventType.MouseDown && Root is DecoratorElement d)
            {
                // check if the mouse is inside an action element, iterate the hierarchy from the last level to the first
                var a = d.GetElementHovered(pos);

                if (a != null)
                {
                    SelectContext(a);
                    IsDragging = true;
                    e.Use();
                    return;
                }
            }

            // dragging action element
            if (IsDragging && Root is DecoratorElement d1)
            {
                EditorGUIUtility.AddCursorRect(new Rect(0, 0, Screen.width, Screen.height), MouseCursor.Pan);

                // use the event drag to cancel other events
                if (e.type == EventType.MouseDrag)
                {
                    GUIUtility.hotControl = controlId;
                    e.Use();
                }

                var a = d1.GetElementHovered(pos);
                if (a != null && a != Context)
                {
                    Rect CurrentRect = a.GlobalTransform;
                    if (pos.y < CurrentRect.y + CurrentRect.height / 2)
                        DrawPreview(new Rect(CurrentRect.x, CurrentRect.y, CurrentRect.width, 2));
                    else
                        DrawPreview(new Rect(CurrentRect.x, CurrentRect.y + CurrentRect.height - 2, CurrentRect.width, 2));
                }

                void DrawPreview(Rect GlobalRect)
                {
                    // revert GlobalRect to GUI space
                    Rect rect = GUIUtility.ScreenToGUIRect(GlobalRect);
                    EditorGUI.DrawRect(rect, Color.red);
                }

                // stop dragging when mouse up using the control id we got earlier
                if (e.GetTypeForControl(controlId) == EventType.MouseUp)
                {
                    IsDragging = false;
                    if (a != null && a != Context)
                    {
                    Rect CurrentRect = a.GlobalTransform;
                    bool up = pos.y < CurrentRect.y + CurrentRect.height / 2;
                    ApplyDragging (a, up );
                    }
                    e.Use();
                }

                void ApplyDragging ( ActionElementBase TargetContext, bool up )
                {
                    UndoRecord ( "Move Action" );
                    // discard if the TargetContext is a descendant of the Context
                    if (Context is DecoratorElement d && TargetContext.IsDescendantOf(d)) return;

                    if (TargetContext is DecoratorElement d1)
                    {
                        if ( up && (TargetContext.parent != null) )
                        Context.SetParent ( TargetContext.parent, TargetContext.IndexInParent () );
                        else
                        Context.SetParent ( d1 );
                    }

                    else if (TargetContext.parent != null)
                    Context.SetParent ( TargetContext.parent, TargetContext.IndexInParent () + ( up ? 0 : 1) );
                }
            }

            // duplicate action element CTRL+D
            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.D && e.control && Context != null && Context.parent != null )
            {
                UndoRecord ( "Duplicate Action" );
                var Copy = Context.Model.Copy ();
                SelectContext ( Context.parent.InsertNewActionElementFromModel ( Copy, Context.IndexInParent () + 1 ) );
                ScriptEditor.CurrentWindow.Repaint ();
                e.Use ();
            }

            // delete action element DEL
            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.Delete && Context != null && Context.parent != null )
            {
                UndoRecord ( "Delete Action" );
                Context.parent.RemoveActionElement ( Context );
                ScriptEditor.CurrentWindow.Repaint ();
                SelectContext ( null );
                e.Use ();
            }

            // cut action element CTRL+X
            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.X && e.control && Context != null && Context.parent != null )
            {
                Clipboard = Context.Model.Copy ();
                UndoRecord ( "Cut Action" );
                Context.parent.RemoveActionElement ( Context );
                ScriptEditor.CurrentWindow.Repaint ();
                SelectContext ( null );
                e.Use ();
            }

            // copy action element CTRL+C
            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.C && e.control && Context != null )
            {
                Clipboard = Context.Model.Copy ();
                e.Use ();
            }

            // paste action element CTRL+V
            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.V && e.control && Clipboard != null && Context != null && Context.parent != null )
            {
                UndoRecord ( "Paste Action" );
                var Copy = Clipboard.Copy ();
                if (Context is DecoratorElement d2 && d2.IsExpanded)
                SelectContext ( d2.InsertNewActionElementFromModel ( Copy ) );
                else
                SelectContext ( Context.parent.InsertNewActionElementFromModel ( Copy, Context.IndexInParent () + 1 ) );
                ScriptEditor.CurrentWindow.Repaint ();
                e.Use ();
            }

            // undo action element CTRL+Z
            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.Z && e.control )
            {
                Undo ();
                e.Use ();
            }

            // Rename Tag F2
            if ( e.type == EventType.KeyDown && e.keyCode == KeyCode.F2 && RenameSquare == null )
            {
                RenameSquare = new RenameSquare ( Context );
                RenameSquare.RelativeTransform ( new Rect ( 0.4f, 0.45f, 0.2f, 0.1f ) );
                Add ( RenameSquare );
                e.Use ();
            }
        }

        public void CancelRenameTag ()
        {
            Remove ( RenameSquare );
            RenameSquare = null;
            ScriptEditor.CurrentWindow.Repaint ();
        }

        public void ConfirmRenameTag ( string Tag )
        {
            if ( Context == null ) return;

            UndoRecord ( "Rename Tag" );
            Context.Model.Tag = RenameSquare.Name;
            Remove ( RenameSquare );
            RenameSquare = null;
            ScriptEditor.CurrentWindow.Repaint ();
        }

        
        // because unity undo redo is hard to predict and requires serialized assets to work, this is a custom undo stack for command list that is not integrated with unity
        Stack<Record> undoStack = new Stack<Record>();

        void UndoRecord ( string name )
        {
            List<int> PathToContext = new List<int>();
            ActionElementBase contextRecord = null;
            if (Context != null)
            {
                RecursiveFindPath ( Context );

                void RecursiveFindPath ( ActionElementBase a )
                {
                    int index = a.IndexInParent ();
                    if (index != -1)
                    {
                        PathToContext.Add ( index );
                        RecursiveFindPath ( a.parent );
                    }
                }
            }

            var scriptRecord = ScriptableObject.Instantiate (script);
            var rootRecord = ActionElementBase.CreateActionElementFromModel(scriptRecord.Root);

            if (rootRecord is DecoratorElement d && PathToContext.Count > 0)
            {
                DecoratorElement currentLevel = d;
                // find the path to the context
                for (int i = PathToContext.Count - 1; i > 0; i--)
                {
                    currentLevel = (DecoratorElement) currentLevel.SubContent.Children[PathToContext[i]];
                }
                contextRecord = (ActionElementBase) currentLevel.SubContent.Children[PathToContext[0]];
            }
            
            undoStack.Push ( new Record { name = name, script = scriptRecord, context = contextRecord, root = rootRecord } );
        }

        void Undo ()
        {
            if (undoStack.Count == 0) return;

            var record = undoStack.Pop ();
            script = record.script;
            Context = record.context;
            Root = record.root;

            
            var Scroll2 = new Scroll(Root);
            Scroll2.spos = Scroll.spos;
            Remove (Scroll);
            Scroll = Scroll2;
            Add(Scroll);
        }

        struct Record
        {
            public string name;
            public ScriptModel script;
            public ActionElementBase context;
            public ActionElementBase root;
        }
    }


    [Serializable]
    abstract class ActionElementBase : AreaAutoHeight
    {
        public ActionModel Model { get; private set; }
        public Rect GlobalTransform { get; private set; }

        public DecoratorElement parent { get; private set; }
        
        protected Label label;
        protected string Description;

        public ActionElementBase(ActionModel model)
        {
            Model = model;
            label = new Label ( "", o.TextMiddleLeftX );

            if ( model.BluePrintPaper.blueprint.GetType().GetCustomAttribute<NodeDescriptionAttribute>() != null )
            Description = model.BluePrintPaper.blueprint.GetType().GetCustomAttribute<NodeDescriptionAttribute>().Description;
        }

        public void SetParent(DecoratorElement parent)
        {
            if (this.parent != null)
                {
                    this.parent.SubContent.Remove(this);
                    this.parent.SpecialModel.Child.Remove(this.Model);
                }

            this.parent = parent;

            if (parent != null)
                {
                parent.SubContent.Add(this);
                parent.SpecialModel.Child.Add(this.Model);
                }
        }

        public void SetParent(DecoratorElement parent, int index)
        {
            // if the parent is the same, the index needs some shifting because removing the element from the old parent will shift the index if it is later in the list
            if ( parent != null && parent == this.parent )
                {
                    if ( index > IndexInParent() )
                    index --;
                }

            SetParent(parent);

            if (parent != null)
            {
                // insert it into the desired index, and remove it from the old parent
                parent.SubContent.Children.Insert(index, this);
                parent.SubContent.Children.RemoveAt(parent.SubContent.Children.Count - 1);
                parent.SpecialModel.Child.Insert(index, Model);
                parent.SpecialModel.Child.RemoveAt ( parent.SpecialModel.Child.Count - 1);
            }
        }

        public int IndexInParent()
        {
            if (parent == null) return -1;
            return parent.SubContent.Children.IndexOf(this);
        }

        override public void Draw()
        {
            string hexNodeTintColor = ColorUtility.ToHtmlStringRGB ( Model.BluePrintPaper.blueprint.GetType().GetCustomAttribute<NodeTintAttribute>().Tint );

            label.text = string.Concat ( $"<color=#{hexNodeTintColor}>{Model.BluePrintPaper.blueprint.GetType().Name}</color> -",Model.Tag,"-", $"<color=#aaaaaa> {Description} </color>", $"<color=#aaaaee> { ((action) Model.BluePrintPaper.blueprint).GetAdditionalInfo() }</color>" );

            base.Draw();
            GlobalTransform = GUIUtility.GUIToScreenRect(Transform);
        }

        /// <summary>
        /// Check if the element is hovered by the mouse
        /// </summary>
        /// <param name="pos"> mouse position in screen space </param>
        /// <returns></returns>
        public bool IsHovered(Vector2 pos)
        {
            return GlobalTransform.Contains(pos);
        }

        public bool IsDescendantOf ( DecoratorElement D )
        {
            if ( parent == null ) return false;

            bool descendant = parent == D;

            if ( !descendant )
            descendant = parent.IsDescendantOf ( D );

            return descendant;
        }

        public static ActionElementBase CreateActionElementFromModel(ActionModel model)
        {
            if (model is DecoratorModel d)
                return new DecoratorElement(d);
            else
                return new ActionElement(model);
        }
    }

    class ActionElement : ActionElementBase
    {
        public ActionElement(ActionModel model) : base(model)
        {
            Add(
                label.Size(new Vector2(0, 16)).RelativeTransform(new Rect(0, 0, 1, 0))
            );
        }
        
        override public void Draw()
        {
            if (this == CommandList.o.Context)
                EditorGUI.DrawRect(Transform, Color.gray);
            base.Draw();
        }
    }

    class DecoratorElement : ActionElementBase
    {
        public DecoratorModel SpecialModel { get; private set; }
        public Area SubContent { get; private set; }
        public bool IsExpanded => SubContent.on;

        public DecoratorElement(DecoratorModel model) : base(model)
        {
            SpecialModel = model;
            SubContent = new AreaList();
            SubContent.Padding(new Vector4(8, 40, 8, 8));

            var ChildCopy = SpecialModel.Child;
            SpecialModel.Child = new List<ActionModel>();
            for (int i = 0; i < ChildCopy.Count; i++)
                CreateActionElementFromModel(ChildCopy[i]).SetParent(this);

            CreateVisual();
        }

        override public void Draw()
        {
            if (this == CommandList.o.Context)
                EditorGUI.DrawRect(Transform, Color.gray);
            base.Draw();
        }

        public ActionElementBase InsertNewActionElementFromModel(ActionModel model)
        {
            var a = CreateActionElementFromModel(model);
            a.SetParent(this);
            return a;
        }

        public ActionElementBase InsertNewActionElementFromModel(ActionModel model, int index)
        {
            var a = CreateActionElementFromModel(model);
            a.SetParent(this, index);
            return a;
        }

        public void RemoveActionElement(ActionElementBase a)
        {
            a.SetParent(null);
        }

        /// <summary>
        /// Get the element hovered by the mouse
        /// </summary>
        /// <param name="pos"> mouse position in screen space </param>
        /// <returns></returns>
        public ActionElementBase GetElementHovered(Vector2 pos)
        {
            ActionElementBase Hovered = null;
            for (int i = 0; i < SubContent.Children.Count; i++)
            {
                if (SubContent.Children[i] is ActionElementBase e)
                {
                    if (e.IsHovered(pos))
                        Hovered = e;
                }
            }

            if (Hovered != null)
            {
                if (Hovered is DecoratorElement d)
                Hovered = d.GetElementHovered(pos);
                return Hovered;
            }

            if (Hovered == null && IsHovered(pos))
                return this;
            else
                return null;
        }

        void CreateVisual()
        {
            Color BorderColor = Model.BluePrintPaper.blueprint.GetType().GetCustomAttribute <NodeTintAttribute>().Tint;
            Color BoxColor = new Color ( BorderColor.r, BorderColor.g, BorderColor.b, 0.2f );

            Add(
                new Area(
                        new Box() { ColorA = BoxColor, ColorB = BorderColor, BorderWidth = 2 },
                        new Area (
                                new Toggle(
                                    new Area(
                                        new Box() { ColorA = Color.clear, ColorB = BorderColor, BorderWidth = 1 },
                                        new Label("+", o.TextMiddle)
                                        ),
                                    new Area(
                                        new Box() { ColorA = Color.clear, ColorB = BorderColor, BorderWidth = 1 },
                                        new Label("-", o.TextMiddle)
                                        )
                                    )
                                { OnEnable = Fold, OnDisable = Unfold }.Size(new Vector2(32, 0)).RelativeTransform(new Rect(0, 0, 0, 1)).Padding(new Vector4(2, 2, 2, 2)),

                                label.Padding(new Vector4(34, 2, 2, 2))
                            )

                        ).RelativeTransform(new Rect(0, 0, 1, 0)).Size(new Vector2(0, 32)),

                new Box() { ColorA = Color.clear, ColorB = BorderColor, BorderWidth = 2 },
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

    class RenameSquare : Element
    {
        ActionElementBase Element;
        public string Name;

        bool focused;

        public RenameSquare(ActionElementBase element)
        {
            Element = element;
            Name = element.Model.Tag;
        }

        public override void Draw()
        {
            EditorGUI.DrawRect(Transform, new Color(.1f, .1f, .1f));
            GUILayout.BeginArea(Transform);

                GUI.Label(new Rect ( 0, 0, Transform.width, Transform.height * .5f ),"Rename TAG", o.TextMiddle);
                GUI.SetNextControlName("RenameTag");
                Name = EditorGUI.TextField(new Rect ( 0, Transform.height * .5f, Transform.width, Transform.height * .5f ), Name);

                if (!focused)
                {
                    EditorGUI.FocusTextInControl("RenameTag");
                    focused = true;
                }

                // confirm ENTER
                if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
                {
                    CommandList.o.ConfirmRenameTag ( Name );
                    Event.current.Use ();
                }

                // cancel ESC
                if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
                {
                    CommandList.o.CancelRenameTag ();
                    Event.current.Use ();
                }

            GUILayout.EndArea();
        }
    }

    #endregion

    #region properties
    public class Properties : Area
    {
        NodeEditor nE;
        ActionModel Selected;
        
        public Properties()
        {
            Add ( 
                new IMGUI ( OnGUI )
             );
        }

        Vector2 spos;
        void OnGUI ( Vector2 Size )
        {
            PrepareEditor ();


            if ( nE != null )
            nE.GUI ();
            
        }

        void PrepareEditor ()
        {
            if ( CommandList.o.Context == null )
            {
                Selected = null;
                nE = null;
            }
            else if  ( CommandList.o.Context != null && Selected != CommandList.o.Context.Model) 
            {
                Selected = CommandList.o.Context.Model;
                nE = NodeEditor.CreateEditor ( Selected.BluePrintPaper.blueprint, CommandList.o.script );
                ScriptEditor.CurrentWindow.Repaint ();
            } 
        }
    }
    #endregion
}
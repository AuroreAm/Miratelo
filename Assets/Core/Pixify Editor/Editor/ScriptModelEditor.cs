using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Linq;
using System.Collections.Generic;
using Pixify.Editor.NGUI;

namespace Pixify.Editor
{
    public class ScriptModelWindow : EditorWindow
    {
        public static ScriptModelWindow o;
        static ScriptModel Target;
        Cursor<action> Cursor;
        bool TargetHasValidRoot => Target.Root != null && Target.Root.Valid;
        ActionElement Selected;

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (!(obj is ScriptModel s)) return false;

            GetWindow<ScriptModelWindow>().Load(s);
            return true;
        }

        void Load(ScriptModel s)
        {
            o = this;
            titleContent = new GUIContent("Script Editor");
            Focus();
            Target = s;
            Cursor = new Cursor<action>(AddActionModel);
            CreateLayout();
        }

        void OnGUI()
        {
            if (Target == null)
            Close ();
            Main.Draw();
        }

        void OnEnable()
        {
            Undo.undoRedoPerformed += UndoCall;
        }

        void OnDestroy()
        {
            Undo.undoRedoPerformed -= UndoCall;
        }

        void UndoCall()
        {
            if (TargetHasValidRoot)
                Selected = CreateActionElement (Target.Root, RealActionList);
            EditorUtility.SetDirty(Target);
        }

        AreaTree Main;
        AreaRelative ActionList;
        AreaRelative ActionLibrary;
        AreaRelative Clipboard;
        AreaRelative Command;
        AreaRelative NodeInspector;

        void CreateLayout()
        {
            AreaFull Container = new AreaFull ();
            Main = new AreaTree( Container );

            ActionList = new AreaRelativePadded(new Rect(0, 0, .75f, .75f), 16);
            ActionLibrary = new AreaRelativePadded(new Rect(0, .75f, .25f, .25f), 16);
            Clipboard = new AreaRelativePadded(new Rect(.5f, .75f, .25f, .25f), 16);
            Command = new AreaRelativePadded(new Rect(.25f,.75f,.25f,.25f),16);
            NodeInspector = new AreaRelativePadded(new Rect(.75f, 0, .25f, .5f), 16);

            Container.Add (ActionList, ActionLibrary, Clipboard, Command, NodeInspector);
            
            ActionLibrary.Add(new IMGUI(Cursor.GUI));
            CreateActionListLayout();
            CreateNodeInspectorLayout();
            CreateCommandLayout();
            CreateClipboardLayout();
        }

        #region  Action List
        AreaList RealActionList;
        Scroll Scroll;
        void CreateActionListLayout()
        {
            var pad = new AreaRelativePadded(new Rect(0, 0, 1, 1), 32);

            var ActionListWithBackground = new AreaAutoHeight(0,0,0);

            var Background = new BackgroundList(16);
            RealActionList = new AreaList (0,0,0);

            ActionListWithBackground.Add(Background, RealActionList);

            Scroll = new Scroll (ActionListWithBackground);

            pad.Add (Scroll);
            ActionList.Add(new Label(new Vector2(0, 0), "Actions List ☺", PixStyle.o.Title1), pad);

            if (TargetHasValidRoot)
                Selected = CreateActionElement (Target.Root, RealActionList);
        }

        ActionElement CreateActionElement(ActionModel root, AreaList container)
        {
            container.Clear ();
            ActionElement e = new ActionElement(root, this, container);
            container.Add ( e );
            e.DropDownOn ();
            return e;
        }

        class ActionElement : ElementPaddedHor
        {
            public ActionModel Model;
            ActionElement Parent;

            ScriptModelWindow Main;
            public AreaList Container { get; private set; }
            AreaList SubContainer;
            bool DropDown;

            public ActionElement( ActionModel Model, ScriptModelWindow Main, AreaList Container ) : base(0, 0, 0, 16)
            {
                this.Model = Model;
                this.Main = Main;
                this.Container = Container;
                SubContainer = new AreaList ( 16, 0, 0 );

                if (Model is DecoratorModel d)
                {
                    foreach (var c in d.Child)
                    {
                        var a = new ActionElement(c, Main, SubContainer);
                        a.Parent = this;
                        SubContainer.Add(a);
                    }
                }
            }

            public void Insert (ActionModel actionModel)
            {
                if (Model is DecoratorModel d)
                Insert(actionModel, d.Child.Count);
                else Insert (actionModel, 0);
            }

            void Insert (ActionModel actionModel, int index)
            {
                if (Model is DecoratorModel d)
                {
                    if (index + 1 < d.Child.Count)
                    d.Child.Insert(index + 1, actionModel);
                    else d.Child.Add(actionModel);
                    
                    var a = new ActionElement(actionModel, Main, SubContainer);
                    a.Parent = this;
                    SubContainer.Insert(a, index + 1);
                    
                    if (Main.Selected != this)
                    {
                        Main.Selected = a;
                        Main.Scroll.spos.y += 16;
                    }
                }
                else if ( Parent != null )
                {
                    Parent.Insert(actionModel, Parent.SubContainer.IndexOf(this));
                }
            }

            void CreateChildElement ()
            {
                if (Parent != null)
                Container.Insert ( SubContainer, Parent.SubContainer.IndexOf(this) + 1 );
                else
                Container.Insert ( SubContainer, 1 );
            }

            public void DropDownOn ()
            {
                if (DropDown == false)
                {
                    DropDown = true;
                    CreateChildElement();
                }
            }

            public void DropDownOff ()
            {
                if (DropDown == true)
                {
                    DropDown = false;
                    Container.Remove(SubContainer);
                }
            }

            public override void Draw()
            {
                if (Main.Selected == this)
                EditorGUI.DrawRect( new Rect ( Rect.position, Rect.size + ( DropDown? SubContainer.Rect.size : new Vector2 (0,0) ) ) , new Color(.2f, .5f, .2f, .3f));

                if ( Model is DecoratorModel && DropDown)
                {
                    if (GUI.Button ( new Rect ( Rect.position, new Vector2 ( 32, 16) ), "▼" ))
                        DropDownOff ();
                }
                else if (  Model is DecoratorModel d && d.Child.Count > 0 )
                {
                    if (GUI.Button ( new Rect ( Rect.position, new Vector2 ( 32, 16) ), "►" ))
                        DropDownOn ();
                }

                if (GUI.Button(new Rect(Rect.x + 32, Rect.y, Rect.width - 64, 16), $"{Model.Tag} > {Model.BluePrintPaper.blueprint.GetType().Name}", PixStyle.o.TextMiddleLeft))
                    Main.Selected = this;

                if (GUI.Button(new Rect(Rect.x + Rect.width - 32, Rect.y, 32, 16), "✕"))
                {
                    if (Parent != null)
                    {
                        Undo.RecordObject(Target, "Remove Action");
                        if (Main.Selected == this)
                        Main.Selected = null;

                        ((DecoratorModel) Parent.Model).Child.Remove(Model);
                        Parent.SubContainer.Remove(SubContainer);
                        Parent.SubContainer.Remove(this);
                    }
                }
            }
        }
        #endregion
        
        #region  ActionLibrary
        void AddActionModel(Type t)
        {
            Type model = typeof(ActionModel);
            if (t.IsSubclassOf(typeof(decorator)))
                model = typeof(DecoratorModel);

            var newActionModel = Activator.CreateInstance(model) as ActionModel;
            newActionModel.BluePrintPaper.Set(t);

            Undo.RecordObject(Target, "Add Action");
            if (!TargetHasValidRoot)
            {
                Target.Root = newActionModel;
                Selected = CreateActionElement (Target.Root, RealActionList);
            }
            else
            {
                if (Selected != null)
                Selected.Insert (newActionModel);
            }
            EditorUtility.SetDirty(Target);
        }
        #endregion
        
        #region  NodeInspector

        void CreateNodeInspectorLayout()
        {
            var Label = new Label (new Vector2 (8, 8), "Node Inspector", PixStyle.o.Title1);
            var Pad = new AreaRelativePadded (new Rect (0, 0, 1, 1), 32);
            var Background = new ColorFull (new Color (0, .25f, .5f));

            var Inspector = new IMGUI(NodeInspectorGUI);
            var Scroll = new Scroll (Inspector);

            Pad.Add (Scroll);

            NodeInspector.Add (Background, Label, Pad);
        }

        NodeEditor nodeEditor;
        ActionElement NodeInspectorElement;
        void NodeInspectorGUI()
        {
            if ( Selected != NodeInspectorElement  )
            {
                if (Selected != null)
                {
                nodeEditor = NodeEditor.CreateEditor ( Selected.Model.BluePrintPaper.blueprint, Target );
                NodeInspectorElement = Selected;
                Repaint ();
                }
                else
                {
                nodeEditor = null;
                NodeInspectorElement = null;
                Repaint ();
                }
            }

            if (nodeEditor != null)
                {
                EditorGUI.BeginChangeCheck();
                Selected.Model.Tag = EditorGUILayout.TextField( "Tag", Selected.Model.Tag );
                if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(Target);

                nodeEditor.GUI();
                }
        }

        #endregion

        #region  Clipboard

        ActionElementClipboard ClipboardMemory;
        AreaList ClipboardAction;
        void CreateClipboardLayout()
        {
            var Label = new Label (new Vector2 (8, 8), "Clipboard", PixStyle.o.Title1);
            var Pad = new AreaRelativePadded (new Rect (0, 0, 1, 1), 32);
            ClipboardAction = new AreaList (0, 0, 0);
            var Scroll = new Scroll (ClipboardAction);
            Pad.Add (Scroll);

            Clipboard.Add (Label, Pad);
        }

        void CopyAction( ActionModel actionModel )
        {
            ClipboardAction.Clear ();
            ClipboardMemory = new ActionElementClipboard(actionModel);
            ClipboardAction.Add ( ClipboardMemory );
            ClipboardMemory.AddSubContainer ( ClipboardAction );
        }

        class ActionElementClipboard : ElementPaddedHor
        {
            public ActionModel Model;

            public ActionElementClipboard( ActionModel Model ) : base(0, 0, 0, 16)
            {
                this.Model = Model;
            }

            public void AddSubContainer (AreaList Container)
            {
                var SubContainer = new AreaList ( 16, 0, 0 );

                if (Model is DecoratorModel d)
                {
                    foreach (var c in d.Child)
                    {
                        var a = new ActionElementClipboard(c);
                        SubContainer.Add(a);

                        a.AddSubContainer(SubContainer);
                    }
                    Container.Add ( SubContainer );
                }
            }

            public override void Draw()
            {
                GUI.Box (Rect, $"{Model.Tag} > {Model.BluePrintPaper.blueprint.GetType().Name}", PixStyle.o.TextMiddleLeft);
            }
        }

        #endregion

        #region  Command
        AreaList CommandAction;

        void CreateCommandLayout()
        {
            var Label = new Label (new Vector2 (8, 8), "Command", PixStyle.o.Title1);
            var Pad = new AreaRelativePadded (new Rect (0, 0, 1, 1), 32);
            var GUI = new IMGUI(CommandGUI);
            Pad.Add (GUI);
            
            Command.Add (Label, Pad); 
        }

        void CommandGUI ()
        {
            if (GUILayout.Button ("Copy") && Selected != null)
            CopyAction (Selected.Model);

            if (GUILayout.Button ("Duplicate") && Selected != null)
            {
                Undo.RecordObject(Target, "Duplicate Action");
                Selected.Insert (Selected.Model.Copy());
            }

            if (GUILayout.Button ("Paste") && Selected != null && ClipboardMemory != null)
            {
                Undo.RecordObject(Target, "Paste Action");
                Selected.Insert (ClipboardMemory.Model.Copy());
            }
        }

        #endregion

    }
}
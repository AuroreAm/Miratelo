using Lyra;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;
using Lyra.Editor.GUI;
using static Lyra.Editor.Styles;

namespace Lyra.Editor {
    public class ScriptDebugger : EditorWindow {

        GameObject selected;
        AreaScreen screen;
        Area main;
        AreaList content;

        [MenuItem ("Window/Lyra/Script Debugger")]
        static void Init () {
            ScriptDebugger window = (ScriptDebugger) GetWindow (typeof (ScriptDebugger));
            window.Show ();
        }

        void OnGUI () {
            if (!EditorApplication.isPlaying) return;

            SelectionCheck ();

            if (screen != null)
            screen.Draw ();

            Repaint ();
        }

        void SelectionCheck (){
            if (Selection.activeGameObject && selected != Selection.activeGameObject) {
                selected = Selection.activeGameObject;
                
                if (script.crypt.contains (selected.GetInstanceID ()))
                    load (script.crypt.get ( selected.GetInstanceID () ));
            }
        }

        void load ( Dictionary <term,action> l ) {
            main = new Area(); main.Padding (new Vector4 (8,8,8,8));

            content = new AreaList ();
            foreach (var a in l.Values)
                content.Add ( ActionGUIBase.Create ( a ) );
            main.Add ( new Scroll ( content ) );

            screen = new AreaScreen (main);
        }
    }

    abstract class ActionGUIBase : AreaAutoHeight {
        public action Target;
        protected Label Label;

        public ActionGUIBase ( action a ) {
            Target = a;
            Label = new Label ("", o.TextMiddleLeft );
        }

        public override void Draw() {
            Label.text = Target.GetType ().Name;
            EditorGUI.DrawRect ( Transform, Target.on? new Color (1,1,1) : new Color (.5f,.5f,.5f) );
            base.Draw();
        }

        public static ActionGUIBase Create ( action a ) {
            if (a is decorator_kind)
                return new DecoratorGUI (a);
            return new ActionGUI (a);
        }
    }

    class ActionGUI : ActionGUIBase {
        public ActionGUI ( action a ) : base (a) {
            Add (Label.
                Size (new Vector2 (0, 16)).
                RelativeTransform (new Rect (0,0,1,0))
            );
        }
    }

    class DecoratorGUI : ActionGUIBase {
        
        Area SubContent;

        public DecoratorGUI ( action a ) : base (a) {
            action [] child = null;
            SubContent = new AreaList ();
            SubContent.Padding ( new Vector4 (16,2+16,2,2) );

            if (a is decorator d)
                child = (action []) GetChild (d);
            else if (a is task_decorator t) {
                action [] c = (action []) GetChild (t);
                child = new action [c.Length];
                for (int i = 0; i < c.Length; i++)
                    child[i] = c[i];
            } else if (a is acting aa) {
                child = (action []) GetChild (aa);
            }

            for (int i = 0; i < child.Length; i++)
                SubContent.Add (Create (child[i]));

            CreateVisual ();
        }
        
        void CreateVisual (){
            Color BorderColor = Color.cyan;

            Add (
                Label.RelativeTransform ( new Rect (0,0,1,0) ).Size (new Vector2 (0,16)),
                SubContent
            );
        }

        object GetChild(action a) {
            Type t = a.GetType();
            FieldInfo fi = null;

            while (fi == null) {
                fi = t.GetField("o", BindingFlags.NonPublic | BindingFlags.Instance);
                t = t.BaseType;
            }

            return fi.GetValue (a);
        }
    }
}
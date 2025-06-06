using System.Reflection;
using System.Collections.Generic;
using Pixify.Editor.PixGUI;
using UnityEditor;
using UnityEngine;
using static Pixify.Editor.ScriptEditorStyles;

namespace Pixify.Editor
{
     class actionPreviewBase : AreaAutoHeight
    {
        public ActionModel Model { get; private set; }
        protected Label label;
        protected string Description;
        
        protected action blueprint => Model.BluePrintPaper.blueprint as action;

        public actionPreviewBase(ActionModel A)
        {
            Model = A;
            label = new Label("", o.TextMiddleLeftX);

            if (blueprint.GetType().GetCustomAttribute<NodeDescriptionAttribute>() != null)
                Description = blueprint.GetType().GetCustomAttribute<NodeDescriptionAttribute>().Description;
        }

        override public void Draw()
        {
            string hexNodeTintColor = ColorUtility.ToHtmlStringRGB(blueprint.GetType().GetCustomAttribute<NodeTintAttribute>().Tint);

            label.text = string.Concat($"<color=#{hexNodeTintColor}>{blueprint.GetType().Name}</color> -", Model.Tag, "-", $"<color=#aaaaaa> {Description} </color>", $"<color=#aaaaee> {blueprint.GetAdditionalInfo()}</color>");

            base.Draw();
        }

        public static actionPreviewBase CreatePreview (ActionModel a)
        {
            if (a is DecoratorModel d)
                return new decoratorPreview(d);
            else
                return new actionPreview(a);
        }
    }

    class actionPreview : actionPreviewBase
    {
        public actionPreview(ActionModel a) : base(a)
        {
            Add(
                label.Size(new Vector2(0, 16)).RelativeTransform(new Rect(0, 0, 1, 0))
            );
        }
    }

    class decoratorPreview : actionPreviewBase
    {
        public DecoratorModel Special { get; private set; }
        public Area SubContent { get; private set; }

        public decoratorPreview(DecoratorModel d) : base(d)
        {
            SubContent = new AreaList();
            SubContent.Padding(new Vector4(8, 40, 8, 8));
            Special = d;

            for (int i = 0; i < d.Child.Count; i++)
                SubContent.Add(CreatePreview(d.Child[i]));

            CreateVisual();
        }

        void CreateVisual()
        {
            Color BorderColor = blueprint.GetType().GetCustomAttribute<NodeTintAttribute>().Tint;

            Add(
                new Area(
                        new Area(label.Padding(new Vector4(2, 2, 2, 2)))

                        ).RelativeTransform(new Rect(0, 0, 1, 0)).Size(new Vector2(0, 32)),

                new Box() { ColorA = Color.clear, ColorB = BorderColor, BorderWidth = 2 },
                SubContent
                );
        }
    }
}
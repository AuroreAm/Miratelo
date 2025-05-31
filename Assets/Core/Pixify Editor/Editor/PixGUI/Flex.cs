using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Editor
{
    namespace PixGUI
    {
        public class AreaAutoHeight : Area
        {
            public AreaAutoHeight ( params Element[] E) : base ( E )
            {
                DefTransform.RelativeTransform = new Rect(0, 0, 1, 0);
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize)
            {
                Vector2 size = base.GetInitSize(ParentSize);
                float h = 0;
                foreach (var e in Children)
                {
                    if (e.on)
                    {
                        e.InitRect(size);
                        if ( h < e.Transform.yMax )
                        h = e.Transform.yMax;
                    }
                }

                size = new Vector2(size.x, h + DefTransform.Padding.w);
                InitRectChildren(size);

                return size;
            }
        }

        public class AreaRow : Area
        {
            public AreaRow ( params Element[] E) : base ( E )
            {
                DefTransform.RelativeTransform = new Rect(0, 0, 0, 1);
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize)
            {
                Vector2 size = base.GetInitSize(ParentSize);
                float w = 0;
                foreach (var e in Children)
                {
                    if (e.on)
                    {
                        e.DefTransform.Position = new Vector2(w, 0);
                        e.InitRect(size);
                        w += e.Transform.width;
                    }
                }
                size = new Vector2(w + DefTransform.Padding.z, size.y);
                InitRectChildren(size);
                return size;
            }
        }

        public class AreaList : Area
        {
            public AreaList ( params Element[] E) : base ( E )
            {
                DefTransform.RelativeTransform = new Rect(0, 0, 1, 0);
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize)
            {
                Vector2 size = base.GetInitSize(ParentSize);
                float h = 0;
                foreach (var e in Children)
                {
                    if (e.on)
                    {
                        e.DefTransform.Position = new Vector2(0, h);
                        e.InitRect(size);
                        h += e.Transform.height;
                    }
                }
                size = new Vector2(size.x, h + DefTransform.Padding.w);
                InitRectChildren(size);
                return size;
            }
        }
    }
}
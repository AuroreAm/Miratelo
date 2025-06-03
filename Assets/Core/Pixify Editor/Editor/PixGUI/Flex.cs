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
            
            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                // discard calculations if the parent size height is not 0 but its defined size is
                if (Transform.height != 0 && ParentDefTransform.Size.y == 0 && ParentDefTransform.RelativeTransform.size.y == 0)
                    return Transform.size;

                    Vector2 size = DefaultInitSize(ParentSize);

                    float h = 0;
                    foreach (var e in Children)
                    {
                        if (e.on)
                        {
                            e.InitRect(size, DefTransform);
                            if ( h < e.Transform.yMax )
                            h = e.Transform.yMax;
                        }
                    }

                    size = new Vector2(size.x, h + DefTransform.Padding.w);
                    InitRectChildren(size, DefTransform);
                    return size;
            }
        }

        public class AreaRow : Area
        {
            public float Spacing;

            public AreaRow ( params Element[] E) : base ( E )
            {
                DefTransform.RelativeTransform = new Rect(0, 0, 0, 1);
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                // discard calculations if the parent size width is not 0 but its defined size is
                if (Transform.width != 0 && ParentDefTransform.Size.x == 0 && ParentDefTransform.RelativeTransform.size.x == 0)
                    return Transform.size;

                Vector2 size = DefaultInitSize(ParentSize);
                float w = 0;
                foreach (var e in Children)
                {
                    if (e.on)
                    {
                        e.DefTransform.Position = new Vector2(w, 0);
                        e.InitRect(size, DefTransform);
                        w += e.Transform.width + Spacing;
                    }
                }
                size = new Vector2(w + DefTransform.Padding.z, size.y);
                InitRectChildren(size, DefTransform);
                return size;
            }
        }

        public class AreaList : Area
        {
            public AreaList ( params Element[] E) : base ( E )
            {
                DefTransform.RelativeTransform = new Rect(0, 0, 1, 0);
            }

            protected override Vector2 GetInitSize(Vector2 ParentSize, DefTransform ParentDefTransform)
            {
                // discard calculations if the parent size height is not 0 but its defined size is
                if (Transform.height != 0 && ParentDefTransform.Size.y == 0 && ParentDefTransform.RelativeTransform.size.y == 0)
                    return Transform.size;

                Vector2 size = DefaultInitSize(ParentSize);

                float h = 0;
                foreach (var e in Children)
                {
                    if (e.on)
                    {
                        e.DefTransform.Position = new Vector2(0, h);
                        e.InitRect(size, DefTransform);
                        h += e.Transform.height;
                    }
                }
                size = new Vector2(size.x, h + DefTransform.Padding.w);
                InitRectChildren(size, DefTransform);
                return size;
            }
        }
    }
}
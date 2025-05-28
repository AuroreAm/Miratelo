using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;

namespace Pixify.Editor
{
    namespace NGUI
    {
        /// <summary>
        /// Tree of Area, use to make hierarchy of areas and GUI elements
        /// </summary>
        public class AreaTree
        {
            public Area Root;

            /// <summary>
            /// Create a new AreaTree with a root Area
            /// </summary>
            /// <param name="root"> root Area </param>
            public AreaTree(Area root)
            {
                Root = root;
            }

            /// <summary>
            /// Used to build the structure for drawing
            /// </summary>
            void ReBuild ()
            {
                // Define the size of the root area first, using screen size, (-20 is used to avoid the toolbar)
                Root.SetDefSize (new Vector2 (Screen.width, Screen.height-20));
                // Define the position of the root area, using screen size, (-20 is used to avoid the toolbar)
                Root.SetDefPos (new Vector2 (Screen.width, Screen.height-20));

                Root.InitRect ();
            }

            public void Draw()
            {
                // Build the structure for drawing for every repaint
                ReBuild ();
                Root.Draw();
            }
        }

        /// <summary>
        /// Base class for all GUI elements
        /// </summary>
        public abstract class Element
        {
            /// <summary>
            /// real local position and size of this element in the AreaTree after the AreaTree is structured
            /// </summary>
            public Rect Rect { get; private set; }
            /// <summary>
            /// defined local size of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            protected Vector2 DefSize { get; private set; }
            /// <summary>
            /// define local position of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            protected Vector2 DefPos { get; private set; }
            /// <summary>
            /// real local position and size of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            public Rect DefRect => new Rect ( DefPos, DefSize );

            /// <summary>
            /// Move the defined position of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            /// <param name="Dir"></param>
            public void TweakDefPos ( Vector2 Dir )
            {
                DefPos += Dir;
            }

            /// <summary>
            /// Get the definition position of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            /// <param name="DefParentSize"> The definition size of parent element </param>
            internal void SetDefPos ( Vector2 DefParentSize )
            {
                DefPos = GetDefPos ( DefParentSize );
            }

            /// <summary>
            /// Get the definition size of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            /// <param name="DefParentSize"> The definition size of parent element </param>
            internal void SetDefSize ( Vector2 DefParentSize )
            {
                DefSize = GetDefSize ( DefParentSize );
            }

            internal virtual void InitRect ()
            {
                Rect = DefRect;
            }

            /// <summary>
            /// implement this to define the position of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            /// <param name="DefParentSize"> given definition size of parent element </param>
            /// <returns> desired position </returns>
            protected abstract Vector2 GetDefPos ( Vector2 DefParentSize );
            /// <summary>
            /// implement this to define the size of this element in the AreaTree before the AreaTree is structured
            /// </summary>
            /// <param name="DefParentSize"> given definition size of parent element </param>
            /// <returns> desired size </returns>
            protected abstract Vector2 GetDefSize ( Vector2 DefParentSize);

            /// <summary>
            /// implementation of this element GUI drawing
            /// </summary>
            public abstract void Draw();
        }

        /// <summary>
        /// Element to make hierarchy of elements
        /// </summary>
        public abstract class Area : Element
        {
            protected List<Element> Children = new List<Element> ();
            public sealed override void Draw()
            {
                GUILayout.BeginArea ( Rect );
                int ChildrenCount = Children.Count;

                for (int i = 0; i < ChildrenCount; i++)
                {
                    Children [i].Draw ();

                    if (ChildrenCount != Children.Count)
                        break;
                }

                GUILayout.EndArea ();
            }

            public void Add ( params Element[] E )
            {
                foreach (var e in E)
                Children.Add (e);
            }

            public void Insert ( Element e, int index )
            {
                if (index < Children.Count)
                Children.Insert (index, e);
                else Children.Add (e);
            }

            public void Remove ( Element e )
            {
                Children.Remove (e);
            }

            public int IndexOf ( Element e )
            {
                return Children.IndexOf (e);
            }

            public void Clear ()
            {
                Children.Clear ();
            }

            internal override void InitRect()
            {
                base.InitRect();
                foreach (var e in Children)
                    e.InitRect();
            }

            /// <summary>
            /// Set the definition position of children elements in the AreaTree before the AreaTree is structured, this needs to be called at least once to create the structure position of children
            /// </summary>
            protected void SetDefPosChildren (Vector2 DefParentSize)
            {
                foreach (var e in Children)
                e.SetDefPos ( DefParentSize );
            }

            /// <summary>
            /// Set the definition size of children elements in the AreaTree before the AreaTree is structured, this needs to be called at least once to create the structure to know the size of children
            /// </summary>
            protected void SetDefSizeChildren (Vector2 DefParentSize)
            {
                foreach (var e in Children)
                e.SetDefSize ( DefParentSize );
            }
        }

        public class AreaFull : Area
        {
            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                SetDefPosChildren ( DefParentSize );
                return Vector2.zero;
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                SetDefSizeChildren( DefParentSize );
                return DefParentSize;
            }
        }

        public class AreaRelative : Area
        {
            protected Rect RelativeFactor;

            public AreaRelative (Rect factor)
            {
                RelativeFactor = factor;
            }

            protected override Vector2 GetDefPos (Vector2 DefParentSize)
            {
                Rect area = new Rect (new Vector2 ( DefParentSize.x * RelativeFactor.x, DefParentSize.y * RelativeFactor.y ), new Vector2 ( DefParentSize.x * RelativeFactor.width, DefParentSize.y * RelativeFactor.height ) );

                SetDefPosChildren (area.size);

                return area.position;
            }

            protected override Vector2 GetDefSize (Vector2 DefParentSize)
            {
                Vector2 size =  new Vector2 ( DefParentSize.x * RelativeFactor.width, DefParentSize.y * RelativeFactor.height );

                SetDefSizeChildren (size);

                return size;
            }
        }

        public class AreaRelativePadded : AreaRelative
        {
            public float Pad;

            public AreaRelativePadded ( Rect factor, float pad ) : base ( factor )
            {
                Pad = pad;
            }

            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                Rect area = new Rect (new Vector2 ( DefParentSize.x * RelativeFactor.x + Pad, DefParentSize.y * RelativeFactor.y + Pad ), new Vector2 ( DefParentSize.x * RelativeFactor.width, DefParentSize.y * RelativeFactor.height ) );
                SetDefPosChildren (area.size);
                return area.position;
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                Vector2 size =  new Vector2 ( DefParentSize.x * RelativeFactor.width - Pad * 2, DefParentSize.y * RelativeFactor.height - Pad * 2 );
                SetDefSizeChildren (size);
                return size;
            }
        }

        /// <summary>
        /// Area automatically set height to ymax of children, don't add elements that rely on height of this
        /// </summary>
        public class AreaAutoHeight : Area
        {
            public float PaddingLeft;
            public float PaddingRight;
            public float y;

            public AreaAutoHeight (float paddingLeft, float paddingRight, float y)
            {
                PaddingLeft = paddingLeft;
                PaddingRight = paddingRight;
                this.y = y;
            }

            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                return new Vector2 ( PaddingLeft, y );
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                Vector2 defSize = new Vector2 (  DefParentSize.x - PaddingLeft - PaddingRight, 0 );
                float h = 0;

                foreach (var n in Children)
                {
                    n.SetDefSize (defSize);
                    n.SetDefPos (defSize);
                    if ( h < n.DefRect.yMax )
                    h = n.DefRect.yMax;
                }

                defSize = new Vector2 ( DefParentSize.x - PaddingLeft - PaddingRight, h );

                SetDefSizeChildren (defSize);

                return defSize;
            }
        }

        public abstract class ElementPaddedHor : Element
        {
            public float PaddingLeft;
            public float PaddingRight;
            public float y;
            public float height;
            public ElementPaddedHor(float paddingLeft, float paddingRight, float y, float height)
            {
                PaddingLeft = paddingLeft;
                PaddingRight = paddingRight;
                this.y = y;
                this.height = height;
            }

            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                return new Vector2 (PaddingLeft, 0);
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                return new Vector2 ( DefParentSize.x - PaddingLeft - PaddingRight, height );
            }
        }

        public abstract class ELementFull : Element
        {
            protected override Vector2 GetDefPos(Vector2 DefParentSize)
            {
                return Vector2.zero;
            }

            protected override Vector2 GetDefSize(Vector2 DefParentSize)
            {
                return DefParentSize;
            }
        }

    }
}
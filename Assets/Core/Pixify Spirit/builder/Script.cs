using System;

namespace Pixify.Spirit
{
    public class Script : ActionPaper
    {
        public action WriteTree ( block b )
        {
            return GetAction (this, b);
        }

        action GetAction ( ActionPaper P , block c )
        {
            if ( !string.IsNullOrEmpty (P.paper.StrNodeType) && Type.GetType ( P.paper.StrNodeType ) != null &&  Type.GetType (P.paper.StrNodeType).IsSubclassOf (typeof (decorator) ) )
            {
                ActionPaper [] ChildPaper = new ActionPaper [ P.transform.childCount ];
                for (int i = 0; i < ChildPaper.Length; i++)
                    ChildPaper [i] = P.transform.GetChild (i).GetComponent <ActionPaper> ();


                action [] childs = new action [ChildPaper.Length];

                for (int i = 0; i < childs.Length; i++)
                    childs [i] = GetAction ( ChildPaper [i], c );

                decorator d = P.paper.Write () as decorator;
                d.o = childs;

                c.IntegratePix (d);
                return d;
            }

            else
            {
                action a = P.paper.Write ();
                c.IntegratePix (a);
                return a;
            }
        }

        public void Start ()
        {
            Destroy (this.gameObject);
        }
    }
}
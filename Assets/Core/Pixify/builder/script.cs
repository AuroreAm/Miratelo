using System.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify
{
    public sealed class script : ActionPaper
    {

        public action WriteTree ( Character c )
        {
            return GetAction (this, c);
        }

        action GetAction ( ActionPaper P , Character c )
        {
            if ( !string.IsNullOrEmpty (P.paper.StrNodeType) && Type.GetType ( P.paper.StrNodeType ) != null &&  Type.GetType (P.paper.StrNodeType).IsSubclassOf (typeof (decorator) ) )
            {
                ActionPaper [] ChildPaper = new ActionPaper [ P.transform.childCount ];
                for (int i = 0; i < ChildPaper.Length; i++)
                    ChildPaper [i] = P.transform.GetChild (i).GetComponent <ActionPaper> ();


                action [] childs = new action [ChildPaper.Length];

                for (int i = 0; i < childs.Length; i++)
                    childs [i] = GetAction ( ChildPaper [i], c );

                return decorator.New ( P.paper.StrNodeType, P.paper.StrNodeData, c, childs );
            }

            else
            return catom.New <action> ( P.paper.StrNodeType, P.paper.StrNodeData, c );
        }

        public void Start ()
        {
            Destroy (this);
        }
    }
}
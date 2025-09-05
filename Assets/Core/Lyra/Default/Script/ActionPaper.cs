using UnityEngine;

namespace Lyra
{
    public sealed class ActionPaper : MonoBehaviour
    {
        public moon_paper <action> Paper;

        public action write () 
        {
            var a = Paper.write ();

            if ( a is decorator d)
            {
                action [] Childs = new action [ transform.childCount ];
                for (int i = 0; i < Childs.Length; i++)
                    Childs [i] = transform.GetChild (i).GetComponent <ActionPaper> ().write ();
                d.set (Childs);
            }

            return a;
        }

        void Start () =>
            Destroy ( gameObject );
    }
}
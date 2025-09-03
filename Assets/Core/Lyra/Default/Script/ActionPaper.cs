using UnityEngine;

namespace Lyra
{
    public sealed class ActionPaper : MonoBehaviour
    {
        public DatPaper <action> Paper;

        public action GetAction () 
        {
            var a = Paper.Extract ();

            if ( a is decorator d)
            {
                action [] Childs = new action [ transform.childCount ];
                for (int i = 0; i < Childs.Length; i++)
                    Childs [i] = transform.GetChild (i).GetComponent <ActionPaper> ().GetAction ();
                d.SetChild (Childs);
            }

            return a;
        }

        void Start () =>
            Destroy ( gameObject );
    }
}
using UnityEngine;

namespace Lyra
{
    public sealed class ActPaper : MonoBehaviour
    {
        public ShardPaper <act> Paper;

        public act GetAction () 
        {
            var a = Paper.radiate ();

            if ( a is decorator d)
            {
                act [] Childs = new act [ transform.childCount ];
                for (int i = 0; i < Childs.Length; i++)
                    Childs [i] = transform.GetChild (i).GetComponent <ActPaper> ().GetAction ();
                d.leaf (Childs);
            }
            return a;
        }

        void Start () =>
            Destroy ( gameObject );
    }
}
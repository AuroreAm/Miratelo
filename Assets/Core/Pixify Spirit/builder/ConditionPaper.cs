using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class ConditionPaper : ThoughtAuthor <conditional>
    {
        [SerializeField]
        PixPaper <condition> [] paper;

        protected override conditional Get(block b)
        {
            condition [] conditions = new condition [paper.Length];

            for (int i = 0; i < paper.Length; i++)
            {
                conditions [i] = paper[i].Write ();
                b.IntegratePix ( conditions [i] );
            }

            var t = new conditional ( conditions, transform.GetChild (0).GetComponent <ThoughtAuthor>().Write (b) );

            b.IntegratePix ( t );
            return t;
        }
    }
}

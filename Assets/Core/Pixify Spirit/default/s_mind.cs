using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class s_mind : reaction
    {
        List <reflexion> Reflexions = new List<reflexion> ();

        Dictionary < term, thought.chain > ConceptThoughts = new Dictionary<term, thought.chain> ();
        public mind master {private set; get;} = new mind ();

        cortex cortex;
        public void SetCortex ( cortex newCortex )
        {
            cortex = newCortex;
            b.IntegratePix (newCortex);
            cortex.Setup ();
        }

        public override void Create()
        {
            Stage.Start (this);
        }

        protected override void Step()
        {
            foreach (var r in Reflexions)
                r.iReflex ();
        }

        public void AddReflexion ( reflexion r )
        {
            if (!Reflexions.Contains (r))
            Reflexions.Add (r);
        }

        public void ClearReflexion ()
        {
            Reflexions.Clear ();
        }

        public void AddConcepts ( params ( term, thought.chain ) [] values )
        {
            for (int i = 0; i < values.Length; i++)
                ConceptThoughts.Add ( values [i].Item1, values [i].Item2 );
        }

        public bool ThoughtExists ( term key )
        {
            return ConceptThoughts.ContainsKey ( key );
        }

        public thought.chain GetThought ( term key )
        {
            return ConceptThoughts [ key ];
        }
    }

    public class mind : thought
    {
        thought main;

        public void StartRootThought ( chain thought )
        {
            main = thought;
            thought.Aquire (this);
        }

        protected override bool OnGuestSelfFree(thought guest)
        {
            // TODO safe stop
            return false;
        }
    }
}
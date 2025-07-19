using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class s_mind : pix
    {
        List <reflexion> Reflexions = new List<reflexion> ();
        List <int> ReflexionKeys = new List<int> ();

        Dictionary < term, thought > ConceptThoughts = new Dictionary<term, thought> ();
        public mind master {private set; get;} = new mind ();

        cortex cortex;
        public void SetCortex ( cortex newCortex )
        {
            cortex = newCortex;
            b.IntegratePix (newCortex);
            TriggerThinking ();
        }

        public void TriggerThinking ()
        {
            ClearReflexion ();
            cortex.Think ();
        }

        public void AddReflexion ( reflexion r )
        {
            if (Reflexions.Contains (r))
                return;
            ReflexionKeys.Add ( Stage.Start ( r ) );
            Reflexions.Add (r);
        }

        public void ClearReflexion ()
        {
            foreach ( var i in ReflexionKeys )
            Stage.Stop ( i );

            Reflexions.Clear ();
            ReflexionKeys.Clear ();
        }

        public void AddConcepts ( params ( term, thought ) [] values )
        {
            for (int i = 0; i < values.Length; i++)
                ConceptThoughts.Add ( values [i].Item1, values [i].Item2 );
        }

        public bool ThoughtExists ( term key )
        {
            return ConceptThoughts.ContainsKey ( key );
        }

        public thought GetThought ( term key )
        {
            return ConceptThoughts [ key ];
        }
    }

    public class mind : thought
    {
        thought main;

        public void StartRootThought ( thought thought )
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
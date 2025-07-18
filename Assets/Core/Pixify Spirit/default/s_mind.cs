using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{
    public class s_mind : pix
    {
        List <reflexion> Reflexions = new List<reflexion> ();
        List <int> ReflexionKeys = new List<int> ();

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
    }
}
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class graphic : moon {
        public List <GameObject> renderers {private set; get;} = new List<GameObject> ();

        List <int> old_layers = new List<int> ();
        public void set_layers_for_capture () {
            old_layers.Clear ();
            for (int i = 0; i < renderers.Count; i++) {
                old_layers.Add ( renderers[i].layer );
                renderers [i].layer = vecteur.CAPTURE;
            }
        }

        public void restore_layers () {
            for (int i = 0; i < old_layers.Count; i++) {
                renderers [i].layer = old_layers [i];
            }
        }
    }
}
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class skin_material : moon {
        [link]
        graphic g;

        List <Renderer> renderers = new List<Renderer> ();
        List <Material []> old_material = new List<Material []> ();
        List <Material []> new_material = new List<Material[]> ();

        protected override void _ready() {
            var meshes = g.root.GetComponentsInChildren <Renderer> ();

            foreach ( var m in meshes )
            renderers.Add ( m );

            backup_material ();
            init_new_array ();
        }

        public void push_temp_material ( Material material ) {
            foreach ( var n in new_material ) {
                for (int i = 0; i < n.Length; i++)
                {
                    n [i] = material;
                }
            }

            for (int i = 0; i < renderers.Count; i++) {
                renderers [i].sharedMaterials = new_material [i];
            }
        }

        public void reset () {
            for (int i = 0; i < renderers.Count; i++) {
                renderers [i].sharedMaterials = old_material [i];
            }
        }

        void backup_material () {
            foreach ( var r in renderers ) {
                old_material.Add ( r.sharedMaterials );
            }
        }

        void init_new_array () {
            foreach ( var r in old_material ) {
                new_material.Add ( new Material [ r.Length ] );
            }
        }
    }

    public class skin_layer : moon {
        [link]
        graphic g;

        List <GameObject> renderers = new List<GameObject> ();

        protected override void _ready() {
            var meshes = g.root.GetComponentsInChildren <Renderer> ();
            foreach ( var m in meshes )
            renderers.Add ( m.gameObject );
        }

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
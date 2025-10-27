using Lyra;
using UnityEngine;
using UnityEngine.UI;

namespace Triheroes.Code {
    [inked]
    public class player_hud : graphic_element {
        character_label[] labels;
        health_hud[] healths;
        energy_hud energy;

        public prompt prompt {private set; get;}

        main_actors ma;

        public class ink : ink<player_hud> {
            public ink(RectTransform[] healths_hud, RectTransform energy, Text prompt) {
                o.healths = new health_hud[healths_hud.Length];
                o.labels = new character_label[healths_hud.Length];

                for (int i = 0; i < healths_hud.Length; i++) {
                    o.healths[i] = new health_hud(healths_hud[i].GetChild(1).GetComponent<RectTransform>());

                    o.labels[i] = new character_label(healths_hud[i].GetChild(0).GetComponent<Text>());
                }

                o.energy = new energy_hud(energy);

                o.prompt = new prompt ( prompt );
            }
        }

        protected override void _ready() {
            phoenix.core.start_action(this);
            ma = phoenix.star.get<main_actors>();
        }

        void start_healths_hud() {
            for (int i = 0; i < healths.Length; i++) {
                if (i < ma.count) {
                    healths[i].start(ma[i].system.get<health>());
                    labels[i].start(((actor)ma[i]).name);
                }
                else {
                    labels[i].end();
                    healths[i].end();
                }
            }
        }

        protected override void _step() {
            if (energy.on)
                energy.container.anchoredPosition = energy.stamina.hud_pos.stamina();
        }

        public void frame_player(actor actor) {
            start_healths_hud();
            energy.start(actor.system.get<stamina>());
        }
    }
} 
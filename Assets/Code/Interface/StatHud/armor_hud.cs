using Lyra;
using UnityEngine;
using UnityEngine.UI;
/*
namespace Triheroes.Code
{
    [need_ready]
    public class armor_hud : graphic_element {
        public const float unit_HP = 100;
        public const float unit_penetration = 10;

        shielding shielding;
        RectTransform container;

        ArmorGraphic layer0;
        ArmorGraphic layer1;

        public armor_hud ( shielding _shielding ) {
            shielding = _shielding;
        }

        public void start ( RectTransform _container ) {
            container = _container;

            ready_for_tick ();
            phoenix.core.start_action ( this );
        }

        protected override void _step() {
            layer0.SetVerticesDirty ();
            layer1.SetVerticesDirty ();
        }

        protected override void _start() {
            layer0 = instantiate_graphic_to_container < ArmorGraphic > ( GO.armor_wheel, container );
            layer1 = instantiate_graphic_to_container < ArmorGraphic > ( GO.armor_wheel_glow, container );

            int capacity = Mathf.CeilToInt ( shielding.max / unit_HP );
            layer0.capacity = capacity;
            layer1.capacity = capacity;

            layer0._vh = _l0;
            layer1._vh = _l1;
        }

        void _l0 ( VertexHelper vh ) {
            layer0.DrawFrame ( vh );

            // draw raw HP
            int HP = Mathf.CeilToInt (shielding.HP_ui / unit_HP);
            for (int i = 0; i < HP; i++)
                layer0.DrawSlice ( vh, i, layer0.NormalSliceTile, Color.white, Vector3.zero );

            layer0.DrawCover ( vh );
        }

        void _l1 ( VertexHelper vh ) {
            // draw penetration hot hp
            float HP = (shielding.penetrationHP_max - shielding.penetrationHP_ui) / unit_penetration;

            for (int i = 0; i < HP ; i++) {
                float alpha = Mathf.Clamp01 ( HP - i );
                layer1.DrawSliceBlur ( vh, i, layer0.AltSliceTile, new Color (1,.5f,.5f, alpha ) );
            }
        }

        protected override void _stop() {
            container.ClearChildren ();
        }
    }
}*/
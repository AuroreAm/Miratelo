using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    // Armored metal
    // Consecutive hit reduces armor points
    // after a cooldown, the armor points are restored
    // when no more armor points, hit will damage the character

    [Category("character")]
    public class ce_armored_metal : element, IElementListener <Slash>
    {
        [Export]
        public float MaxAP;
        [Export]
        public float Cooldown = 10;
        [Export]
        public float APRestorationPerSecond = 0.1f;

        [Depend]
        s_element se;

        [Depend]
        restorer r;

        float AP;

        public override void Create()
        {
            AP = MaxAP;
            Stage.Start (r);
        }

        public void OnMessage(Slash context)
        {
            if (AP > 0)
            AP -= context.raw;
            else
            se.SendMessage (new Damage (context.raw));

            r.RestoreCooldown = Cooldown;
        }

        class restorer : s_stat_generator
        {
            [Depend]
            ce_armored_metal o;

            public float RestoreCooldown;

            protected override void Step()
            {
                if (RestoreCooldown > 0)
                    RestoreCooldown -= Time.deltaTime;
                else if (o.AP != o.MaxAP)
                    o.AP = Mathf.MoveTowards(o.AP, o.MaxAP, o.APRestorationPerSecond * Time.deltaTime); 
            }
        }
    }
}
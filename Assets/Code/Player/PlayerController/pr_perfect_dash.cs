using System.Collections;
using System.Collections.Generic;
using Pixify.Spirit;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_perfect_dash : reflexion, IElementListener<incomming_slash>
    {
        bool Ready;
        ac_slash attacker;

        [Depend]
        pc_perfect_dash_bullet_time pfdbt;

        [Depend]
        s_element se;

        [Depend]
        ac_dash ad;
        [Depend]
        ac_backflip ab;

        bool dodging => ad.on || ab.on;

        protected override void Start()
        {
            se.LinkMessage(this);
        }

        protected override void Stop()
        {
            se.UnlinkMessage(this);
        }

        protected override void Step()
        {
            if (Ready && attacker.on == false && !pfdbt.on)
            {
                if (dodging)
                    Stage.Start1(pfdbt);
                Reset();
            }
        }

        public void OnMessage(incomming_slash context)
        {
            ReadyForPerfectDash(ActorList.Get(context.sender).block.GetPix<ac_slash>());
        }

        void ReadyForPerfectDash(ac_slash _attacker)
        {
            Ready = true;
            attacker = _attacker;
        }

        void Reset()
        {
            Ready = false;
            attacker = null;
        }
    }

    public class pc_perfect_dash_bullet_time : action
    {
        [Depend]
        bullet_time bt;

        [Depend]
        pc_perfect_dash pfd;

        [Depend]
        ac_dash ad;
        [Depend]
        ac_backflip ab;

        bool dodging => ad.on || ab.on;
        bool ReadyForDash;

        public override void Create()
        {
            Link (bt);
        }

        protected override void Start()
        {
            bt.Set(.4f);

            ReadyForDash = false;
        }

        protected override void Step()
        {
            if (!dodging)
                SelfStop();

            // NOTE while dash is still processing, if the player press action2, append the perfect dash

            if (Player.Action2.OnActive)
                ReadyForDash = true;
        }

        protected override void Stop()
        {
            if (ReadyForDash)
                Stage.Start1(pfd);
        }
    }

    public class pc_perfect_dash : action, IMotorHandler
    {
        [Depend]
        bullet_time bt;

        [Depend]
        s_skin ss;

        [Depend]
        ac_dash ad;

        [Depend]
        s_motor sm;

        [Depend]
        d_actor da;

        [Depend]
        pc_SS7 ss7;

        public void OnMotorEnd(motor m)
        {
            SelfStop();
        }

        public override void Create()
        {
            Link (bt);
        }

        protected override void Start()
        {
            if (!da.target)
            {
                SelfStop();
                return;
            }

            ss.rotY = Vecteur.RotDirection(ss.Coord.position, da.target.dd.position);

            ad.SetDashDirection(direction.forward);
            ad.OverrideDashAnimation(AnimationKey.perfect_dash, 0);

            var success = sm.SetState(ad, this);
            if (!success)
            {
                SelfStop();
                return;
            }

            bt.Set(.2f);
        }

        protected override void Stop()
        {
            Stage.Start1(ss7);
        }
    }

    public class pc_SS7 : action, IMotorHandler
    {
        [Depend]
        s_motor sm;
        motor[] Combo;

        int ComboPtr;
        bool ReadyForCombo;
        float timer = 0;
        readonly float TimeOut = .2f;

        [Depend]
        bullet_time bt;

        public void OnMotorEnd(motor m)
        { }

        public override void Create()
        {
            Link (bt);

            Combo = new motor[10];

            var slashKeys = SS7.SlashKeys;
            for (int i = 0; i < 10; i++)
            {
                var motor_slash = new ac_slash(slashKeys[i % 3]);
                b.IntegratePix(motor_slash);
                Combo[i] = motor_slash;
            }
        }

        protected override void Start()
        {
            bt.Set(.2f);

            ComboPtr = 0;
            ReadyForCombo = false;

            StartSlash();
        }

        void StartSlash()
        {
            sm.SetState(Combo[ComboPtr], this);
            timer = 0;
        }

        protected override void Step()
        {
            if (Player.Action2.OnActive)
                ReadyForCombo = true;

            if (!(sm.state is ac_slash))
            {
                if (timer > TimeOut)
                {
                    SelfStop();
                    return;
                }

                if (ReadyForCombo)
                {
                    ComboPtr++;

                    if (ComboPtr >= Combo.Length)
                    {
                        SelfStop();
                        return;
                    }

                    StartSlash();
                    ReadyForCombo = false;
                }

                timer += Time.deltaTime;
            }
        }
    }

}
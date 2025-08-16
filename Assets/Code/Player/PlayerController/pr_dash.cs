using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_dash : reflexion
    {
        [Depend]
        d_skill ds;

        [Depend]
        d_actor da;

        [Depend]
        s_skin ss;

        public void OnMotorEnd(motor m)
        { }

        protected override void Step()
        {
            if ( Player.Dash.OnActive && ds.SkillValid <DS0_dash> () )
            {
                direction direction = direction.forward;

                if (da.target)
                {
                    Vector3 InputAxis;
                    InputAxis = Player.MoveAxis3.normalized;
                    InputAxis = Vecteur.LDir(Mathf.DeltaAngle(ss.rotY.y, s_camera.o.td.rotY.y) * Vector3.up, InputAxis);

                    if (Mathf.Abs(InputAxis.x) > Mathf.Abs(InputAxis.z))
                    {
                        if (InputAxis.x < 0)
                            direction = direction.left;
                        else
                            direction = direction.right;
                    }
                    else if (InputAxis.z < 0)
                        direction = direction.back;
                }
                ds.GetSkill <DS0_dash> ().Spam (direction);
            }
            // UpdateDirectionIfTarget();
        }

        /*
        void UpdateDirectionIfTarget()
        {
            if (ad.on && da.target)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(ss.rotY.y, Vecteur.RotDirectionY(ss.Coord.position, da.target.dd.position))) < 90)
                    ss.rotY = new Vector3(0, Mathf.MoveTowardsAngle(ss.rotY.y, Vecteur.RotDirectionY(ss.Coord.position, da.target.dd.position), Time.deltaTime * 720), 0);
            }
        }*/
    }
}

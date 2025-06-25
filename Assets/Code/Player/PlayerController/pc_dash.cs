using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class pr_dash : reflection
    {
        [Depend]
        m_actor ma;
        [Depend]
        m_skin ms;

        [Depend]
        ac_dash ad;

        public override void Main()
        {
            if (Player.Dash.OnActive)
            {
                ad.DashDirection = direction.forward;

                if (ma.target)
                {
                    Vector3 InputAxis;
                    InputAxis = Player.MoveAxis3.normalized;
                    InputAxis = Vecteur.LDir(Mathf.DeltaAngle(ms.rotY.y, m_camera.o.td.rotY.y) * Vector3.up, InputAxis);

                    if (Mathf.Abs(InputAxis.x) > Mathf.Abs(InputAxis.z))
                    {
                        if (InputAxis.x < 0)
                            ad.DashDirection = direction.left;
                        else
                            ad.DashDirection = direction.right;
                    }
                    else if (InputAxis.z < 0)
                        ad.DashDirection = direction.back;
                }

                mst.SetState(ad, Pri.Action);
                return;
            }

            UpdateDirectionIfTarget();
        }

        void UpdateDirectionIfTarget()
        {
            if (ad.on && ma.target)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(ms.rotY.y, Vecteur.RotDirectionY(ms.Coord.position, ma.target.md.position))) < 90)
                    ms.rotY = new Vector3(0, Mathf.MoveTowardsAngle(ms.rotY.y, Vecteur.RotDirectionY(ms.Coord.position, ma.target.md.position), Time.deltaTime * 720), 0);
                ms.SkinDir = Vecteur.LDir(ms.rotY, ac_dash.Direction(ad.DashDirection));
            }
        }
    }
}

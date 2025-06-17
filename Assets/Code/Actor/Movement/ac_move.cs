using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public abstract class ac_walk_target_base : action
    {
        [Depend]
        protected m_actor ma;
        [Depend]
        protected c_ground_movement_complex cgmc;

        public float Speed;
        public float WalkFactor = 1;

        protected m_actor target;

        protected override void BeginStep()
        {
            target = ma.target;
            cgmc.Aquire(this);
        }

        protected sealed override bool Step()
        {
            if (ma.target != target)
                return true;

            if (!target)
                return true;

            return Move();
        }

        protected abstract bool Move();
        protected override void Stop()
        {
            cgmc.Free(this);
        }
    }

    public class ac_walk_to_target : ac_walk_target_base
    {
        public bool StopWhenDone;
        public float StopDistance = 0.23f;
        float distance;

        protected override void BeginStep()
        {
            base.BeginStep();

            if (ma.target)
                distance = ma.md.r + target.md.r + StopDistance;
        }

        protected override bool Move()
        {
            Vector3 targetPosition = target.md.position;
            Vector3 direction = (targetPosition - ma.md.position).Flat();

            if (Vector3.Distance(ma.md.position, targetPosition) > distance)
                cgmc.Walk(direction.normalized * Speed, WalkFactor);
            else if (StopWhenDone)
                return true;

            return false;
        }
    }

    public class ac_walk_arround_target : ac_walk_target_base
    {
        public float AngleAmount;

        float angle;

        protected override void BeginStep()
        {
            base.BeginStep();
            angle = 0;
        }

        protected override bool Move()
        {
            Vector3 DesiredRotY = Vecteur.RotDirection(ma.md.position, target.md.position).OnlyY() + Mathf.Sign(AngleAmount) * Vector3.up * 90;
            cgmc.rotDir = DesiredRotY;
            Vector3 DesiredDir = Vecteur.LDir(DesiredRotY, Speed * Vector3.forward);

            angle += Mathf.DeltaAngle(Vecteur.RotDirectionY(target.md.position, ma.md.position), Vecteur.RotDirectionY(target.md.position, ma.md.position + DesiredDir * Time.deltaTime/*a*/ * WalkFactor));

            cgmc.Walk(DesiredDir, WalkFactor);

            if (Mathf.Abs(angle) > Mathf.Abs(AngleAmount))
                return true;

            return false;
        }
    }
}

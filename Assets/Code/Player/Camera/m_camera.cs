using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Triheroes.Code
{
    public class m_camera : core
    {
        public static m_camera o;
        public Transform Coord;
        public Camera Cam;
        camera_shot Shot;

        public override void Create()
        {
            o = this;
            Aquire(this);
            SetCameraShot(dummy);
        }

        public override void Main()
        {
            Coord.position = Shot.CamPos;
            Coord.rotation = Shot.CamRot;
            Cam.fieldOfView = Shot.CamFoV;
        }

        void SetCameraShot(camera_shot Shot)
        {
            if (this.Shot != null)
                this.Shot.Free(this);

            this.Shot = Shot;
            Shot.Aquire(this);
        }

        // public methods
        // camera control
        [Depend]
        public tps_data td;
        [Depend]
        camera_dummy dummy;
        [Depend]
        tps_normal tps;
        [Depend]
        tps_transition ttr;
        [Depend]
        tps_target tt;
        [Depend]
        cs_subject cs;

        public void TpsACharacter(m_actor MainCharacter)
        {
            td.SetSubject(MainCharacter);
            SetCameraShot(tps);
        }

        public void TpsTransitionToTarget(m_dimension Target)
        {
            tt.target = Target;
            if (Shot is tps_shot s)
            {
                ttr.Set(s, tt);
                SetCameraShot(ttr);
            }
        }

        public void CutToShot ()
        {
            SetCameraShot(cs);
        }

        // Screen ray
        Ray ScreenRay;
        public Vector3 PointScreenCenter(Transform Exclude)
        {
            ScreenRay.origin = Coord.position;
            ScreenRay.direction = Coord.forward;

            int d = Exclude.gameObject.layer;
            Exclude.gameObject.layer = 0;
            RaycastHit hit;

            bool HasHitSomething = Physics.Raycast(ScreenRay, out hit,
            256, Vecteur.SolidCharacter);

            Exclude.gameObject.layer = d;

            if (HasHitSomething)
                return hit.point;
            else
                return Coord.position + Coord.forward * 256;
        }

        // built in shots
        public class tps_transition : tps_shot
        {
            Vector3 inRotY;
            float inHeight;
            float inDistance;
            Vector3 inOffset;
            tps_shot IN;
            tps_shot OUT;

            public void Set(tps_shot In, tps_shot Out)
            {
                IN = In;
                OUT = Out;
            }

            Vector3 internalRotY;
            protected override void OnAquire()
            {
                inRotY = td.rotY;
                inHeight = IN.height;
                inDistance = IN.distance;
                inOffset = IN.offset;

                OUT.Aquire(this);
                t = 0;
            }

            float t;
            public override void Main()
            {
                t = Mathf.Lerp(t, 1, .1f);

                internalRotY = new Vector3( Mathf.LerpAngle(inRotY.x, td.rotY.x, t), Mathf.LerpAngle(inRotY.y, td.rotY.y, t), 0);
                offset = Vector3.Lerp(inOffset, OUT.offset, t);
                height = Mathf.Lerp(inHeight, OUT.height, t);
                distance = Mathf.Lerp(inDistance, OUT.distance, t);

                RayCameraPosition(internalRotY);

                if (t >= .95f)
                {
                    OUT.Free(this);
                    o.SetCameraShot(OUT);
                }
            }

            protected override void OnFree()
            {
                IN = null;
                OUT = null;
            }
        }
    }

    [CoreBase]
    public abstract class camera_shot : core
    {
        public Vector3 CamPos;
        public Quaternion CamRot;
        public float CamFoV = 60;
    }

    public class camera_dummy : camera_shot
    {
        public override void Main()
        {
            CamPos = Vector3.zero;
            CamRot = Quaternion.identity;
        }
    }
}
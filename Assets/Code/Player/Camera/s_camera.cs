using System;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    [SysBase (SysOrder.s_camera)]
    [NeedPackage]
    public class s_camera : sys
    {
        public static s_camera o { private set; get; }
        public Transform Coord {private set; get;}
        public static Camera Cam {private set; get;}

        camera_shot _shot;

        public class package : Package <s_camera>
        {
            public package ( Camera camera, Transform coord )
            {
                o.Coord = coord;
                Cam = camera;
            }
        }

        protected override void OnStructured()
        {
            o = this;
            SceneMaster.Processor.Start (this);
            SetCameraShot(dummy);
        }

        protected override void OnStep()
        {
            Coord.position = _shot.CamPos;
            Coord.rotation = _shot.CamRot;
            Cam.fieldOfView = _shot.CamFoV;
        }

        void SetCameraShot ( camera_shot shot )
        {
            if ( _shot != null )
                this.Unlink ( _shot );

            _shot = shot;
            this.Link ( _shot );
        }

        // public methods
        // camera control
        [Link]
        public tps_data td;
        [Link]
        camera_dummy dummy;
        [Link]
        tps_normal tps;
        [Link]
        tps_transition ttr;
        [Link]
        tps_target tt;
        [Link]
        cs_subject cs;

        public void TpsACharacter ( d_dimension_meta MainCharacter )
        {
            td.SetSubject ( MainCharacter );
            SetCameraShot ( tps );
        }

        public void TpsTransitionToTps ()
        {
            if (_shot is tps_shot s)
            {
                ttr.Set(s, tps);
                SetCameraShot(ttr);
            }
        }

        public void TpsTransitionToTarget ( d_dimension_meta Target )
        {
            tt.Target = Target;
            if (_shot is tps_shot s)
            {
                ttr.Set(s, tt);
                SetCameraShot(ttr);
            }
        }

        public void CutToShot ( )
        {
            SetCameraShot(cs);
        }

        // Screen ray
        Ray ScreenRay;
        public Vector3 PointScreenCenter ( GameObject Exclude )
        {
            ScreenRay.origin = Coord.position;
            ScreenRay.direction = Coord.forward;

            int d = Exclude.layer;
            Exclude.layer = 0;
            RaycastHit hit;

            bool HasHitSomething = Physics.Raycast(ScreenRay, out hit,
            256, Vecteur.SolidCharacter);

            Exclude.layer = d;

            if (HasHitSomething)
                return hit.point;
            else
                return Coord.position + Coord.forward * 256;
        }


        // built in shots
        public class tps_transition : camera_shot
        {
            float inRotY;
            float inRotX;
            float inHeight;
            float inDistance;
            Vector3 inOffset;
            tps_shot IN;
            tps_shot OUT;

            protected const float radius = .5f;
            public Vector3 offset { get; protected set; }
            public float height { get; protected set; }
            public float distance { get; protected set; }

            [Link]
            tps_data td;

            public void Set(tps_shot In, tps_shot Out)
            {
                if (In == null || Out == null)
                    throw new NullReferenceException("tps_transition: null tps_shot");
                IN = In;
                OUT = Out;
            }

            float _RotY;
            float _RotX;

            protected override void OnStart()
            {
                inRotY = td.RotY;
                inRotX = td.RotX;
                inHeight = IN.Height;
                inDistance = IN.Distance;
                inOffset = IN.Offset;

                this.Link ( OUT );
                t = 0;
            }

            float t;
            protected override void OnStep()
            {
                t = Mathf.Lerp(t, 1, .1f);

                _RotX = Mathf.LerpAngle ( inRotX, td.RotX, t );
                _RotY = Mathf.LerpAngle ( inRotY, td.RotY, t );
                offset = Vector3.Lerp ( inOffset, OUT.Offset, t );
                height = Mathf.Lerp ( inHeight, OUT.Height, t );
                distance = Mathf.Lerp ( inDistance, OUT.Distance, t );

                RayCameraPosition();

                if (t >= .95f)
                    o.SetCameraShot ( OUT );
            }

            protected override void OnStop()
            {
                IN = null;
                OUT = null;
            }

            protected void RayCameraPosition()
            {
                float RayDistance = distance;
                Vector3 TargetPos = td.Subject.Position + offset + height * Vector3.up;

                if (Physics.SphereCast(td.Subject.Position, radius, Vecteur.LDir( new Vector3(_RotX, _RotY,0), Vector3.back ), out RaycastHit hit, distance, Vecteur.Solid) )
                    RayDistance = hit.distance - 0.05f;

                CamPos = TargetPos + Vecteur.LDir ( new Vector3(_RotX, _RotY,0), Vector3.back ) * RayDistance;
                CamRot = Quaternion.Euler ( new Vector3(_RotX, _RotY,0) );
            }
        }
    }

    [SysBase (SysOrder.camera_shot)]
    public abstract class camera_shot : sys.ext
    {
        public Vector3 CamPos;
        public Quaternion CamRot;
        public float CamFoV = 60;
    }

    public class camera_dummy : camera_shot
    {
        protected override void OnStep()
        {
            CamPos = Vector3.zero;
            CamRot = Quaternion.identity;
        }
    }
}
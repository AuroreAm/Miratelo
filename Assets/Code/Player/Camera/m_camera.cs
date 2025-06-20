using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Triheroes.Code
{
    public class m_camera : module
    {
        public static m_camera o;

        public Transform Coord;
        public Transform CameraPivot;
        public Camera Cam;

        [Depend]
        public m_camera_tps mct;

        public override void Create()
        {
            o = this;
            mct.Aquire (this);
        }

        // public methods
        // Screen ray
        Ray ScreenRay;
        public Vector3 PointScreenCenter(Transform Exclude)
        {
            // Update ScreenRay
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
    }

    public abstract class m_camera_controller : core
    {}

    [RegisterAsBase]
    public class m_camera_tps : m_camera_controller
    {
        [Depend]
        public m_camera mc;

        public m_actor C;
        public Vector3 Target => C.md.position;
        // controls
        public Vector3 rotY { get; private set; }
        public Vector3 offset { get; private set; }
        public float height { get; private set; }
        public float distance { get; private set; }

        bool isTransitioning = false;
        pc_camera_tps_controller currentController;
        m_camera_tps_transition transitionController;

        // embeded controllers
        tps_normal tps;
        tps_target tt;

        public override void Create()
        {
            transitionController = character.ConnectNode ( new m_camera_tps_transition() );
            tps = character.ConnectNode ( new tps_normal() );
            tt = character.ConnectNode ( new tps_target() );

            currentController = tps;
        }

        void SetController ( pc_camera_tps_controller newController )
        {
            if (newController != currentController)
            {
                currentController = newController;
                transitionController.SetNextController(newController);
                transitionController.Default();
                isTransitioning = true;
            }
        }

        public sealed override void Main()
        {
            // TODO: get the target character from Gamedata
            C = GameObject.Find ("Player").GetComponent <Character>().GetModule<m_actor> ();
            CameraController ();
            UpdateController ();
            UpdateCamera ();
        }

        void CameraController ()
        {
            // TODO: chose the right controller here according to the target character behaviour
            if (C.target != null)
            {
                SetController(tt);
                return;
            }

            SetController(tps);
        }

        void UpdateController ()
        {
            if (!isTransitioning)
            {
                currentController.Update ();
                rotY = currentController.rotY;
                offset = currentController.offset;
                height = currentController.height;
                distance = currentController.distance;
            }
            else
            {
                transitionController.Update ();
                rotY = transitionController.rotY;
                offset = transitionController.offset;
                height = transitionController.height;
                distance = transitionController.distance;
            }
        }

        void UpdateCamera ()
        {
            float RayDistance = distance;

            // TODO: use spherecast instead of raycast to fix the camera glitch entering the wall
            // TODO: use multiple cast to smooth camera movement
            if (Physics.Raycast(mc.Coord.position, mc.CameraPivot.TransformDirection(Vector3.back), out RaycastHit hit, distance, Vecteur.Solid))
                RayDistance = hit.distance - 0.25f;

            mc.Coord.position = Target + offset + height * Vector3.up;
            mc.Coord.rotation = Quaternion.Euler(rotY);
            mc.CameraPivot.transform.localPosition = Vector3.back * RayDistance;
        }

        class m_camera_tps_transition : pc_camera_tps_controller
        {
            pc_camera_tps_controller nextController;

            public Vector3 inRotY;
            public Vector3 inOffset;
            public float inHeight;
            public float inDistance;
            // transition time (0-1)
            float t = 0;

            public void SetNextController(pc_camera_tps_controller nextController)
            {
                this.nextController = nextController;
            }

            public override void Default()
            {
                t = 0;
                SyncWithTps();

                inRotY = c.rotY;
                inOffset = c.offset;
                inHeight = c.height;
                inDistance = c.distance;

                // start the next controller
                nextController.Default();
            }

            public override void Update()
            {
                nextController.Update();

                t += Time.unscaledDeltaTime * 2;

                rotY.y = Mathf.LerpAngle(inRotY.y, nextController.rotY.y, t);
                rotY.x = Mathf.LerpAngle(inRotY.x, nextController.rotY.x, t);
                height = Mathf.Lerp(inHeight, nextController.height, t);
                distance = Mathf.Lerp(inDistance, nextController.distance, t);
                offset = Vector3.Lerp(inOffset, nextController.offset, t);

                if (t >= .99f)
                {
                    c.isTransitioning = false;
                }
                return;
            }
        }
    }

    public abstract class pc_camera_tps_controller : node
    {
        [Depend]
        public m_camera_tps c;

        // parameters
        public Vector3 rotY;
        public Vector3 offset;
        public float height;
        public float distance;

        protected void SyncWithTps ()
        {
            rotY = c.rotY;
            offset = c.offset;
            height = c.height;
            distance = c.distance;
        }

        public virtual void Default()
        { }

        public virtual void Update()
        { }
    }
}

using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    // TODO spectre linkage with sound effect // random sound effect
    public class Spectre : pix
    {
        public static Spectre o;

        Dictionary <int, ParticleSystem> Effects;

        public override void Create()
        {
            o = this;
            Effects = new Dictionary<int, ParticleSystem> ();
            var ParticleSystems = SubResources <ParticleSystem>.GetAll ();
            for (int i = 0; i < ParticleSystems.Length; i++)
            {
                var u = GameObject.Instantiate (ParticleSystems[i]);
                Effects.Add ( new term (ParticleSystems[i].name), u );
            }
        }

        public static void Fire ( int name, Vector3 pos )
        {
            ParticleSystem.EmitParams e = new ParticleSystem.EmitParams { position = pos };
            o.Effects[name].Emit (e, 1);
        }
    }

    /*
    public class s_trail_spectre : ThingSystem<p_trail_spectre>
    {
        public static s_trail_spectre o;
        protected override int InitialPieces => 5;

        public s_trail_spectre()
        { o = this; }

        public static int Bind ( int ModelName )
        {
            o.pool.NextPiece().Set(SubResources<TrailRenderer>.q(ModelName));
            return o.pool.GetPiece();
        }

        public static void SetPosition ( int id, Vector3 pos )
        {
            o.pool.indexedPieces[id].Trail.transform.position = pos;
        }

        public static void UnBind ( int id )
        {
            o.pool.ReturnPiece(id);
        }
    }

    public class p_trail_spectre : thing
    {
        public TrailRenderer Trail { get; private set; }
        public override void Create ()
        {
            Trail = new GameObject("Trail Channel").AddComponent<TrailRenderer>();
            Trail.emitting = true;
            Trail.gameObject.SetActive (false);
        }

        public void Set ( TrailRenderer Model )
        {
            Trail.alignment = Model.alignment;
            Trail.colorGradient = Model.colorGradient;
            Trail.endColor = Model.endColor;
            Trail.endWidth = Model.endWidth;
            Trail.minVertexDistance = Model.minVertexDistance;
            Trail.numCapVertices =  Model.numCapVertices;
            Trail.numCornerVertices = Model.numCornerVertices;
            Trail.startColor = Model.startColor;
            Trail.startWidth = Model.startWidth;
            Trail.textureMode = Model.textureMode;
            Trail.time = Model.time;
            Trail.widthCurve = Model.widthCurve;
            Trail.widthMultiplier = Model.widthMultiplier;
            Trail.sharedMaterial = Model.sharedMaterial;
        }

        public override void BeginStep()
        {
            Trail.gameObject.SetActive (true);
            Trail.Clear ();
        }

        public override void Stop()
        {
            Trail.gameObject.SetActive (false);
        }

        public override bool Main()
        {
            return false;
        }
    }*/
}
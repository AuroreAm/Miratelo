using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class p_slash_attack : piece
    {
        public Sword sword { private set; get; }

        Vector3 position;
        Quaternion rotation;
        Vector3 previousPosition;
        Quaternion previousRotation;
        float length;
        float duration;

        float time;

        public override void Stop()
        {
            time = 0;
            sword = null;
        }

        public void Set(Sword sword, float duration)
        {
            this.sword = sword;
            position = sword.transform.position;
            rotation = sword.transform.rotation;
            previousPosition = position;
            previousRotation = rotation;
            length = sword.Length;
            this.duration = duration;
        }

        public void Shift()
        {
            previousPosition = position;
            previousRotation = rotation;
            position = sword.transform.position;
            rotation = sword.transform.rotation;
        }


        public class s_slash_attack : PieceSystem<p_slash_attack>
        {
            public static s_slash_attack o;

            public s_slash_attack()
            {
                o = this;
                dimensions = new SlashRayCalculation();
                rayCasters = new RayCaster();
            }

            SlashRayCalculation dimensions;
            RayCaster rayCasters;

            public override void Execute()
            {
                for (int i = pieces.Count - 1; i >= 0; i--)
                    pieces[i].Shift();

                // transfer data to the system
                dimensions.SetEffectiveSize(pieces.Count);
                rayCasters.SetEffectiveSize(pieces.Count * 5);

                for (int i = 0; i < dimensions.effectiveSize; i++)
                {
                    dimensions.data[i] = new SlashDimensions()
                    {
                        position = pieces[i].position,
                        rotation = pieces[i].rotation,
                        previousPosition = pieces[i].previousPosition,
                        previousRotation = pieces[i].previousRotation,
                        length = pieces[i].length
                    };
                }

                // calculate the rays
                dimensions.Execute(ref rayCasters.data);

                // cast the rays
                rayCasters.Execute();

                // duration
                for (int i = pieces.Count - 1; i >= 0; i--)
                {
                    pieces[i].time += Time.deltaTime;
                    if (pieces[i].time > pieces[i].duration)
                        ReturnPiece (pieces[i]);
                }
            }

            class SlashRayCalculation : PieceDataSystem<SlashDimensions>
            {
                public SlashRayCalculation() : base(100) { }

                public void Execute(ref SlashLine5[] rays)
                {
                    // create 5 rays along the sword
                    for (int i = 0; i < effectiveSize; i++)
                        for (int j = 0; j < 5; j++)
                        {
                            rays[i*5+j].line = new SlashLine5.Line(data[i].previousPosition + data[i].previousRotation * Vector3.forward * data[i].length * (j / 4f), data[i].position + data[i].rotation * Vector3.forward * data[i].length * (j / 4f));
                        }
                }
            }

            class RayCaster : PieceDataSystem<SlashLine5>
            {
                public RayCaster() : base(20) { }

                public void Execute()
                {
                    for (int i = 0; i < effectiveSize; i++)
                    {
                        if (Physics.Linecast(data[i].line.start, data[i].line.end, Vecteur.Solid))
                        {}
                    }
                }
            }

            public static p_slash_attack Fire(Sword sword, float duration)
            {
                o.PeekPiece().Set(sword, duration);
                p_slash_attack p = o.GetPiece();
                return p;
            }

            struct SlashDimensions
            {
                public Vector3 position;
                public Quaternion rotation;
                public Vector3 previousPosition;
                public Quaternion previousRotation;
                public float length;
            }

            // 5 steps slash rays along the sword
            struct SlashLine5
            {
                public Line line;

                public struct Line
                {
                    public Vector3 start;
                    public Vector3 end;
                    public Line(Vector3 start, Vector3 end)
                    {
                        this.start = start;
                        this.end = end;
                    }
                }
            }
        }
    }
}
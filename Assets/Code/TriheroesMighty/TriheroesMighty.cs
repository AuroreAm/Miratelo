using System.Collections;
using System.Collections.Generic;
using Lyra;
using Triheroes.Code;
using UnityEngine;

namespace Triheroes.Code
{
    public static class order
    {
        public const int axeal_ground = -70;

        public const int attack = 48;
        public const int arrow = 49;
        public const int slash = 50;

        public const int axeal_gravity = 70;
        public const int capsule = 71;

        public const int skin = 80;
        public const int spectre = 81;

        public const int auto_stat = 88;

        public const int camera_shot = 89;
        public const int camera = 90;

        public const int graphic_element = 100;
    }
}

namespace Triheroes{

    [CreateAssetMenu(fileName = "TriheroesMighty", menuName = "Game/Main Resources")]
    public class TriheroesMighty : ScriptableObject {
        public List <GameObject> Prefabs;
        public List <CurveRes> Curves;
        public List <StellarAuthor> Stellars;
        public List <ArrowAuthor> Arrows;
    }

    [superstar]
    public class res : moon {

        protected override void _ready() {
            TriheroesMighty main = Resources.Load <TriheroesMighty> ( "TriheroesMighty" );

            if (!main)
                throw new System.Exception ( "TriheroesMighty not found, game cannot start" );

            foreach (var a in main.Prefabs)
                go.add ( new term (a.name), a );

            foreach (var a in main.Stellars)
                stellars.add ( new term (a.name), a );
            
            foreach (var a in main.Arrows)
                arrows.add ( new term (a.name), a );

            foreach (var a in main.Curves)
                curves.add ( new term (a.name), a );
        }

        public static res <GameObject> go = new res<GameObject> ();
        public static res <StellarAuthor> stellars = new res<StellarAuthor> ();
        public static res <ArrowAuthor> arrows = new res<ArrowAuthor> ();
        public static res <CurveRes> curves = new res<CurveRes> ();
    }
    
}
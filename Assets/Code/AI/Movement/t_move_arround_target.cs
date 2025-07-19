using System.Collections;
using System.Collections.Generic;
using Pixify;
using Pixify.Spirit;
using UnityEngine;

namespace Triheroes.Code
{
    public class t_move_arround_target : thought.final
    {
        public float angleAmount {get; private set;}
        public float distance {get; private set;}
        
        [Category ("movement")]
        public class move_arround_target : thought
        {
            [Depend]
            t_move_arround_target main;

            [Export]
            public float AngleAmount;
            [Export]
            public float Distance;

            protected override void OnAquire()
            {
                main.angleAmount = AngleAmount;
                main.distance = Distance;
                main.Aquire (this);
            }
        }
    }
}
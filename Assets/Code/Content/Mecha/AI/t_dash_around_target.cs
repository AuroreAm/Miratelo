using Lyra;
using UnityEngine;
using Lyra.Spirit;

namespace Triheroes.Code
{
    public class t_dash_around_target : thought.final
    {
        
        public float direction {  get; private set; }

        [Category ("mecha")]
        public class dash_around_target : package.o <t_dash_around_target>
        {
            [Export]
            public float Direction;

            protected override void BeforeStart()
            {
                main.direction = Direction;
            }
        }
    }
}
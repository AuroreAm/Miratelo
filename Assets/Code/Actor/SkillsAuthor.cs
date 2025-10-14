using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class SkillsAuthor : ActorAuthorModule {

        public moon_paper < skill > [] skills;

        public override void _create() {
            new ink <skills> ();
        }

        public override void _created(system s) {
            skills ss = s.get <skills> ();
            foreach ( var skill in skills ) {
                ss.add ( skill.write () );
            }
        }
    }
}
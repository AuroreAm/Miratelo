using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class StandardCharacterAuthor : Scripter
    {
        [Header("Skin (appearance)")]
        public skin_writer skin;
        [Header("Actor definition")]
        public actor_writer actor;
        [Header("Stats")]
        public m_stat_writer stat;

        override public ModuleWriter[] GetModules ()
        {
            return new ModuleWriter[] { skin, actor, stat };
        }

        override public void OnSpawn ( Vector3 position, Quaternion rotation, Character c )
        {
            c.GetModule <m_skin> ().rotY = rotation.eulerAngles;
            c.RequireModule <m_state> ().AddReflection ( c.RequireModule <r_idle> ()  );
            c.RequireModule <m_state> ().AddReflection ( c.RequireModule <r_hooked_up_ccc> () );
            c.RequireModule <m_state> ().AddReflection ( c.RequireModule <r_knocked_out> () );
        }
    }
}
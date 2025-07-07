using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_cortex : module
    {
        public cortex cortex {private set; get;}

        /// <summary>
        /// Set the character cortex and connect to the character
        /// </summary>
        /// <param name="NewCortex"></param>
        public void SetCortex ( cortex NewCortex )
        {
            cortex = NewCortex;
            cortex.TriggerThinking ();
        }
    }

    // base class to determine which reflection the character needs 
    // basically the base that determine behavior for player controller or NPC
    public abstract class cortex : catom
    {
        [Depend]
        protected mc_motor mm;

        /// <summary>
        /// List of tasks available for execution
        /// </summary>
        Dictionary <SuperKey, task> Master = new Dictionary <SuperKey, task> ();

        public task GetTask (SuperKey key)
        {
            if (Master.ContainsKey (key))
                return Master [key];
            return null;
        }

        Dictionary < (int, object), SuperKey > TaskForEffects = new Dictionary < (int, object), SuperKey > ();

        public SuperKey GetTaskForEffect (int key, object value)
        {
            if (TaskForEffects.ContainsKey ((key, value)))
                return TaskForEffects [(key, value)];
            return AIKeys.zero;
        }


        protected void AddMaster ( task task )
        {
            Master.Add ( task.TaskID, task );
        }

        public void TriggerThinking ()
        {
            mm.ClearReflection ();
            Master.Clear ();
            Think ();
        }

        protected void AddReflection <T> () where T:reflection, new ()
        {
            mm.AddReflection ( mm.character.RequireModule <T> () );
        }

        protected abstract void Think ();
    }
}
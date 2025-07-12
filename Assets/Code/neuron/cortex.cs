using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_cortex : module
    {
        public cortex cortex {private set; get;}

        Dictionary <SuperKey, plan_notion> Notion = new Dictionary <SuperKey, plan_notion> ();

        public plan_notion GetPlan ( SuperKey key )
        {
            if (Notion.ContainsKey (key))
                return Notion [key];
            else
                return null;
        }

        public void AddNotion ( plan_notion notion )
        {
            Notion.Add (notion.descriptor, notion);
        }

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
    public abstract class cortex : catom
    {
        [Depend]
        protected mc_motor mm;

        public void TriggerThinking ()
        {
            mm.ClearReflection ();
            Think ();
        }

        protected void AddReflection <T> () where T:reflection, new ()
        {
            mm.AddReflection ( mm.character.RequireModule <T> () );
        }

        protected abstract void Think ();
    }
}
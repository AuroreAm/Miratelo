using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{

    /// <summary>
    /// transition to: slash"
    /// </summary>
    [Unique]
    public class t_slash : action
    {
        [Depend]
        m_sword_user msu;

        protected override bool Step()
        {
            if (Player.GetButtonDown(BoutonId.Fire1))
            selector.CurrentSelector.SwitchTo (StateKey2.slash);

            return false;
        }
    }

    /// <summary>
    /// "success the controlled selector to advance the combo
    /// </summary>
    [Unique]
    public class t_combo_success : action
    {
        [Depend]
        m_sword_user msu;

        protected override void BeginStep()
        {
            controlled_sequence.CurrentStatus = controlled_sequence.TaskStatusEnum.Failure;
        }

        protected override bool Step()
        {
            if (Player.GetButtonDown(BoutonId.Fire1))
            controlled_sequence.CurrentStatus = controlled_sequence.TaskStatusEnum.Success;
            return false;
        }
    }

}
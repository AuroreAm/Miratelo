using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{

    [Unique]
    [NodeDescription("transition to: slash")]
    [Category("player controller")]
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

    [Unique]
    [NodeDescription("success the controlled selector to advance the combo")]
    [Category("player controller")]
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
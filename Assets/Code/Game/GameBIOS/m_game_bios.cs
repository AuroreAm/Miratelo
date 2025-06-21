using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_game_bios : module
    {
        bios Current;
        public void Set (bios bios)
        {
            if (Current == bios) return;

            if (Current != null)
                Current.Free (this);

                Current = bios;
                bios.Aquire (this);
        }
    }

    [CoreBase]
    public abstract class bios : core
    {}
}
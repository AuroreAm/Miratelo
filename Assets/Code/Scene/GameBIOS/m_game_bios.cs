using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class m_game_bios : pix
    {
        bios Current;

        int key_i;

        public void Set (bios bios)
        {
            if (Current == bios) return;

            if (Current != null)
                Stage.Stop (key_i);

                Current = bios;
                key_i = Stage.Start (bios);
        }

    }

    [PixiBase]
    public abstract class bios : pixi
    {}
}
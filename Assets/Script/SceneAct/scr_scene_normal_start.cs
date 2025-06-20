using System.Collections;
using System.Collections.Generic;
using Pixify;
using Triheroes.Code;
using UnityEngine;
using static Pixify.treeBuilder;

public class scr_scene_normal_start : script
{
    public override action CreateTree()
    {
        new sequence () {repeat = false};
            new g_fade () { ToBlack = false };
            new g_show_title () { Text = MapId.o.Scene.NomDeLaCarte, TitleId = TriheroesTitle.MapTitle };
            new g_show_title () { Text = "Event test", TitleId = TriheroesTitle.EventTitle };
            new g_bios_use_play ();
            // new g_playBGM () { BGMName = new SuperKey (MapId.o.Scene.BGMNatif) };
        end ();
        
        return TreeFinalize ();
    }
}

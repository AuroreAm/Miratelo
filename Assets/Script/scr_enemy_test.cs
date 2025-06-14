using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using static Pixify.treeBuilder;

public class scr_enemy_test : script
{
    public override action CreateTree()
    {
        return TreeFinalize ();
    }
}

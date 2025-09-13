using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public class script : moon
    {
        Dictionary < term, index > library = new Dictionary<term, index> ();

        public void add_index ( index ind )
        {
            system.add (ind);
            library.Add ( ind.name, ind );
        }

        public index this [ term id ] => library [id];
    }
}
/*using System.Collections;
using System.Collections.Generic;
using Lyra;
using UnityEngine;

namespace Triheroes.Code
{
    public class WriteWriterGroup : CustomActionPaper <write_writer_group>
    {
        public GameObject group;

        protected override write_writer_group Instance() {
            return new write_writer_group (group);
        }
    }

    [paper (typeof (WriteWriterGroup))]
    [path ("scene")]
    public class write_writer_group : action {
        GameObject group;

        public write_writer_group ( GameObject _group ) {
            group = _group;
        }

        protected override void _start() {
            Writer [] g = group.GetComponentsInChildren < Writer > ();
            foreach ( var a in g ) {
                a.Write ();
            }

            stop ();
        }
    }
}*/

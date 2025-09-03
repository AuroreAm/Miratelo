using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Lyra
{
    public interface IStructureAuthor
    {
        /// <summary>
        /// set state of the dat before it is structured
        /// use PreStructure.Package to configure states
        /// PreBlock.Package can only be instancied within this method
        /// </summary>
        public void OnStructure ();
    }
}
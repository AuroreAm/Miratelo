using UnityEngine;

namespace Lyra
{
    public static class Act
    {
        public static void Start (action Script)
        {
            if (Script != null)
            {
                SceneMaster.Processor.Start ( Script );
            }
            else
            Debug.LogWarning ("script is null");
        }
    }
}
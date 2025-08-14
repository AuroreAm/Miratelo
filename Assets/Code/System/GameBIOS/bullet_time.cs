using System.Collections;
using System.Collections.Generic;
using Pixify;
using UnityEngine;

namespace Triheroes.Code
{
    public class bullet_time : bios
    {
        float scale = .1f;

        public void Set (float _scale)
        {
            scale = _scale;
        }

        protected override void Start()
        {
            Time.timeScale = scale;
        }

        protected override void Stop()
        {
            Time.timeScale = 1;
        }
    }
}
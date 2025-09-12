using System;
using System.Collections.Generic;
using Lyra;
using UnityEngine;
/*
namespace Triheroes.Code.TPS
{
    public sealed class transition : TPS.shot
    {
        float in_roty;
        float in_rotx;
        float in_h;
        float in_distance;
        Vector3 in_offset;
        public TPS.shot @out {private set; get;}

        [link]
        new TPS.data data;

        public void reset (TPS.shot _in, TPS.shot _out)
        {
            if (_in == null || _out == null)
                throw new NullReferenceException("tps_transition: null tps_shot");
            
            in_roty = _in.roty;
            in_rotx = _in.rotx;
            in_h = @_in.h;
            in_distance = @_in.distance;
            in_offset = @_in.offset;

            if ( @out != null && @out.on )
            unlink (@out);
            
            _out.rotx = in_rotx;
            _out.roty = in_roty;

            @out = _out;
            link(@out);
        }

        protected override void _start()
        {
            t = 0;
        }

        float t;
        protected override void _step()
        {
            if ( @out == null ) return;

            t = Mathf.Lerp (t, 1, .1f);

            rotx = Mathf.LerpAngle(in_rotx, @out.rotx, t);
            roty = Mathf.LerpAngle(in_roty, @out.roty, t);

            offset = Vector3.Lerp(in_offset, @out.offset, t);
            h = Mathf.Lerp(in_h, @out.h, t);
            distance = Mathf.Lerp(in_distance, @out.distance, t);

            tps_pos ();
        }

        protected override void _stop()
        {
            @out = null;
        }
    }
}
*/
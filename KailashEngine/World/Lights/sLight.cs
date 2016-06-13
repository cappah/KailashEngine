﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace KailashEngine.World.Lights
{
    class sLight : Light
    {

        private float _spot_angle;
        public float spot_angle
        {
            get { return _spot_angle; }
            set { _spot_angle = value; }
        }



        public sLight(string id, Vector3 position, Vector3 rotation, float size, Vector3 color, float intensity, float falloff, float spot_angle, bool shadow)
            : base(id, position, rotation, size, color, intensity, falloff, shadow)
        {
            _spot_angle = spot_angle;
        }

    }
}
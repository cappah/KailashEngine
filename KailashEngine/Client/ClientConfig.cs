﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using OpenTK;

namespace KailashEngine.Client
{
    class ClientConfig
    {

        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; }
        }


        //------------------------------------------------------
        // Paths
        //------------------------------------------------------

        private string _path_base;
        public string path_base
        {
            get { return _path_base; }
            set { _path_base = value; }
        }


        //------------------------------------------------------
        // OpenGL
        //------------------------------------------------------

        private int _gl_major_version;
        public int gl_major_version
        {
            get { return _gl_major_version; }
            set { _gl_major_version = value; }
        }

        private int _gl_minor_version;
        public int gl_minor_version 
        {
            get { return _gl_minor_version; }
            set { _gl_minor_version = value; }
        }

        private int _glsl_version;
        public int glsl_version
        {
            get { return _glsl_version; }
            set { _glsl_version = value; }
        }

        public string gl_version_string
        {
            get
            {
                return _gl_major_version + "." + _gl_minor_version;
            }
        }


        //------------------------------------------------------
        // Rendering
        //------------------------------------------------------

        private float _fps_target;
        public float fps_target
        {
            get { return _fps_target; }
            set { _fps_target = value; }
        }

        private Vector2 _near_far;
        public Vector2 near_far
        {
            get { return _near_far; }
            set { _near_far = value; }
        }




        public ClientConfig(string title, int gl_major_version, int gl_minor_version, float target_fps, float near_plane, float far_plane)
        {
            _title = title;

            // Set base engine path for defaults
            _path_base = Path.GetFullPath("../../../");

            _gl_major_version = gl_major_version;
            _gl_minor_version = gl_minor_version;

            _fps_target = target_fps;

            _near_far = new Vector2(near_plane, far_plane);

        }

    }
}
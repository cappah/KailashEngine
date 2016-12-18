﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

using KailashEngine.Client;
using KailashEngine.Render.Shader;
using KailashEngine.Render.Objects;
using KailashEngine.Output;

namespace KailashEngine.Render.FX
{
    class fx_Shadow : RenderEffect
    {
        // Propteries
        private const int _num_spot_shadows = 2;

        private const float _texture_scale = 0.5f;
        private Resolution _resolution_half;

        // Programs
        private Program _pSpot;
        private Program _pPoint;
        private Program _pDirectional;


        // Frame Buffers
        private FrameBuffer _fHalfResolution_Spot;


        // Textures
        private Texture _tDepth_Spot;

        private Texture _tSpot;
        public Texture tSpot
        {
            get { return _tSpot; }
        }



        public fx_Shadow(ProgramLoader pLoader, string glsl_effect_path, Resolution full_resolution)
            : base(pLoader, glsl_effect_path, full_resolution)
        {
            _resolution_half = new Resolution(_resolution.W * _texture_scale, _resolution.H * _texture_scale);
        }

        protected override void load_Programs()
        {
            _pSpot = _pLoader.createProgram_Geometry(new ShaderFile[]
            {
                new ShaderFile(ShaderType.GeometryShader, _path_glsl_effect + "shadow_Spot.geom", null),
                new ShaderFile(ShaderType.FragmentShader, _path_glsl_effect + "shadow_Spot.frag", null)
            });
            _pSpot.enable_MeshLoading();
        }

        protected override void load_Buffers()
        {
            _tDepth_Spot = new Texture(TextureTarget.Texture2DArray,
                _resolution_half.W, _resolution_half.H, _num_spot_shadows,
                false, false,
                PixelInternalFormat.DepthComponent32f, PixelFormat.DepthComponent, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tDepth_Spot.load();

            _tSpot = new Texture(TextureTarget.Texture2DArray,
                _resolution_half.W, _resolution_half.H, _num_spot_shadows, 
                false, false,
                PixelInternalFormat.Rgba16f, PixelFormat.Rgba, PixelType.Float,
                TextureMinFilter.Linear, TextureMagFilter.Linear, TextureWrapMode.Clamp);
            _tSpot.load();

            _fHalfResolution_Spot = new FrameBuffer("Shadow - Spot");
            _fHalfResolution_Spot.load(new Dictionary<FramebufferAttachment, Texture>()
            {
                { FramebufferAttachment.DepthAttachment, _tDepth_Spot },
                { FramebufferAttachment.ColorAttachment0, _tSpot },
            });
        }

        public override void load()
        {
            load_Programs();
            load_Buffers();
        }

        public override void unload()
        {

        }

        public override void reload()
        {

        }


        public void render(Scene scene)
        {
            GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Front);

            _fHalfResolution_Spot.bind(DrawBuffersEnum.ColorAttachment0);

            GL.DepthMask(true);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, _tSpot.width, _tSpot.height);

            _pSpot.bind();

            scene.renderMeshes(BeginMode.Triangles, _pSpot);

            //GL.CullFace(CullFaceMode.Back);
        }


    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Diagnostics;

namespace ThanaNita.MonoGameTnt
{
    public abstract class Game2D : Game
    {
        protected MouseInfo MouseInfo { get; set; }
        protected KeyboardInfo KeyboardInfo { get; set; }
        protected OrthographicCamera Camera { get; set; }
        protected GraphicsDeviceManager GraphicsManager { get; set; }

        // for Drawing
        protected Actor All { get; set; } = new Actor();
        protected EffectAdapter Adapter { get; set; }
        protected GenericBatch Batch { get; private set; }


        // Set any time
        protected Keys? ToggleFullScreenKey { get; set; } = Keys.F12;
        protected Keys? ExitKey { get; set; } = Keys.Escape;
        protected Color ClearColor { get; set; } = Color.Black;
        protected Color? BackgroundColor 
        {
            get => _backgroundColor;
            set { _backgroundColor = value; CreateBackgroundRect(); }
        }
        private Color? _backgroundColor = Color.CornflowerBlue;
        private RectangleActor background;

        // Initialized by the constructor
        protected ViewportAdapterTypes ViewportAdapterType { get; }
        protected Vector2 ScreenSize { get; }
        protected Vector2 PreferredWindowSize { get; }
        protected bool StartAsFullScreen { get; }
        public GraphicsDeviceConfig Config { get; private set; }
        public CollisionDetection CollisionDetectionUnit { get; private set; } = new CollisionDetection();

        public enum ViewportAdapterTypes { Boxing, Default, Scaling };

        public Game2D(ViewportAdapterTypes viewportAdapterType = ViewportAdapterTypes.Boxing, 
                        Vector2? virtualScreenSize = null,
                        Vector2? preferredWindowSize = null,
                        bool startAsFullScreen = false,
                        bool geometricalYAxis = false,
                        bool directLoadTextureCache = true)
        {
            GlobalConfig.GeometricalYAxis = geometricalYAxis;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GraphicsManager = new GraphicsDeviceManager(this);
            SetPreserveRenderTargetContent();

            // set parameters
            this.StartAsFullScreen = startAsFullScreen;
            this.ViewportAdapterType = viewportAdapterType;
            this.ScreenSize = virtualScreenSize ?? new Vector2(1920, 1080);
            this.PreferredWindowSize = preferredWindowSize ?? new Vector2(1920, 1080);
            TextureCache.Init(GraphicsDevice, Content, directLoadTextureCache);
        }

        private void SetPreserveRenderTargetContent()
        {
            GraphicsManager.PreparingDeviceSettings += (object s, PreparingDeviceSettingsEventArgs args) =>
            {
                args.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
            };
        }

        protected override void Initialize()
        {
            SetFullScreen(StartAsFullScreen, true);
            SetDefaultGraphicsStates();
            InitStatic();
            CameraSetup();
            CreateBatch();
            CreateBackgroundRect();
            MouseInfo = new MouseInfo(GraphicsDevice, Camera);
            KeyboardInfo = new KeyboardInfo();
            Window.KeyDown += Game2D_KeyDown;
            CreateGlobal();

            base.Initialize(); // This method will call LoadContent().
        }
        protected virtual void Game2D_KeyDown(object sender, InputKeyEventArgs e)
        {
            if (ToggleFullScreenKey != null && e.Key == ToggleFullScreenKey.Value)
                ToggleFullScreen();

            if (ExitKey != null && e.Key == ExitKey)
                this.Exit();

            DispatchKeyEvents(sender, e);
        }
        private void DispatchKeyEvents(object sender, InputKeyEventArgs e)
        {

        }

        private void CreateGlobal()
        {
            GlobalGraphicsDevice.Value = GraphicsDevice;
            GlobalMouseInfo.Value = MouseInfo;
            GlobalKeyboardInfo.Value = KeyboardInfo;
            GlobalGraphicsDeviceConfig.Value = Config;
            GlobalEffectAdapter.Value = Adapter;
        }

        private void InitStatic()
        {
            TextureCache.Init(GraphicsDevice, Content, directLoad: true);
        }

        protected virtual void SetDefaultGraphicsStates()
        {
            // for crisp pixel art and tiled texture
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            // for transpancy (e.g. in png)
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            // Z-buffer may not be relevant
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            GraphicsDevice.RasterizerState = new RasterizerState()
            {
                // For flipped Triangle (and when flipping Y Axis)
                CullMode = CullMode.None,

                // Currently Not Use. For Clip UI Using Scissor Rectangle
                // ScissorTestEnable = true 
            };
        }

        protected virtual void CameraSetup()
        {
            // camera
            var viewportAdapter = CreateViewportAdapter();
            Camera = new OrthographicCamera(viewportAdapter);
            Camera.Zoom = 1.0f;
            viewportAdapter.Reset();
        }

        private void CreateBatch()
        {
            Adapter = new SpriteEffectAdapter(GraphicsDevice);
            Batch = new GenericBatch(Adapter);
            Config = new GraphicsDeviceConfig(GraphicsDevice, Batch);
        }

        private void CreateBackgroundRect()
        {
            if (BackgroundColor != null)
                background = new RectangleActor(BackgroundColor.Value,
                                                new RectF(0, 0, ScreenSize.X, ScreenSize.Y));
            else
                background = null;
        }

        protected virtual ViewportAdapter CreateViewportAdapter()
        {
            if (ViewportAdapterType == ViewportAdapterTypes.Boxing)
                return new BoxingViewportAdapter(Window, GraphicsDevice, (int)ScreenSize.X, (int)ScreenSize.Y);
            else if (ViewportAdapterType == ViewportAdapterTypes.Default)
                return new DefaultViewportAdapter(GraphicsDevice);
            else if (ViewportAdapterType == ViewportAdapterTypes.Scaling)
                return new ScalingViewportAdapter(GraphicsDevice, (int)ScreenSize.X, (int)ScreenSize.Y);
            else
            {
                Debug.Assert(false, "Unknown ViewportAdapterTypes : " + ViewportAdapterType);
                return new DefaultViewportAdapter(GraphicsDevice);
            }
        }



        // ถ้าจะ override ตัวนี้ ต้องเรียก base.Update() ก่อนเลย เพราะจะเรียก MouseInfo.Update() ก่อน
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseInfo.Update();
            KeyboardInfo.Update();

            float deltaTime = CalcDeltaTime(gameTime);
            Update(deltaTime);
            //Act(deltaTime);
            All?.Act(deltaTime);
            AfterAct();
            CollisionDetectionUnit.DetectAndResolve(All);
            AfterUpdateAndCollision();
        }

        protected virtual void AfterAct()
        {
        }
        protected virtual void AfterUpdateAndCollision()
        {
        }

        private float CalcDeltaTime(GameTime gameTime)
        {
            return (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //[Obsolete("Use Act(deltaTime) instead.")]
        protected virtual void Update(float deltaTime) { }
        // protected virtual void Act(float deltaTime) { }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(ClearColor);

            Adapter.ViewMatrix = Camera.GetViewMatrix();

            DrawBackground();
            DrawAllActors();

            DrawAdditional(CalcDeltaTime(gameTime));
        }

        private void DrawBackground()
        {
            if (background == null)
                return;

            Config.SetBlendState(BlendState.Opaque);

            Batch.Begin();
            background.Draw(Batch, DrawState.Identity);
            Batch.End();

            Config.ResetBlendState();
        }

        private void DrawAllActors()
        {
            if (All == null)
                return;

            Batch.Begin();
            All.Draw(Batch, DrawState.Identity);
            Batch.End();
            //Debug.WriteLine($"batch count = {batch.BatchCount}");
        }

        protected virtual void DrawAdditional(float deltaTime) 
        { 
        }


        //******************** Toggle Full Screen **********************************************

        // https://community.monogame.net/t/get-the-actual-screen-width-and-height-on-windows-10-c-monogame/10006/2
        protected void ToggleFullScreen()
        {
            SetFullScreen(!GraphicsManager.IsFullScreen, false);
        }

        // https://community.monogame.net/t/how-to-implement-borderless-fullscreen-on-desktopgl-project/8359
        protected void SetFullScreen(bool bFullScreen, bool forcePreferredSize)
        {
            Vector2 display = new Vector2(
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            if (bFullScreen)
            {
                GraphicsManager.IsFullScreen = true;
                GraphicsManager.HardwareModeSwitch = false;
                GraphicsManager.PreferredBackBufferWidth = (int)display.X;
                GraphicsManager.PreferredBackBufferHeight = (int)display.Y;
                GraphicsManager.ApplyChanges();
            }
            else
            {
                Vector2 size = (forcePreferredSize || (display.X > PreferredWindowSize.X && display.Y > PreferredWindowSize.Y)
                        ) ? PreferredWindowSize : display * 0.75f;
                GraphicsManager.IsFullScreen = false;
                GraphicsManager.PreferredBackBufferWidth = (int)size.X;
                GraphicsManager.PreferredBackBufferHeight = (int)size.Y;
                GraphicsManager.ApplyChanges();
            }
            Window.AllowUserResizing = true;
        }
    }
}

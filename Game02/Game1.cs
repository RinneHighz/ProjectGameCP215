using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ThanaNita.MonoGameTnt;

namespace Game02
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Vector2 ScreenSize = new Vector2(1280, 720);
        private GenericBatch Batch;
        private Actor All;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TextureCache.Init(GraphicsDevice, Content, true);

            _graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            _graphics.PreferredBackBufferHeight = (int) ScreenSize.Y;
            _graphics.ApplyChanges();

            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Batch = BatchFactory.Create(GraphicsDevice);
            All = new Actor();

            for (int i = 0; i < 100; ++i)
            {
                int imgNo = RandomUtil.Next(2);
                if(imgNo == 0)
                    All.Add(CreateBall());
                else
                    All.Add(CreatePuppy());
            }
        }

        private Actor CreateBall()
        {
            var texture = TextureCache.Get("Ball.png");
            var ball = new SpriteActor(texture);
            ball.Origin = ball.RawSize / 2;
            //ball.Scale = new Vector2(4, 4);
            ball.Position = ScreenSize / 2;
            ball.AddAction(new RandomMover(ball));
            ball.AddAction(new RotateAction(ball, 360));
            return ball;
        }
        private Actor CreatePuppy()
        {
            var texture = TextureCache.Get("Puppy.jpg");
            var actor = new SpriteActor(texture);
            actor.Origin = actor.RawSize / 2;
            actor.Scale = new Vector2(0.3f, -0.3f);
            actor.Position = ScreenSize / 2;
            actor.AddAction(new RandomMover(actor));
            //actor.AddAction(new RotateAction(actor, -90));
            return actor;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            All.Act(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            Batch.Begin();
            All.Draw(Batch, DrawState.Identity);
            Batch.End();

            base.Draw(gameTime);
        }
    }
}

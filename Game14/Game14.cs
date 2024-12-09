
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game14
{
    public class Game14 : Game2D
    {
        ParticleEmitterPoint emitter;
        protected override void LoadContent()
        {
            var mouseBullet = new MouseBullet(ScreenSize);
            //mouseBullet.Position = new Vector2(-100, -100);
            //mouseBullet.Scale = new Vector2(1.2f, 1.2f);
            //mouseBullet.Rotation = 45;
            All.Add(mouseBullet);

            emitter = new ParticleEmitterPoint(0.02f, 10);
            emitter.StartPosition = ScreenSize / 2;
            All.Add(emitter);

            All.Add(new ParticleEmitterRandom(0.1f, 5, ScreenSize));

            Test4Ball();
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            emitter.StartPosition = GlobalMouseInfo.Value.WorldPosition;
        }

        private void Test4Ball()
        {
            float start = 40;
            float start2 = start + 520;
            All.Add(new Ball4(SamplerState.PointWrap) { Position = new Vector2(start, start) });
            All.Add(new Ball4(SamplerState.LinearWrap) { Position = new Vector2(start2, start) });
            All.Add(new Ball4(SamplerState.PointClamp) { Position = new Vector2(start, start2) });
            All.Add(new Ball4(SamplerState.LinearClamp) { Position = new Vector2(start2, start2) });
        }
    }
}

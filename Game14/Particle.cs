
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game14
{
    public class Particle : SpriteActor
    {
        public Particle()
        {
            SetTexture(TextureCache.Get("Particle.png"));
            Origin = RawSize / 2;

            var v = RandomUtil.Direction() * RandomUtil.NextSingle(20, 50);
            float duration = 2.0f;

            Color = Color.Yellow;
            Scale = new Vector2(2, 2);
            AddAction(new ColorAction(duration, Color.Red, this));
            AddAction(new ScaleToAction(duration, 0.1f * Scale, this));
            AddAction(Actions.FadeOut(duration, this));
            AddAction(new Mover(this, v));
            AddAction(new SequenceAction(
                        new DelayAction(duration),
                        new RunAction(() => this.Detach())
                ));
        }
    }
}

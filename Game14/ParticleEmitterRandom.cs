
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game14
{
    public class ParticleEmitterRandom : Actor
    {
        float interval;
        int emitCount;
        Vector2 screenSize;
        public ParticleEmitterRandom(float interval, int emitCount, Vector2 screenSize)
        {
            this.interval = interval;
            this.emitCount = emitCount;
            this.screenSize = screenSize;
        }

        float time = 0;
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);

            time += deltaTime;
            if (time >= interval)
            {
                Emit();
                time -= interval;
            }
        }
        private void Emit()
        {
            for (int i = 0; i < emitCount; i++)
            {
                var start = RandomUtil.Position(screenSize);
                Add(new Particle() { Position = start });
            }
        }
    }
}


using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game14
{
    public class ParticleEmitterPoint : Actor
    {
        float interval;
        int emitCount;
        public ParticleEmitterPoint(float interval, int emitCount)
        {
            this.interval = interval;
            this.emitCount = emitCount;
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

        public Vector2 StartPosition { get; set; }

        private void Emit()
        {
            for (int i = 0; i < emitCount; i++)
                Add(new Particle() { Position = StartPosition });
        }
    }
}

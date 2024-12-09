using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class ScaleToAction : TemporalAction
    {
        Actor actor;
        Vector2 startScale;
        Vector2 endScale;
        public ScaleToAction(float duration, Vector2 endScale, Actor actor, Interpolation interpolation = null)
            : base(duration, interpolation)
        {
            this.actor = actor;
            this.endScale = endScale;
        }

        protected override void Begin()
        {
            startScale = actor.Scale;
        }

        protected override void Update(float percent)
        {
            actor.Scale = InterpolationUtil.Apply(startScale, endScale, percent);
        }
    }
}

using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game02
{
    public class RotateAction : Action
    {
        protected Actor Actor { get; set; }
        public float AngularVelocity { get; set; }
        public RotateAction(Actor actor, float angularVelocity)
        {
            this.Actor = actor;
            this.AngularVelocity = angularVelocity;
        }

        public virtual bool Act(float deltaTime)
        {
            float deltaAngle = AngularVelocity * deltaTime; // ds = v * dt
            Actor.Rotation += deltaAngle;
            return false;
        }

        public void Restart()
        {
            
        }

        public bool IsFinished()
        {
            return false;
        }
    }
}

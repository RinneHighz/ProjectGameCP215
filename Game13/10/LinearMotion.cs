using System.Diagnostics;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game10
{
    public class LinearMotion : Action
    {
        Actor obj;
        Vector2 v;
        float t;
        public Vector2 TargetPosition { get; private set; }
        public Vector2 Direction { get; private set; }

        public void ToPreciseTarget()
        {
            if (obj == null)
                return;
            obj.Position = TargetPosition;
        }

        public LinearMotion(Actor obj, float speed, Vector2 targetPosition, Vector2 direction)
        {
            Debug.Assert(speed > 0);
            this.obj = obj;
            TargetPosition = targetPosition;
            Direction = direction;

            if (obj == null)
            {
                v = Vector2.Zero;
                t = 0;
                return;
            }
            var ds = targetPosition - obj.Position;
            t = ds.Length() / speed;
            if (t == 0.0f)
                v = new Vector2(0, 0);
            else
                v = ds / t;
        }

        public bool IsFinished()
        {
            return t <= 0 || obj == null;
        }

        public bool Act(float deltaTime)
        {
            if (IsFinished())
                return true;

            obj.Position += v * deltaTime;
            t -= deltaTime;
            return false;
        }

        public static LinearMotion Empty() 
        { 
            return new LinearMotion(null, 1, new Vector2(), new Vector2()); 
        }

        public void Restart()
        {
            throw new System.NotImplementedException();
        }
    }
}

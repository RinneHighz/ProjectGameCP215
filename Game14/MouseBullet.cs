
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game14
{
    public class MouseBullet : Actor
    {
        Vector2 startPosition;
        public MouseBullet(Vector2 screenSize)
        {
            this.RawSize = screenSize;

            startPosition = screenSize/2;
            Add(new CrossHair(startPosition));
        }

        float time;
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            var mouseInfo = GlobalMouseInfo.Value;

            if (time < 0)
                time += deltaTime;

            if(mouseInfo.IsLeftButtonDown() && time >= 0)
            {
                var world = mouseInfo.WorldPosition;
                var click = GlobalTransform.GetInverse().Transform(world);

                var v = (click - startPosition).UnitVector() * 800;
                var ball = new Ball(v) { Position = startPosition };
                Add(ball);

                time = -0.1f; // cooldown
            }
        }
    }
}

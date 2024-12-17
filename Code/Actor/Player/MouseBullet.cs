
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class MouseBullet : Actor
    {
        Vector2 startPosition;
        public MouseBullet(Vector2 startPosition)
        {
            this.RawSize = startPosition;
            // this.startPosition = startPosition;

            // startPosition = screenSize -screenSize;
            Add(new CrossHair(startPosition));
            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            Add(collisionObj);
        }

        public void OnCollide(CollisionObj objB, CollideData collideData)
        {
            var enemy = objB.Actor as Slime;
            enemy?.Detach();

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

                time = -1.0f; // cooldown
            }
        }
    }
}

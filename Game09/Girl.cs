using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Girl : SpriteActor
    {
        AnimationStates states;
        Vector2 V;
        bool onFloor = false;
        public Girl(Vector2 position)
        {
            var size = new Vector2(60, 60);
            Position = position;
            Origin = size / 2;
            Scale = new Vector2(2, 2);

            var texture = TextureCache.Get("Girl.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var selector = new RegionSelector(regions2d);
            var stay = new Animation(this, 1.0f, selector.Select1by1(0, 4));
            var left = new Animation(this, 1.0f, selector.Select(start: 8, count: 8));
            var right = new Animation(this, 1.0f, selector.Select(start: 16, count: 8));
            states = new AnimationStates([stay, left, right]);
            AddAction(states);

            var collisionObj = CollisionObj.CreateWithRect(this, RawRect.CreateAdjusted(0.4f, 1), 1);
            collisionObj.OnCollide = OnCollide;
            collisionObj.DebugDraw = false;
            Add(collisionObj);
        }

        private void OnCollide(CollisionObj objB, CollideData data)
        {
            var direction = data.objA.RelativeDirection(data.OverlapRect);

            if(direction.Y > 0)
                onFloor = true;

            if( (direction.Y > 0 && V.Y > 0) ||
                (direction.Y < 0 && V.Y < 0)
                )
            {
                V.Y = 0;
                Position -= new Vector2(0, data.OverlapRect.Height * direction.Y);
            }

            if ((direction.X > 0 && V.X > 0) ||
                (direction.X < 0 && V.X < 0)
                )
            {
                V.X = 0;
                Position -= new Vector2(data.OverlapRect.Width * direction.X, 0);
            }
        }

        public override void Act(float deltaTime)
        {
            ChangeVy(deltaTime);

            var direction = DirectionKey.Direction;
            V.X = direction.X * 500; // เปลี่ยนแค่ V.X

            if (direction.X > 0)
                states.Animate(2);
            else if (direction.X < 0)
                states.Animate(1);
            else
                states.Animate(0);

            base.Act(deltaTime);
            Position += V * deltaTime; // s += v*dt
            onFloor = false;
        }

        private void ChangeVy(float deltaTime)
        {
            // 1. Gravitation
            var a = new Vector2(0, 1500);
            V.Y += a.Y * deltaTime;

            // 2. Jump
            var keyInfo = GlobalKeyboardInfo.Value;
            if (keyInfo.IsKeyPressed(Keys.Space) && onFloor)
                V.Y = -750;

            // 3. Jet
            if (keyInfo.IsKeyDown(Keys.Tab))
                V.Y = -500;
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class RandomMover : Mover
    {
        private float timePassed;
        private float duration;
        private Actor target; // เป้าหมายที่ Slime จะเคลื่อนที่เข้าหา
        Actor actor;
        private float speed;  // ความเร็วของ Slime



        public RandomMover(Actor actor, Actor target) : base(actor, new Vector2())
        {
            this.target = target;
            this.actor = actor;
        }
        public override bool Act(float deltaTime)
        {
            if (target == null) return base.Act(deltaTime);


            timePassed += deltaTime;
            if (timePassed >= duration)
            {

                timePassed = 0;
                duration = RandomUtil.NextSingle() * 4 + 0.5f; 

                float speed = 2;
                Vector2 direction = target.Position - actor.Position;

                if (direction.LengthSquared() > 0)
                {
                    direction.Normalize();
                }

                Velocity = speed * direction;
                // หรือจะเรียก RandomUtil.Direction() ก็ได้แล้วคูณด้วย speed
            }

            return base.Act(deltaTime);
        }
    }
}

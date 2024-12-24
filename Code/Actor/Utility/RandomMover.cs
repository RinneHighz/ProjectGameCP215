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
        private Actor actor;
        private float speed; // ความเร็วของ Slime

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
                duration = RandomUtil.NextSingle() * 4 + 0.5f; // สุ่มระยะเวลาใหม่ทุกครั้ง

                speed = 100; // ความเร็วของ Slime
                Vector2 direction;
                float distance = Vector2.Distance(actor.Position, target.Position);


                // เดินเข้าหาเป้าหมายตามปกติ
                direction = target.Position - actor.Position;

                if (direction.LengthSquared() > 0)
                {
                    direction.Normalize();
                }


                Velocity = speed * direction; // ตั้งค่าความเร็ว
            }

            return base.Act(deltaTime);
        }
    }
}

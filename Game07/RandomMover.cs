using Microsoft.Xna.Framework;
using System;
using ThanaNita.MonoGameTnt;

namespace Game07
{
    public class RandomMover : Mover
    {
        private float timePassed;
        private float duration;
        public RandomMover(Actor actor) : base(actor, new Vector2())
        {
        }
        public override bool Act(float deltaTime)
        {
            timePassed += deltaTime;
            if (timePassed >= duration)
            {

                timePassed = 0;
                duration = RandomUtil.NextSingle() * 4 + 0.5f; // สุ่มเวลา 0.5 ถึง 4.5 วินาที

                float speed = 30;
                float angle = RandomUtil.NextSingle() * 2 * MathF.PI;
                Velocity = speed * new Vector2(MathF.Cos(angle), MathF.Sin(angle));
                            // หรือจะเรียก RandomUtil.Direction() ก็ได้แล้วคูณด้วย speed
            }

            return base.Act(deltaTime);
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using ThanaNita.MonoGameTnt;

namespace Game03
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
                duration = 0.5f + RandomUtil.NextSingle() * 4; // สุ่มเวลา 0.5 ถึง 4.5 วินาที

                float speed = 50;
                Velocity = RandomUtil.Direction() * speed;
                AngularVelocity = RandomUtil.NextSingle(-90, 90);
            }

            return base.Act(deltaTime);
        }
    }
}

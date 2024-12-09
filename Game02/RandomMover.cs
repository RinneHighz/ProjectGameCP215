using Microsoft.Xna.Framework;
using System;
using ThanaNita.MonoGameTnt;

namespace Game02
{
    public class RandomMover : Mover
    {
        private float timePassed;
        private float duration;
        private Actor actor;
        public RandomMover(Actor actor) : base(actor)
        {
            this.actor = actor;
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
                int min = 220;
                actor.Color =
                    new Color(
                            RandomUtil.Next(min, 256),
                            RandomUtil.Next(min, 256),
                            RandomUtil.Next(min, 256));
            }

            return base.Act(deltaTime);
        }
    }
}

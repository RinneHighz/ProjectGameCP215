using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game03
{
    public class BigBall : Ball
    {
        public BigBall(Vector2 position)
            : base(position)
        {
            Scale = new Vector2(2, 2);

            var empty = new Actor();
            //empty.AddAction(new Mover(empty, 360));
            Add(empty);

            empty.Add(CreateSmall(new Vector2(60, 0) + RawSize / 2, Color.Red));
            empty.Add(CreateSmall(new Vector2(-60, 0) + RawSize / 2, Color.GreenYellow));
        }
        public static BigBall StillBall(Vector2 position)
        {
            var ball = new BigBall(position);
            ball.ClearAction();
            return ball;
        }

        private Actor CreateSmall(Vector2 position, Color color)
        {
            var small = Ball.StillBall(position);
            small.Scale = new Vector2(0.5f, 0.5f);
            small.Color = color;
            return small;
        }
    }
}

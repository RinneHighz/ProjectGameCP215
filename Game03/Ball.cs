using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game03
{
    public class Ball : SpriteActor
    {
        public Ball(Vector2 position)
        {
            var texture = TextureCache.Get("Ball.png");
            SetTexture(texture);
            Origin = RawSize / 2;
            Position = position;
            AddAction(new RandomMover(this));
        }

        public static Ball StillBall(Vector2 position)
        {
            var ball = new Ball(position);
            ball.ClearAction();            
            return ball;
        }
    }
}

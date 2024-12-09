using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game04
{
    public class Ball : SpriteActor
    {
        public Ball(Vector2 position)
        {
            var texture = TextureCache.Get("Ball.png");
            SetTexture(texture);
            Origin = RawSize / 2;
            Position = position;
        }

        public override void Act(float deltaTime)
        {
            Position += -DirectionKey.Direction * 1500 * deltaTime;

            base.Act(deltaTime);
        }
    }
}

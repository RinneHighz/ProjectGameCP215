using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using ThanaNita.MonoGameTnt;

namespace Game06
{
    public class Aladdin : SpriteActor
    {
        Animation animation;
        public Aladdin(Vector2 position)
        {
            var size = new Vector2(45, 45);
            var sprite = this;
            sprite.Position = position;
            sprite.Origin = size / 2;
            sprite.Scale = new Vector2(4, 4);

            var texture = TextureCache.Get("Aladdin.png");
            var regions2d = RegionCutter.Cut(texture, size, countX: 4, countY: 4);
            var regions1d = RegionSelector.Select(regions2d, start: 5, count: 8);
            animation = new Animation(sprite, 0.5f, regions1d);
            sprite.AddAction(animation);
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);

            var keyInfo = GlobalKeyboardInfo.Value;
            if (keyInfo.IsKeyPressed(Keys.Space))
            {
                animation.Restart();
                animation.Running = !animation.Running;
            }
        }
    }
}

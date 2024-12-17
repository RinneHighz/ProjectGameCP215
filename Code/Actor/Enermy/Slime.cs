using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class Slime : SpriteActor
    {
        Animation[] animationArray;

        public Slime(Vector2 position)
        {
            var size = new Vector2(18, 18);
            Position = position;
            Origin = size / 2;
            Scale = new Vector2(4, 4);

            var texture = TextureCache.Get("Content/Resource/SpriteSheet/Slime.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var selector = new RegionSelector(regions2d);
            var stay = new Animation(this, 1.0f, selector.Select1by1(0, 1));


            animationArray = [stay];
            AddAction(stay);
            // AddAction(new RandomMover(this));
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            Add(collisionObj);
        }

        int last = 1;
        public void Animate(int index)
        {
            PlayOnly(index);
            if (index != last)
            {
                animationArray[index].Restart();
                last = index;
            }
        }

        private void PlayOnly(int playIndex)
        {
            for (int i = 0; i < animationArray.Length; i++)
            {
                animationArray[i].Running = (i == playIndex);
            }
        }

    }
}

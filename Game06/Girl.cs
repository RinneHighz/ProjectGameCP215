using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game06
{
    public class Girl : SpriteActor
    {
        //Animation[] animationArray;
        AnimationStates states;
        public Girl(Vector2 position)
        {
            var size = new Vector2(60, 60);
            var sprite = this;
            sprite.Position = position;
            sprite.Origin = size / 2;
            sprite.Scale = new Vector2(4, 4);

            var texture = TextureCache.Get("Girl.png");
            var regions2d = RegionCutter.Cut(texture, size, countX: 8, countY: 4);

            var selector = new RegionSelector(regions2d);
            var stay = new Animation(sprite, 1.0f, selector.Select1by1(0, 1, 7, 3, 4, 0));
            var left = new Animation(sprite, 1.0f, selector.Select(8, 8));
            var right = new Animation(sprite, 1.0f, selector.Select(16, 8));
            var up = new Animation(sprite, 1.0f, selector.Select(24, 8));
            var down = new Animation(sprite, 1.0f, selector.Select(0, 8));
            states = new AnimationStates([stay, left, right, up, down]);
            AddAction(states);
        }

/*        int last = -1;
        public void Animate(int index)
        {
            for (int i = 0; i < animationArray.Length; i++)
                animationArray[i].Running = (i == index);

            if (index != last)
                animationArray[index].Restart();
            last = index;
        }
*/
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);

            var direction = DirectionKey.Normalized;
            Position += 1000 * direction * deltaTime;

            if (direction.X > 0)
                states.Animate(2); // right
            else if(direction.X < 0)
                states.Animate(1); // left
            else if(direction.Y > 0)
                states.Animate(4); // down
            else if(direction.Y < 0)
                states.Animate(3); // up
            else
                states.Animate(0);
        }

    }
}

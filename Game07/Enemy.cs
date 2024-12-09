using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game07
{
    public class Enemy : SpriteActor
    {
        public Enemy(Vector2 position)
        {
            SetTexture(TextureCache.Get("Puppy.jpg"));
            Origin = RawSize / 2;
            Scale = new Vector2(0.25f, 0.25f);
            Position = position;
            Color = RandomUtil.LightColor();

            AddAction(new RandomMover(this));

            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            Add(collisionObj);
        }
    }
}

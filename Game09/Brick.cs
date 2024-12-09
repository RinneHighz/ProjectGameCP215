using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Brick : SpriteActor
    {
        public Brick(RectF rect)
        {
            var texture = TextureCache.Get("Brick.png");
            SetTextureRegion(
                new TextureRegion(texture, rect));
            Position = rect.Position;

            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            collisionObj.DebugDraw = false;
            Add(collisionObj);
        }
    }
}

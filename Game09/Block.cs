using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Block : RectangleActor
    {
        public Block(RectF rect)
            : base(Color.White, rect)
        {
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            collisionObj.DebugDraw = false;
            Add(collisionObj);
        }
    }
}

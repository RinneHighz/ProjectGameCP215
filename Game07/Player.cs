using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game07
{
    public class Player : RectangleActor
    {
        public Vector2 V => mover.Velocity;
        private KeyboardMover mover;
        public Player(Actor all, Vector2 screenSize)
            : base(Color.Blue, new Vector2(30,60))
        {
            Origin = RawSize / 2;
            Position = new Vector2(screenSize.X / 2, screenSize.Y - 100);

            AddAction(mover = new KeyboardMover(this, 500));
            AddAction(new Shooter(all, this));

            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            Add(collisionObj);
        }

        // Collision Response
        private void OnCollide(CollisionObj objB, CollideData data)
        {
            if (objB.Actor is Enemy)
                objB.Actor.Color = Color.Black;
        }
    }
}

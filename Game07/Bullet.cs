using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game07
{
    public class Bullet : RectangleActor
    {
        public Bullet(Player player)
            : base(Color.Red, new Vector2(8, 16))
        {
            Color = RandomUtil.Color();
            Origin = RawSize / 2;
            Position = player.Position;
            var v = new Vector2(0, -700) + player.V;
            AddAction(new Mover(this, v));

            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            Add(collisionObj);
        }

        private void OnCollide(CollisionObj objB, CollideData data)
        {
            var enemy = objB.Actor as Enemy;
            enemy?.Detach();

            // (objB.Actor as Enemy)?.Detach();

            // (objB.Actor as Enemy)?.Detach();

            //if (enemy != null)
            //    enemy.Detach();
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            if (Position.Y < -100)
                this.Detach();
        }
    }
}

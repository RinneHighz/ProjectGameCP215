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

        private float timePassed;
        private float duration;
        private Vector2 V;

        public Slime(Vector2 position)
        {
            var size = new Vector2(18, 18);
            Position = position;
            Origin = size / 2;
            Scale = new Vector2(2, 2);

            var texture = TextureCache.Get("Content/Resource/SpriteSheet/Slime.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var selector = new RegionSelector(regions2d);
            var stay = new Animation(this, 1.0f, selector.Select1by1(0, 1));


            animationArray = [stay];
            AddAction(stay);
            // AddAction(new RandomMover(this));
            var collisionObj = CollisionObj.CreateWithRect(this, 2);
            collisionObj.OnCollide = OnCollide;
            collisionObj.DebugDraw = true;
            Add(collisionObj);
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            timePassed += deltaTime;
            if (timePassed >= duration)
            {

                timePassed = 0;
                duration = RandomUtil.NextSingle() * 4 + 0.5f; // สุ่มเวลา 0.5 ถึง 4.5 วินาที

                float speed = 100;
                float angle = RandomUtil.NextSingle() * 2 * MathF.PI;
                V = speed * new Vector2(MathF.Cos(angle), MathF.Sin(angle));
                            // หรือจะเรียก RandomUtil.Direction() ก็ได้แล้วคูณด้วย speed
            }
            // อัปเดตตำแหน่ง
            Position += V * deltaTime;

        }

        public void OnCollide(CollisionObj objB, CollideData data)
        {
            var direction = data.objA.RelativeDirection(data.OverlapRect);

            if ((direction.Y > 0 && V.Y > 0) ||
                (direction.Y < 0 && V.Y < 0))
            {
                V.Y = 0;
                Position -= new Vector2(0, data.OverlapRect.Height * direction.Y);
            }
            if ((direction.X > 0 && V.X > 0) ||
                (direction.X < 0 && V.X < 0))
            {
                V.X = 0;
                Position -= new Vector2(data.OverlapRect.Width * direction.X, 0);
            }

            
        }

    }
}

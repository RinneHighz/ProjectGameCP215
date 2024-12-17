using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class MaleActor : SpriteActor
    {
        AnimationStates states;
        Vector2 V;
        public int score { get; set; } = 0; // เพิ่มตัวแปรเก็บคะแนน
        public float maxHp { get; set; } = 100; // เลือดสูงสุด
        public float hp { get; set; }    // เลือดปัจจุบัน

        public MaleActor(Vector2 position)
        {
            hp = maxHp;
            var size = new Vector2(32, 48);
            Position = position;
            Origin = new Vector2(16, 40);
            Scale = new Vector2(1, 1);

            var texture = TextureCache.Get("Content/Resource/SpriteSheet/MaleActor.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var selector = new RegionSelector(regions2d);

            var stay = new Animation(this, 1.0f, selector.Select1by1(0, 0));
            var left = new Animation(this, 1.0f, selector.Select(start: 4, count: 4));
            var right = new Animation(this, 1.0f, selector.Select(start: 8, count: 4));
            var up = new Animation(this, 1.0f, selector.Select(start: 12, count: 4));
            var down = new Animation(this, 1.0f, selector.Select(start: 0, count: 4));

            states = new AnimationStates([stay, left, right, up, down]);
            AddAction(states);

            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            collisionObj.DebugDraw = true;
            Add(collisionObj);


            // เพิ่ม MouseBullet และส่ง this ไปยัง MouseBullet
            var mouseBullet = new MouseBullet(Origin, this);
            Add(mouseBullet);
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);

            var keyInfo = GlobalKeyboardInfo.Value;
            Vector2 direction = Vector2.Zero;

            // เช็คปุ่ม WASD
            if (keyInfo.IsKeyDown(Keys.W)) direction.Y = -1; // ขึ้น
            if (keyInfo.IsKeyDown(Keys.S)) direction.Y = 1;  // ลง
            if (keyInfo.IsKeyDown(Keys.A)) direction.X = -1; // ซ้าย
            if (keyInfo.IsKeyDown(Keys.D)) direction.X = 1;  // ขวา

            // เช็คปุ่มลูกศร (Arrow Keys)
            direction += DirectionKey.Direction;

            // ปรับความเร็ว
            V = direction * 500 / 2;

            // ตั้งค่าการแสดงอนิเมชัน
            if (direction.X > 0)
                states.Animate(2);  // เดินขวา
            else if (direction.X < 0)
                states.Animate(1);  // เดินซ้าย
            else if (direction.Y < 0)
                states.Animate(3);  // เดินขึ้น
            else if (direction.Y > 0)
                states.Animate(4);  // เดินลง
            else
                states.Animate(0);  // อยู่กับที่

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

            var slime = objB.Actor as Slime;
            slime?.Detach();

            if (hp - 10 <= 0)
            {
                hp = 0;
            }
            else
            {
                hp -= 10;
            }
        }



    }
}

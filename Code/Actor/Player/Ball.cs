using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class Ball : SpriteActor
    {
        public Mover Mover { get; init; }

        private MaleActor player; // เพิ่ม reference ไปยัง MaleActor

        public Ball(Vector2 v, MaleActor player)
        {
            this.player = player;
            // SetTexture(TextureCache.Get("Content/Resource/SpriteSheet/Ball.png"));
            // Origin = RawSize / 2;
            // Scale = new Vector2(0.5f, 0.5f);

            var size = new Vector2(16, 16);
            Origin = size / 2;
            Scale = new Vector2(2, 2);

            var texture = TextureCache.Get("Content/Resource/SpriteSheet/Bullet.png");
            var regions2d = RegionCutter.Cut(texture, size);
            var selector = new RegionSelector(regions2d);
            var stay = new Animation(this, 1.0f, selector.Select1by1(0, 4));

            AddAction(stay);

            AddAction(Mover = new Mover(this, v));
            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            Add(collisionObj);
        }
        public void OnCollide(CollisionObj objB, CollideData collideData)
        {
            var slime = objB.Actor as Slime;
            if (slime != null)
            {
                slime.Detach(); // ลบ Slime ออกจากเกม
                this.Detach();  // ลบ Ball หลังจากชน

                player.score += 1; // เพิ่มคะแนนให้ MaleActor
                player.exp += 2; // เพิ่ม EXP 2 จาก Slime
                player.CheckLevelUp(); // ส่ง PlayState ไปด้วย
            }

            var boss = objB.Actor as Boss;
            if (boss != null)
            {
                if (boss.hp - 10 >= 0)
                {
                    boss.hp -= 10;
                }
                else
                {
                    boss.Detach();
                    player.score += 10;
                    player.exp += 7; // เพิ่ม EXP 7 จาก Boss
                    player.CheckLevelUp();
                }
                this.Detach();  // ลบ Ball หลังจากชน

            }
        }
    }
}

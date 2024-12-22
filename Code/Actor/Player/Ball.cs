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
            SetTexture(TextureCache.Get("Content/Resource/SpriteSheet/Ball.png"));
            Origin = RawSize / 2;

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
            }

            var boss = objB.Actor as Boss;
            if (boss != null)
            {
                if(boss.hp - 10 >= 0){
                    boss.hp -= 10;
                }else{
                    boss.Detach();
                    player.score += 10;
                }
                this.Detach();  // ลบ Ball หลังจากชน

            }
        }
    }
}

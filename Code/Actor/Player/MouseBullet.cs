
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class MouseBullet : Actor
    {
        Vector2 startPosition;
        MaleActor player; // Reference ไปยัง MaleActor
        SoundEffect gunShot;
        public MouseBullet(Vector2 startPosition, MaleActor player)
        {
            this.player = player; // รับ MaleActor เป็นพารามิเตอร์
            this.RawSize = startPosition;
            

            gunShot = SoundEffect.FromFile("Content/Resource/Sound/GunShotEffect.wav");
            Add(new CrossHair(startPosition));
            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            Add(collisionObj);
        }

        public void OnCollide(CollisionObj objB, CollideData collideData)
        {
            var enemy = objB.Actor as Slime;
            enemy?.Detach();

        }

        float time;
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            var mouseInfo = GlobalMouseInfo.Value;

            if (time < 0)
                time += deltaTime;

             if (mouseInfo.IsLeftButtonDown() && time >= 0)
            {
                var world = mouseInfo.WorldPosition;
                var click = GlobalTransform.GetInverse().Transform(world);

                var v = (click - startPosition).UnitVector() * 800;
                var ball = new Ball(v, player) { Position = startPosition }; // ส่ง player ไปยัง Ball
                Add(ball);

                gunShot.Play();
                time = -1.0f; // cooldown

            }
        }
    }
}

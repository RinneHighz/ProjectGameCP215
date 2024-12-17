using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class Ball : SpriteActor
    {
        public Mover Mover { get; init; }

        public Ball(Vector2 v)
        {
            SetTexture(TextureCache.Get("Content/Resource/SpriteSheet/Ball.png"));
            Origin = RawSize / 2;
            
            AddAction(Mover = new Mover(this, v));
            var collisionObj = CollisionObj.CreateWithRect(this, 1);
            collisionObj.OnCollide = OnCollide;
            Add(collisionObj);
        }
        public void OnCollide(CollisionObj objB, CollideData collideData)
        {
            var enemy = objB.Actor as Slime;
            enemy?.Detach();
            this.Detach();

        }
    }
}

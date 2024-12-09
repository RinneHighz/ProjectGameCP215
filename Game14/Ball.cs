using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game14
{
    public class Ball : SpriteActor
    {
        public Mover Mover { get; init; }

        public Ball(Vector2 v)
        {
            SetTexture(TextureCache.Get("Ball.png"));
            Origin = RawSize / 2;
            
            AddAction(Mover = new Mover(this, v));
        }
    }
}

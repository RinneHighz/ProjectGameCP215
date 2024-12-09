using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game10
{
    public class Player : SpriteActor
    {
        public Player()
        {
            var texture = TextureCache.Get("Guy.png");
            SetTextureRegion(new TextureRegion(texture, new RectF(0, 0, 32, 48)));
            Origin = new Vector2(16, 40);
            Scale = new Vector2(4, 4);
        }
    }
}

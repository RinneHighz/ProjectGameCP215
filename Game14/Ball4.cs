using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game14
{
    public class Ball4 : SpriteActor
    {
        SamplerState samplerState;
        public Ball4(SamplerState samplerState)
        {
            this.samplerState = samplerState;
            var textureRegion = new TextureRegion(
                                TextureCache.Get("Ball.png"),
                                new RectF(-40, -40, 160, 160));


            SetTextureRegion(textureRegion);
            Scale = new Vector2(3, 3);

            Add(new HollowRectActor(Color.Blue, 1, RawRect));
        }

        public override void Draw(DrawTarget target, DrawState state)
        {
            var config = GlobalGraphicsDeviceConfig.Value;

            config.SetSamplerState(samplerState);
            base.Draw(target, state);
            config.ResetSamplerState();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class SpriteActor : Actor
    {
        private TextureRegion region; // struct
        private VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[4];
        private short[] indices = new short[6];
        public override Vector2 RawSize => region.Size;

        public SpriteActor()
        {
        }
        public SpriteActor(Texture2D texture)
            : this(new TextureRegion(texture))
        {
        }
        public SpriteActor(TextureRegion region)
        {
            this.region = region;
            UpdatePositionColorTexture();
        }
        public void SetTexture(Texture2D texture)
        {
            SetTextureRegion(new TextureRegion(texture));
        }
        public void SetTextureRegion(TextureRegion region)
        {
            this.region = region;
            UpdatePositionColorTexture();
        }
        private void UpdatePositionColorTexture()
        {
            UpdatePositionTexture();
            UpdateColor();
        }
        private void UpdatePositionTexture()
        {
            if (region.Texture == null)
                return;
            RectF rect = // normalized texture rect
                DrawableUtil.NormalizeRect(region.Region, region.Texture);

            float w = region.Region.Width;
            float h = region.Region.Height;
            vertices[0].Position = new Vector3(0, 0, 0);
            vertices[1].Position = new Vector3(w, 0, 0);
            vertices[2].Position = new Vector3(0, h, 0);
            vertices[3].Position = new Vector3(w, h, 0);

            float l = rect.X;
            float r = rect.XMax;
            float t = !GlobalConfig.GeometricalYAxis ? rect.Y : rect.YMax;
            float b = !GlobalConfig.GeometricalYAxis ? rect.YMax : rect.Y;
            vertices[0].TextureCoordinate = new Vector2(l, t);
            vertices[1].TextureCoordinate = new Vector2(r, t);
            vertices[2].TextureCoordinate = new Vector2(l, b);
            vertices[3].TextureCoordinate = new Vector2(r, b);

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 1;
            indices[4] = 3;
            indices[5] = 2;
        }
        private void UpdateColor()
        {
            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Color = Color.White;
        }

        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);

            if (region.Texture == null)
                return;

            target.Draw(vertices, indices, region.Texture, CombineState(state));
        }
    }
}

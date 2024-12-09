
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class NinePatchRect : Actor
    {
        private TextureRegion region; // struct
        private VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[16];
        private short[] indices = new short[6*9];
        Vector2 size;
        public override Vector2 RawSize => size;
        Vector2 offset1;
        Vector2 offset2;

        public NinePatchRect()
        {
        }
        public NinePatchRect(Texture2D texture, Vector2 offset1, Vector2? offset2 = null)
            : this(new TextureRegion(texture), offset1, offset2)
        {
        }
        public NinePatchRect(TextureRegion region, Vector2 offset1, Vector2? offset2 = null)
        {
            SetTextureRegion(region, offset1, offset2);
            size = region.Size;
        }
        public void SetTexture(Texture2D texture, Vector2 offset1, Vector2? offset2 = null)
        {
            SetTextureRegion(new TextureRegion(texture), offset1, offset2);
        }
        public void SetTextureRegion(TextureRegion region, Vector2 offset1, Vector2? offset2_ = null)
        {
            this.region = region;
            Vector2 offset2 = offset2_ ?? offset1;
            this.offset1 = offset1;
            this.offset2 = offset2;

            var pos = region.Region.Position;
            var rect = region.Region;
            UpdateVerticesTextureColor(new(0, 0), pos, new RectF(rect.Position, offset1));
            UpdateVerticesTextureColor(new(2, 0), pos, new RectF(new(rect.XMax - offset2.X, rect.Y), new Vector2(offset2.X, offset1.Y)));
            UpdateVerticesTextureColor(new(0, 2), pos, new RectF(new(rect.X, rect.YMax - offset2.Y), new Vector2(offset1.X, offset2.Y)));
            UpdateVerticesTextureColor(new(2, 2), pos, new RectF(new(rect.XMax - offset2.X, rect.YMax - offset2.Y), offset2));
            UpdateIndices( 0, new(0, 0));
            UpdateIndices( 6, new(1, 0));
            UpdateIndices(12, new(2, 0));
            UpdateIndices(18, new(0, 1));
            UpdateIndices(24, new(1, 1));
            UpdateIndices(30, new(2, 1));
            UpdateIndices(36, new(0, 2));
            UpdateIndices(42, new(1, 2));
            UpdateIndices(48, new(2, 2));
        }
        public void SetSize(Vector2 rawSize)
        {
            size = rawSize;
            var pos = region.Region.Position;
            var rect = new RectF(pos, rawSize);

            UpdateV(new(2, 0), pos, new RectF(new(rect.XMax - offset2.X, rect.Y), new Vector2(offset2.X, offset1.Y)));
            UpdateV(new(0, 2), pos, new RectF(new(rect.X, rect.YMax - offset2.Y), new Vector2(offset1.X, offset2.Y)));
            UpdateV(new(2, 2), pos, new RectF(new(rect.XMax - offset2.X, rect.YMax - offset2.Y), offset2));
        }
        private void UpdateVerticesTextureColor(Vector2i vStart, Vector2 pos, RectF textureRect)
        {
            if (region.Texture == null)
                return;
            RectF normalizedRect = // normalized texture rect
                DrawableUtil.NormalizeRect(textureRect, region.Texture);

            int i0 = To1D(vStart);
            int i1 = To1D(vStart.PlusX());
            int i2 = To1D(vStart.PlusY());
            int i3 = To1D(vStart.PlusXY());

            UpdateV(vStart, pos, textureRect);

            float l = normalizedRect.X;
            float r = normalizedRect.XMax;
            float t = !GlobalConfig.GeometricalYAxis ? normalizedRect.Y : normalizedRect.YMax;
            float b = !GlobalConfig.GeometricalYAxis ? normalizedRect.YMax : normalizedRect.Y;
            vertices[i0].TextureCoordinate = new Vector2(l, t);
            vertices[i1].TextureCoordinate = new Vector2(r, t);
            vertices[i2].TextureCoordinate = new Vector2(l, b);
            vertices[i3].TextureCoordinate = new Vector2(r, b);

            vertices[i0].Color = Color.White;
            vertices[i1].Color = Color.White;
            vertices[i2].Color = Color.White;
            vertices[i3].Color = Color.White;

        }

        private void UpdateV(Vector2i vStart, Vector2 pos, RectF textureRect)
        {
            int i0 = To1D(vStart);
            int i1 = To1D(vStart.PlusX());
            int i2 = To1D(vStart.PlusY());
            int i3 = To1D(vStart.PlusXY());

            var pos3 = new Vector3(pos, 0);
            vertices[i0].Position = new Vector3(textureRect.Position, 0) - pos3;
            vertices[i1].Position = new Vector3(textureRect.XMax, textureRect.Y, 0) - pos3;
            vertices[i2].Position = new Vector3(textureRect.X, textureRect.YMax, 0) - pos3;
            vertices[i3].Position = new Vector3(textureRect.MaxPoint, 0) - pos3;
        }

        private void UpdateIndices(int iStart, Vector2i coor)
        {
            indices[iStart + 0] = To1D(coor);
            indices[iStart + 1] = To1D(coor.PlusX());
            indices[iStart + 2] = To1D(coor.PlusY());
            indices[iStart + 3] = To1D(coor.PlusX());
            indices[iStart + 4] = To1D(coor.PlusXY());
            indices[iStart + 5] = To1D(coor.PlusY());
        }

        private short To1D(Vector2i coor)
        {
            return (short)(coor.Y * 4 + coor.X);
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

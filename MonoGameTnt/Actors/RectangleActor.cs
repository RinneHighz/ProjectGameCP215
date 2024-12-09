using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ThanaNita.MonoGameTnt
{
    public class RectangleActor : Actor
    {
        private VertexArray array; // struct
        public RectangleActor(Color color, Vector2 size)
                : this(color, Vector2.Zero, size)
        {
        }
        public RectangleActor(Color color, Vector2 position, Vector2 size)
                : base()
        {
            this.Color = color;
            this.Position = position;
            RawSize = size;
            CreateArray(size);
        }
        public RectangleActor(Color color, RectF rect)
                : this(color, rect.Position, rect.Size)
        {
        }
        private void CreateArray(Vector2 size)
        {
            array.texture = null;
            array.vertices = new VertexPositionColorTexture[4];
            InitIndices();
            InitVertices(size);
        }

        private void InitIndices()
        {
            array.indices = new short[6];
            array.indices[0] = 0;
            array.indices[1] = 1;
            array.indices[2] = 2;
            array.indices[3] = 1;
            array.indices[4] = 3;
            array.indices[5] = 2;
        }
        private void InitVertices(Vector2 size)
        {
            float x = 0;
            float y = 0;
            float xMax = size.X;
            float yMax = size.Y;
            array.vertices[0].Position = new Vector3(x, y, 0);
            array.vertices[1].Position = new Vector3(xMax, y, 0);
            array.vertices[2].Position = new Vector3(x, yMax, 0);
            array.vertices[3].Position = new Vector3(xMax, yMax, 0);

            array.vertices[0].Color = Color.White;
            array.vertices[1].Color = Color.White;
            array.vertices[2].Color = Color.White;
            array.vertices[3].Color = Color.White;

            // don't need to init TextureCoordinate because the value already default at (0,0)
            /*            float l = 0;
                        float r = 1;
                        float t = 0;
                        float b = 1;
                        array.vertices[0].TextureCoordinate = new Vector2(l, t);
                        array.vertices[1].TextureCoordinate = new Vector2(r, t);
                        array.vertices[2].TextureCoordinate = new Vector2(l, b);
                        array.vertices[3].TextureCoordinate = new Vector2(r, b);*/
        }
        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            array.Draw(target, CombineState(state));
        }


        //------------------------ Static Factory Methods--------------------
        public static RectangleActor HorizontalLine(Color color, float y, float x0, float x1, float width)
        {
            float half = width / 2;
            return new RectangleActor(color, 
                new RectF(x0 - half, y - half, x1 - x0 + width, width));
        }
        public static RectangleActor VerticalLine(Color color, float x, float y0, float y1, float width)
        {
            float half = width / 2;
            return new RectangleActor(color,
                new RectF(x - half, y0 - half, width, y1 - y0 + width));
        }
    }
}

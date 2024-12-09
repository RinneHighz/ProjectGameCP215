using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public readonly struct Vector2i
    {
        public int X { get; }
        public int Y { get; }

        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2i operator+ (Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X + b.X, a.Y + b.Y);
        }
        public static Vector2i operator- (Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X - b.X, a.Y - b.Y);
        }
        public static Vector2i operator* (Vector2i a, int factor)
        {
            return new Vector2i(a.X * factor, a.Y * factor);
        }
        public static Vector2i operator* (int factor, Vector2i a)
        {
            return new Vector2i(a.X * factor, a.Y * factor);
        }

        public static implicit operator Vector2(Vector2i a)
        {
            return new Vector2(a.X, a.Y);
        }
        public static explicit operator Vector2i(Vector2 a)
        {
            return new Vector2i((int)a.X, (int)a.Y);
        }

        public Vector2i PlusX(int dx = 1)
        {
            return new Vector2i(X + dx, Y);
        }
        public Vector2i PlusY(int dy = 1)
        {
            return new Vector2i(X, Y + dy);
        }
        public Vector2i PlusXY(int dx = 1, int dy = 1)
        {
            return new Vector2i(X + dx, Y + dy);
        }
    }
}

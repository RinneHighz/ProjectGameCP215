using Microsoft.Xna.Framework;
using MonoGame.Extended;
using ThanaNita.MonoGameTnt;

namespace Game09
{
    public class Game09 : Game2D
    {
        protected override void LoadContent()
        {
            ClearColor = Color.Ivory;
            BackgroundColor = null;

            CollisionDetectionUnit.AddDetector(1, 2);

            All.Add(new Brick(new RectF(400, 600, 1000, 100)));
            All.Add(new Brick(new RectF(1400, 500, 150, 200)));
            All.Add(new Brick(new RectF(600, 420, 100, 50)));
            All.Add(new Brick(new RectF(800, 320, 100, 50)));

            var girl = new Girl(new Vector2(ScreenSize.X / 2, 50));
            All.Add(girl);
            girl.Add(new CameraMan(Camera, ScreenSize));
        }
    }
}

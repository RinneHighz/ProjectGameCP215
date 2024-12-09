
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using ThanaNita.MonoGameTnt;

namespace Game08
{
    public class Game08 : Game2D
    {
        protected override void LoadContent()
        {
            ClearColor = Color.Pink;
            BackgroundColor = null;

            All.Position = ScreenSize / 2;

            var texture = TextureCache.Get("Puppy.jpg");
            var puppy = new SpriteActor(texture);
            All.Add(puppy);
            puppy.Origin = new Vector2(200, 200);
            puppy.Scale = new Vector2(1, 0.75f);
            //puppy.Rotation = 20;
            Debug.WriteLine(puppy.GetMatrix());

            All.Add(new OriginAxis(500));
            All.Add(new CrossHair(400, 400));
            All.Add(new CrossHair(200, 200));

            DisplayCameraProperties();
        }

        private void DisplayCameraProperties()
        {
            Debug.WriteLine(ScreenSize);
            Debug.WriteLine(Camera.Origin);
            Debug.WriteLine(Camera.Position);
            Debug.WriteLine(Camera.Rotation); // Radian
            Debug.WriteLine(Camera.Zoom);
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Camera.Position += DirectionKey.Normalized * 500 * deltaTime;

            var mouseInfo = GlobalMouseInfo.Value;
            float zoom = mouseInfo.DeltaScroll / 1200.0f;
            Camera.Zoom += zoom;
        }

    }
}

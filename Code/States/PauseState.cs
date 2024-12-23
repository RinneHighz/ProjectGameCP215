using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class PauseState : Actor
    {
        public PauseState(Vector2 screenSize)
        {
            // เพิ่มพื้นหลังสำหรับ Pause State
            var background = new RectangleActor(Color.Black * 0.5f, new RectF(Vector2.Zero, screenSize));
            Add(background);

            // เพิ่มข้อความ Pause
            var pauseText = new Text("Content/Resource/Font/Roboto-Regular.ttf", 100, Color.White, "PAUSED");
            pauseText.Position = screenSize / 2 - new Vector2(pauseText.RawSize.X / 2, pauseText.RawSize.Y / 2);
            Add(pauseText);
        }
    }
}

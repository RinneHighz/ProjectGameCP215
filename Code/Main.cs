using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class Main : Game2D
    {
        CameraMan cameraMan;
        PlayState playState;
        MainMenuState mainMenuState;


        public Main()
            : base(startAsFullScreen: false)
        {
        }

        protected override void LoadContent()
        {
            BackgroundColor = Color.LightGray;


            CollisionDetectionUnit.AddDetector(1, 2);
            mainMenuState = new MainMenuState(ScreenSize, ExitNotifier);
            All.Add(mainMenuState);

        }

        protected override void AfterUpdateAndCollision()
        {
            if (cameraMan != null)
                cameraMan.AdjustCamera();  // อัปเดตการปรับมุมกล้อง
        }

        private void ExitNotifier(Actor actor, int code)
        {
            if (actor == null)
                return;

            if (actor == mainMenuState)
            {
                mainMenuState.Detach();
                mainMenuState = null;

                // สร้างและเพิ่ม PlayState
                cameraMan = new CameraMan(Camera, ScreenSize);
                cameraMan.FrameLimit = new RectF(ScreenSize).CreateExpand(new Vector2(-500, -400));
                playState = new PlayState(cameraMan, ScreenSize, ExitNotifier);
                All.Add(playState);
            }
            // ถ้าเป็น PlayState ให้กลับไปที่ MainMenuState
            else if (actor == playState)
            {
                playState.Detach();
                playState = null;
                mainMenuState = new MainMenuState(ScreenSize, ExitNotifier);
                All.Add(mainMenuState);
            }
        }

    }
}

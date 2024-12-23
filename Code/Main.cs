using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectGameCP215
{
    public class Main : Game2D
    {
        CameraMan cameraMan;
        PlayState playState;
        MainMenuState mainMenuState;
        GameOverState gameOverState;




        public Main()
            : base(startAsFullScreen: false)
        {

        }



        protected override void LoadContent()
        {
            BackgroundColor = Color.White;
            //background in game
        


            // pausePlaceholder.Enable = false; // เริ่มต้นปิด PauseState


            CollisionDetectionUnit.AddDetector(1, 2);
            // CollisionDetectionUnit.AddDetector(1, 3);
            // CollisionDetectionUnit.AddDetector(2, 3);
            mainMenuState = new MainMenuState(ScreenSize, ExitNotifier);
            var actor = mainMenuState;
            CrossHair crossHair = new CrossHair(ScreenSize / 2);

            All.Add(mainMenuState);
            All.Add(crossHair);

           
           




            base.LoadContent();
        }


        protected override void AfterUpdateAndCollision()
        {
            // if (cameraMan != null)
            //     cameraMan.AdjustCamera();  // อัปเดตการปรับมุมกล้อง
        }




        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private void ExitNotifier(Actor actor, int code)
        {
            if (actor == null)
                return;
            // จัดการ PlayState
            else if (actor == playState && code == 0)
            {
                playState.stopBGM();
                playState.Detach();
                playState = null;
                cameraMan = new CameraMan(Camera, new Vector2(0, 0));
                mainMenuState = new MainMenuState(ScreenSize, ExitNotifier);
                mainMenuState.Add(cameraMan);
                All.Add(mainMenuState);
            }
            // จัดการ MainMenuState
            else if (actor == mainMenuState && code == 0)
            {
                mainMenuState.stopBGM();
                mainMenuState.Detach();
                mainMenuState = null;
                cameraMan = new CameraMan(Camera, ScreenSize);
                // cameraMan.FrameLimit = new RectF(ScreenSize).CreateExpand(new Vector2(-1920, -1080));
                playState = new PlayState(cameraMan, ScreenSize, ExitNotifier, All, CollisionDetectionUnit);
                All.Add(playState);
            }

            else if (actor == playState && code == 1)
            {

                cameraMan = new CameraMan(Camera, new Vector2(0, 0));
                int finalScore = playState.GetScore(); // ดึงคะแนนสุดท้าย
                playState.stopBGM();
                playState.Detach();
                playState = null;
                gameOverState = new GameOverState(ScreenSize, ExitNotifier, finalScore);
                gameOverState.Add(cameraMan);
                All.Add(gameOverState);
            }

            else if (actor == gameOverState && code == 0)
            {
                gameOverState.stopBGM();
                gameOverState.Detach();
                gameOverState = null;
                cameraMan = new CameraMan(Camera, new Vector2(0, 0));
                mainMenuState = new MainMenuState(ScreenSize, ExitNotifier);
                mainMenuState.Add(cameraMan);
                All.Add(mainMenuState);
            }

        }



    }
}

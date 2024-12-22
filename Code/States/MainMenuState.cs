
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;

namespace ProjectGameCP215
{
    public class MainMenuState : Actor
    {
        ExitNotifier exitNotifier;
        Song backgroundMusic;
        public MainMenuState(Vector2 screenSize, ExitNotifier exitNotifier)
        {
            // Origin = screenSize / 2;
            // Scale = new Vector2(1, 1);
            Position = new Vector2(0, 0);
            this.exitNotifier = exitNotifier;

            backgroundMusic = Song.FromUri("Song01",
                      new Uri("Content/Resource/Sound/MainMenuBGM.ogg", UriKind.Relative));
            // MediaPlayer.Play(backgroundMusic);
            // MediaPlayer.Volume = 4;


            //ImageButton 
            var start_button = new TextureRegion(TextureCache.Get("Start_ImageButton.png"),new RectF(0,0,300,100));
            var imgbutton = new ImageButton(start_button);
            imgbutton.Position = new Vector2(210,500);
            imgbutton.ButtonClicked += Button1_ButtonClicked;
            Add(imgbutton);
        }

        private void Button1_ButtonClicked(GenericButton button)
        {
            exitNotifier.Invoke(this, 0);
        }

        // public void stopBGM(){
        //     MediaPlayer.Stop();
        // }
    }
}

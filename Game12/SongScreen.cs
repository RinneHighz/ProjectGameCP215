using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game12
{
    public class SongScreen : Actor
    {
        // ref: https://gamefromscratch.com/monogame-tutorial-audio/
        Song song;
        SoundEffect soundEffect;
        ExitNotifier exitNotifier;
        public SongScreen(Vector2 screenSize, ExitNotifier exitNotifier)
        {
            this.exitNotifier = exitNotifier;

            var panel = new Panel(screenSize, new Vector2(100, 100), Color.Pink, Color.White, 0);
            Add(panel);
            
            AddAction(Actions.FadeIn(1.0f, this));

            song = Song.FromUri("Song01",
                      new Uri("VARIATION-ON-A-THEME-OF-MAZART (trim).ogg", UriKind.Relative));
            //MediaPlayer.Play(song);

            soundEffect = SoundEffect.FromFile("bump.wav");
        }
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            var keyInfo = GlobalKeyboardInfo.Value;

            if (keyInfo.IsKeyPressed(Keys.Space))
            {
                if (MediaPlayer.State == MediaState.Stopped)
                    MediaPlayer.Play(song);
                else if (MediaPlayer.State == MediaState.Playing)
                    MediaPlayer.Pause();
                else if (MediaPlayer.State == MediaState.Paused)
                    MediaPlayer.Resume();
            }

            if(keyInfo.IsKeyPressed(Keys.Tab))
                soundEffect.Play();

            if (keyInfo.IsKeyPressed(Keys.End))
            {
                MediaPlayer.Stop();
                exitNotifier(this, 0);
            }
        }
    }
}


using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;
using System.Linq;
using Microsoft.Xna.Framework.Media;
using System;

namespace ProjectGameCP215
{
    public class GameOverState : Actor
    {
        ExitNotifier exitNotifier;
        Text gameOverText;
        Song backgroundMusic;
        Vector2 screenSize;

        public GameOverState(Vector2 screenSize, ExitNotifier exitNotifier, int finalScore)
        {
            Position = new Vector2(0, 0);
            this.exitNotifier = exitNotifier;
            this.screenSize = screenSize;

            var texture2 = TextureCache.Get("gameover_page.png");
            var backgroundimg = new SpriteActor(texture2);
            Add(backgroundimg);

            var StatPanel = new StatPanel();
            StatPanel.Position = screenSize / 2 - new Vector2(400, 100);
            Add(StatPanel);



            backgroundMusic = Song.FromUri("Song01",
                      new Uri("Content/Resource/Sound/DeathSound.ogg", UriKind.Relative));

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = 4;


            // บันทึกคะแนนหากไม่ใช่ 0
            if (finalScore > 0)
            {
                ScoreManager.SaveScore(finalScore);
            }

            // ดึงคะแนนสูงสุด 5 อันดับแรก
            var topScores = ScoreManager.GetAllScores()
                .OrderByDescending(score => score)
                .Take(5)
                .ToList();

            string topScoresText = string.Join(", ", topScores.Select(score => score.ToString()));

            gameOverText = new Text("Content/Resource/Font/PixelFont.ttf", 50, Color.Black,
                $"Game Over\nYour Score: {finalScore}\nTop 5 Scores: {topScoresText}")
            {
            };

            // var button1 = new Button("Content/Resource/Font/JacquesFrancoisShadow-Regular.ttf", 50,
            //     Color.Brown, "Menu", new Vector2(300, 100));
            // button1.Position = new Vector2(50, 500);
            // button1.ButtonClicked += Button1_ButtonClicked;

            // Add(button1);
            StatPanel.Add(gameOverText);

            //ImageButton 
            var menu_button = new TextureRegion(TextureCache.Get("Menu_ImageButton.png"), new RectF(0, 0, 300, 100));
            var imgbutton = new ImageButton(menu_button);
            imgbutton.Position = new Vector2(1920 / 2 - 300 / 2, 700);
            imgbutton.ButtonClicked += Button1_ButtonClicked;
            Add(imgbutton);
        }

        private void Button1_ButtonClicked(GenericButton button)
        {
            exitNotifier.Invoke(this, 0);
        }
    }
}

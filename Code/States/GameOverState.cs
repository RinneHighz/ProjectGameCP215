
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;
using System.Linq;

namespace ProjectGameCP215
{
    public class GameOverState : Actor
    {
        ExitNotifier exitNotifier;
        Text gameOverText;
        public GameOverState(Vector2 screenSize, ExitNotifier exitNotifier, int finalScore)
        {
            Position = new Vector2(0, 0);
            this.exitNotifier = exitNotifier;

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

            gameOverText = new Text("Content/Resource/Font/JacquesFrancoisShadow-Regular.ttf", 50, Color.Brown, 
                $"Game Over\nYour Score: {finalScore}\nTop 5 Scores: {topScoresText}")
            {
                Position = screenSize / 2 
            };

            var button1 = new Button("Content/Resource/Font/JacquesFrancoisShadow-Regular.ttf", 50,
                Color.Brown, "Menu", new Vector2(300, 100));
            button1.Position = new Vector2(50, 500);
            button1.ButtonClicked += Button1_ButtonClicked;

            Add(button1);
            Add(gameOverText);
        }

        private void Button1_ButtonClicked(GenericButton button)
        {
            exitNotifier.Invoke(this, 0);
        }
    }
}

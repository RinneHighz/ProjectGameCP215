
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class GameOverState : Actor
    {
        ExitNotifier exitNotifier;
        Text gameOverText;
        public GameOverState(Vector2 screenSize, ExitNotifier exitNotifier)
        {
            // Origin = screenSize / 2;
            // Scale = new Vector2(1, 1);
            Position = new Vector2(0, 0);
            this.exitNotifier = exitNotifier;

            gameOverText = new Text("Content/Resource/Font/JacquesFrancoisShadow-Regular.ttf", 50, Color.Brown, "Game Over")
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

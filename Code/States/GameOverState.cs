
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class GameOverState : Actor
    {
        ExitNotifier exitNotifier;
        public GameOverState(Vector2 screenSize, ExitNotifier exitNotifier)
        {
            // Origin = screenSize / 2;
            // Scale = new Vector2(1, 1);
            Position = new Vector2(0, 0);
            this.exitNotifier = exitNotifier;

            var button1 = new Button("Content/Resource/Font/JacquesFrancoisShadow-Regular.ttf", 50,
                Color.Brown, "Menu", new Vector2(300, 100));
            button1.Position = new Vector2(50, 500);
            button1.ButtonClicked += Button1_ButtonClicked;


            Add(button1);
        }

        private void Button1_ButtonClicked(GenericButton button)
        {
            exitNotifier.Invoke(this, 0);
        }
    }
}


using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class MainMenuState : Actor
    {
        ExitNotifier exitNotifier;
        public MainMenuState(Vector2 screenSize, ExitNotifier exitNotifier)
        {
            // Origin = screenSize / 2;
            // Scale = new Vector2(1, 1);
            Position = new Vector2(0, 0);
            this.exitNotifier = exitNotifier;

            // var button1 = new Button("Content/Resource/Font/JacquesFrancoisShadow-Regular.ttf", 50,
            //     Color.Brown, "Start", new Vector2(300, 100));
            // button1.Position = new Vector2(50, 500);
            // button1.ButtonClicked += Button1_ButtonClicked;
            // Add(button1);


            //ImageButton 
            var button2 = new TextureRegion(TextureCache.Get("imgbutton.png"),new RectF(0,0,300,100));
            var imgbutton = new ImageButton(button2);
            imgbutton.Position = new Vector2(50,500);
            imgbutton.ButtonClicked += Button1_ButtonClicked;
            Add(imgbutton);
        }

        private void Button1_ButtonClicked(GenericButton button)
        {
            exitNotifier.Invoke(this, 0);
        }
    }
}

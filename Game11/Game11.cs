using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ThanaNita.MonoGameTnt;

namespace Game11
{
    public class Game11 : Game2D
    {
        Text text;
        Placeholder placeholder = new Placeholder();
        protected override void LoadContent()
        {
            text = new Text("Trirong-Regular.ttf", 100, Color.Green, "UI: ทดสอบ ดูนี้นั้น โน้น น้ำ");
            All.Add(text);

            var panel = new Panel(new Vector2(1000, 800),
                Color.LightSalmon, Color.LightSteelBlue, 10);
            panel.Position = new Vector2(100, 100);
            placeholder.Add(panel);
            All.Add(placeholder);

            All.Add(new CrossHair(new Vector2(100, 100)));
            All.Add(new CrossHair(new Vector2(1100, 900)));

            // 4.
            var button1 = new Button("JacquesFrancoisShadow-Regular.ttf", 50,
                Color.Brown, "Button 1", new Vector2(300, 100));
            button1.Position = new Vector2(50, 500);
            panel.Add(button1);

            var button2 = new Button("Tangerine-Regular.ttf", 170,
                                    Color.Brown, "Button 2", new Vector2(300, 100));
            button2.Position = new Vector2(400, 500);
            button2.NormalColor = Color.Pink;
            button2.UpdateCurrentVisual();
            button2.ButtonClicked += Button2_ButtonClicked;
            panel.Add(button2);

            // 5.
            var region = new TextureRegion(TextureCache.Get("Items.jpg"),
                                            new RectF(0, 0, 300, 300));
            var imageButton = new ImageButton(region);
            imageButton.Position = new Vector2(50, 50);
            imageButton.PressedColor = Color.Pink;
            imageButton.OutlineWidth = 0;
            imageButton.SetButtonText("Tangerine-Regular.ttf", 70, Color.Blue,
                "Image Button", new Vector2(0, 100));
            panel.Add(imageButton);

            // 6.
            var statPanel = new StatPanel();
            statPanel.Position = new Vector2(400, 50);
            panel.Add(statPanel);

        }

        private void Button2_ButtonClicked(GenericButton button)
        {
            placeholder.Toggle();
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            var keyInfo = GlobalKeyboardInfo.Value;
            // Todo:
            if (keyInfo.IsKeyPressed(Keys.Enter))
                text.FontSize *= 1.05f;

            if (keyInfo.IsKeyPressed(Keys.Space))
                placeholder.Toggle();
        }
    }
}

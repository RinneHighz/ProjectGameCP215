using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class LevelUpState : Actor
    {
        public LevelUpState(Vector2 screenSize, MaleActor player, ExitNotifier exitNotifier, PlayState playState)
        {
            // Background
            var background = new RectangleActor(Color.Black * 0.5f, new RectF(Vector2.Zero, screenSize));
            Add(background);

            var texture1 = TextureCache.Get("Content/LevelUpBG.png");
            var backgroundimg = new SpriteActor(texture1);
            Add(backgroundimg);

            // Buttons
            var buttonIncreaseDamage = new Button("Content/Resource/Font/PixelFont.ttf", 50, Color.Black, "Increase Damage", new Vector2(500, 100));
            buttonIncreaseDamage.Position = screenSize / 2 - buttonIncreaseDamage.RawSize / 2;
            buttonIncreaseDamage.ButtonClicked += (btn) =>
            {
                player.damage += 5; // Increase Damage
                exitNotifier.Invoke(this, 0); // Exit LevelUpState
            };
            Add(buttonIncreaseDamage);

            var buttonIncreaseHp = new Button("Content/Resource/Font/PixelFont.ttf", 50, Color.Black, "Increase Max HP", new Vector2(500, 100));
            buttonIncreaseHp.Position = screenSize / 2 - buttonIncreaseHp.RawSize / 2 + new Vector2(0, 100);
            buttonIncreaseHp.ButtonClicked += (btn) =>
            {
                player.maxHp += 20; // Increase HP
                player.hp = player.maxHp; // Refill HP
                exitNotifier.Invoke(this, 0); // Exit LevelUpState
            };
            Add(buttonIncreaseHp);
        }
    }
}

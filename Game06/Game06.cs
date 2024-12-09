using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game06
{
    public class Game06 : Game2D
    {
        protected override void LoadContent()
        {
            //All.Add(new Aladdin(ScreenSize / 2));
            All.Add(new Girl(ScreenSize / 2));
            //TestAladdin();
            //TestGirl();
        }

/*        protected override void SetDefaultGraphicsStates()
        {
            base.SetDefaultGraphicsStates();
            //GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        }*/

        private void TestGirl()
        {
            var size = new Vector2(60, 60);
            var sprite = new SpriteActor();
            sprite.Position = ScreenSize / 2;
            sprite.Origin = size / 2;
            sprite.Scale = new Vector2(4, 4);
            All.Add(sprite);

            var texture = TextureCache.Get("Girl.png");
            var regions2d = RegionCutter.Cut(texture, size, countX: 8, countY: 4);
            var regions1d = RegionSelector.Select(regions2d, 16, 8);
            var animation = new Animation(sprite, 1.0f, regions1d);
            sprite.AddAction(animation);
        }

        private void TestAladdin()
        {
            var size = new Vector2(45, 45);
            var sprite = new SpriteActor();
            sprite.Position = ScreenSize / 2;
            sprite.Origin = size / 2;
            sprite.Scale = new Vector2(4, 4);
            All.Add(sprite);

            var texture = TextureCache.Get("Aladdin.png");
            var regions2d = RegionCutter.Cut(texture, size, countX: 4, countY: 4);
            var regions1d = RegionSelector.Select(regions2d, start: 5, count: 8);
            var animation = new Animation(sprite, 0.5f, regions1d);
            sprite.AddAction(animation);
        }
    }
}

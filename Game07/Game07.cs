using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game07
{
    public class Game07 : Game2D
    {
        public Game07()
        {
            BackgroundColor = Color.LightGray;
            IsFixedTimeStep = false;
        }
        protected override void LoadContent()
        {
            CollisionDetectionUnit.AddDetector(1, 2);

            for (int i = 0; i < 5000; ++i)
                All.Add(new Enemy(ScreenSize / 2));

            All.Add(new Player(All, ScreenSize));

            All.Add(new FPS());
        }
    }
}

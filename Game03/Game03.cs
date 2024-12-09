using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;

namespace Game03
{
    public class Game03 : Game2D
    {
        protected override void LoadContent()
        {
            for (int i = 0; i < 5; ++i)
                All.Add(new BigBall(ScreenSize / 2));

        }
    }
}

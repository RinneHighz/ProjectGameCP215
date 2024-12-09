using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework;

namespace Game03
{
    public class Game03b : Game2D
    {
        protected override void LoadContent()
        {
            var actor = BigBall.StillBall(ScreenSize / 2);

            All.Add(actor);

            actor.AddAction(
                Actions.Forever(
                    Actions.FadeOut(1, actor),
                    Actions.FadeIn(1, actor)
                    ));

            float duration = 0.2f;
/*            actor.AddAction(
                Actions.Forever(
                    new MoveByAction(duration, new Vector2(0, -300), actor),
                    new MoveByAction(duration, new Vector2(300, 0), actor),
                    new MoveByAction(duration, new Vector2(0, 300), actor),
                    new MoveByAction(duration, new Vector2(-300, 0), actor)
            ));*/

        }

        float time = 0;
        bool finished = false;
        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //AddOneTime(deltaTime);

        }

        int childAdded = 0;
        private void AddOneTime(float deltaTime)
        {
            time += deltaTime;
            if (time >= 0.5f && !finished)
            {
                time -= 0.5f;
                All.Add(new Ball(ScreenSize / 2));
                childAdded++;

                if(childAdded >= 10)
                    finished = true;
            }
        }
    }
}

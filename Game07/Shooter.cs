using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using ThanaNita.MonoGameTnt;

namespace Game07
{
    public class Shooter : Action
    {
        Actor all;
        Player player;
        float coolDownTime = 0;
        float coolDownFix = 0.05f;
        public Shooter(Actor all, Player player)
        {
            this.all = all;
            this.player = player;
        }
        public bool Act(float deltaTime)
        {
            coolDownTime += deltaTime;

            var keyInfo = GlobalKeyboardInfo.Value;
            if (keyInfo.IsKeyDown(Keys.Space) 
                && 
                coolDownTime >= 0)
            {
                all.Add(new Bullet(player));
                coolDownTime = -coolDownFix; // -0.10f;
            }
            return false;
        }

        public bool IsFinished()
        {
            return false;
        }

        public void Restart()
        {
            
        }
    }
}

using ThanaNita.MonoGameTnt;

namespace Game04
{
    public class Game04 : Game2D
    {
        Actor actor;
        protected override void LoadContent()
        {
            actor = new Puppy(ScreenSize / 2, ScreenSize);
            All.Add(actor);
            All.Add(new Puppy(ScreenSize / 4, ScreenSize, "Ball.png"));
            All.Add(new Ball(ScreenSize / 2));
            //All.Add(new InputTester());
        }
    }
}

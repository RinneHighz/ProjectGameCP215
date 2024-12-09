
using ThanaNita.MonoGameTnt;

namespace Game12
{
    public class Game12 : Game2D
    {
        Actor dragScreen;
        Actor songScreen;
        protected override void LoadContent()
        {
            dragScreen = new DragScreen(ScreenSize, ExitNotifier);
            All.Add(dragScreen);
        }

        private void ExitNotifier(Actor actor, int code)
        {
            if (actor == null)
                return;

            if (actor == dragScreen)
            {
                dragScreen.Detach();
                dragScreen = null;
                songScreen = new SongScreen(ScreenSize, ExitNotifier);
                All.Add(songScreen);
            }
            else if(actor == songScreen)
            {
                songScreen.Detach(); 
                songScreen = null;
                dragScreen = new DragScreen(ScreenSize, ExitNotifier);
                All.Add(dragScreen);
            }
        }
    }
}

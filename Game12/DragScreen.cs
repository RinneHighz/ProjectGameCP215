
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game12
{
    public class DragScreen : Actor
    {
        ExitNotifier exitNotifier;
        Actor innerPanel;
        public DragScreen(Vector2 screenSize, ExitNotifier exitNotifier)
        {
            this.exitNotifier = exitNotifier;

            Vector2 offset = new Vector2(100, 100);
            var panel1 = new Panel(screenSize - 2 * offset, Color.DarkGreen, Color.Black, 10);
            panel1.Position = offset;
            Add(panel1);

            var panel2 = new Panel(panel1.RawSize, offset, Color.DarkBlue, Color.DarkGray, 5);
            panel2.Scale = new Vector2(0.5f, 0.5f);
            panel1.Add(panel2);

            var panel3 = new CropActor(panel2.RawSize);
            panel2.Add(panel3);

            RandomRects(panel3);

            innerPanel = panel3;
        }

        private static void RandomRects(Actor panel)
        {
            for (int i = 0; i < 20; ++i)
            {
                var rect = new RectF(RandomUtil.Position(panel.RawSize),
                                     new Vector2(200, 150));
                var rectActor = new RectangleActor(RandomUtil.Color(), rect);
                rectActor.Scale = new Vector2(0.5f, 0.5f);
                panel.Add(rectActor);
            }
        }

        private Actor selection = null;
        private Vector2 click; // position
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            var mouseInfo = GlobalMouseInfo.Value;
            var keyInfo = GlobalKeyboardInfo.Value;

            if (mouseInfo.IsLeftButtonPressed())    // Mouse Pressed
            {
                var world = mouseInfo.WorldPosition;
                for(int i = innerPanel.ChildCount-1; i >= 0; --i)
                {
                    var actor = innerPanel.GetChild(i);
                    var local = actor.GlobalTransform.GetInverse().Transform(world);
                    if(actor.RawRect.Contains(local))
                    {
                        selection = actor;
                        click = actor.GetMatrix().Transform(local);
                        break;
                    }
                }
            }

            if (mouseInfo.IsLeftButtonReleased())   // Mouse Released
                selection = null;

                    // Mouse Moved
            if (mouseInfo.DeltaWorldPosition != Vector2.Zero && selection != null)
            {
                var position = mouseInfo.WorldPosition;
                var newClick = selection.Parent.GlobalTransform.GetInverse().Transform(position);
                selection.Position += newClick - click;
                click = newClick;
            }

            if (keyInfo.IsKeyPressed(Keys.End))
                AddAction(new SequenceAction(
                                Actions.FadeOut(0.5f, this),
                                new RunAction(() => exitNotifier(this, 0))
                    ));
        }
    }
}

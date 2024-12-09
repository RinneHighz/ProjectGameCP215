
using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class Panel : Actor
    {
        RectangleActor background;
        HollowRectActor frame;

        public Panel(Vector2 size, Color backgroundColor, 
                     Color outlineColor, float outlineWidth = 2)
        {
            RawSize = size;
            background = new RectangleActor(backgroundColor, RawRect);
            frame = new HollowRectActor(outlineColor, outlineWidth, 
                                        RawRect.CreateExpand(-outlineWidth/2));
        }

        public Panel(Vector2 parentSize, Vector2 offset, Color backgroundColor,
                     Color outlineColor, float outlineWidth = 2)
            : this(parentSize - 2*offset, backgroundColor, outlineColor, outlineWidth)
        {
            Position = offset;
        }

        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            var combine = CombineState(state);
            background.Draw(target, combine);
            frame.Draw(target, combine);
        }
    }
}

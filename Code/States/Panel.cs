using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class Panel : Actor
    {
        RectangleActor background;
        HollowRectActor frame;

        RectF rawRect;

        public Panel (Vector2 size,Color backgroundColor,Color outlineColor,float outlineWidth=5)
        {
            rawRect = new RectF(Vector2.Zero,size);
            background = new RectangleActor(backgroundColor,rawRect);
            frame = new HollowRectActor(outlineColor,outlineWidth,rawRect.CreateExpand(-outlineWidth/2));
        }

        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);
            var combine = CombineState(state);
            background.Draw(target,combine);
            frame.Draw(target,combine); 
        }


    }
}
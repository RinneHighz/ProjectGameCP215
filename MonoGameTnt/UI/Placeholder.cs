
using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public class Placeholder : Actor
    {
        public Placeholder()
        {
        }
        public Placeholder(Actor child)
        {
            Add(child);
        }
        public bool Enable { get; set; } = true;
        public void Toggle() 
        { 
            Enable = !Enable; 
        }
        public override void Act(float deltaTime)
        {
            if(Enable)
                base.Act(deltaTime);
        }
        public override void Draw(DrawTarget target, DrawState state)
        {
            if (Enable)
                base.Draw(target, state);
        }
        public override Vector2 RawSize
        {
            get
            {
                if (ChildCount > 0)
                    return Children[0].RawSize;
                return base.RawSize;
            }
        }
    }
}

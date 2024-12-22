using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace ThanaNita.MonoGameTnt
{
    public class CameraBackground : Actor
    {
        private OrthographicCamera camera;
        private RectangleActor background;
        private Vector2 screenSize;

        public CameraBackground(OrthographicCamera camera, Vector2 screenSize, Color backgroundColor)
        {
            this.camera = camera;
            this.screenSize = screenSize;
            
            // Create background rectangle
            background = new RectangleActor(backgroundColor, 
                new RectF(-screenSize.X/2, -screenSize.Y/2, screenSize.X, screenSize.Y));
            Add(background);
        }

        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            Position = camera.Position + screenSize/2;
        }
    }
}
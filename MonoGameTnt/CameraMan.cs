using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace ThanaNita.MonoGameTnt
{
    public class CameraMan : Actor
    {
        OrthographicCamera camera;
        Vector2 screenSize;
        Actor followed = null;
        public RectF FrameLimit { get; set; }
        public CameraMan(OrthographicCamera camera, Vector2 screenSize)
        {
            this.camera = camera;
            this.screenSize = screenSize;
            FrameLimit = new RectF(screenSize / 2, Vector2.Zero);
        }
        public override void Act(float deltaTime)
        {
            base.Act(deltaTime);
            AdjustCamera();
        }
        public void AdjustCamera()
        {
            var followedActor = GetFollowedActor();
            var playerGlobal = followedActor.GlobalTransform.Transform(followedActor.RawSize / 2);
//            camera.Position = playerGlobal - screenSize / 2;
            float cameraX = camera.Position.X;
            float cameraY = camera.Position.Y;

            if (playerGlobal.X - cameraX < FrameLimit.X)
                cameraX = playerGlobal.X - FrameLimit.X;
            else if (playerGlobal.X - cameraX > FrameLimit.XMax)
                cameraX = playerGlobal.X - FrameLimit.XMax;

            if (playerGlobal.Y - cameraY < FrameLimit.Y)
                cameraY = playerGlobal.Y - FrameLimit.Y;
            else if (playerGlobal.Y - cameraY > FrameLimit.YMax)
                cameraY = playerGlobal.Y - FrameLimit.YMax;

            camera.Position = new Vector2(MathF.Round(cameraX, 0), MathF.Round(cameraY, 0));
        }

        private Actor GetFollowedActor()
        {
            return followed != null ? followed : Parent;
        }

        public void SetFollowedActor(Actor actor)
        {
            followed = actor;
        }
    }
}

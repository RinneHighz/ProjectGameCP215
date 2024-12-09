using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ThanaNita.MonoGameTnt;

namespace Game09
{
/*    public class Game09b : Game2D
    {
        protected override void LoadContent()
        {
            All.Add(new SimpleActor());
        }
    }

    public class SimpleActor : Actor
    {
        protected override void DrawSelf(DrawTarget target, DrawState state)
        {
            base.DrawSelf(target, state);

            var texture = TextureCache.Get("Puppy.jpg");
            var vertices = new VertexPositionColorTexture[3];
            vertices[0].Position = new Vector3(200, 0, 0);
            vertices[1].Position = new Vector3(0, 400, 0);
            vertices[2].Position = new Vector3(400, 400, 0);

            vertices[0].Color = Color.Red;
            vertices[1].Color = Color.Green;
            vertices[2].Color = Color.Blue;

            vertices[0].TextureCoordinate = new Vector2(0.5f, 0);
            vertices[1].TextureCoordinate = new Vector2(0, 1);
            vertices[2].TextureCoordinate = new Vector2(1, 1);


            target.Draw(vertices, [0, 1, 2], null);
        }
    }*/
}

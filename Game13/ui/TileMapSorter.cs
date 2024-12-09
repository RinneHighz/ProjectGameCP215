using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThanaNita.MonoGameTnt
{
    public class TileMapSorter : Actor
    {
        TileMap tileMap;
        public TileMapSorter(TileMap tileMap = null)
        {
            this.tileMap = tileMap;
        }
        public override void Draw(DrawTarget target, DrawState state)
        {
            DrawSelf(target, state);

            var combine = CombineState(state);
            // draw children & each tiles; draw order sorted by Y axis
            var actors = GetAllTiles();
            if(Children != null)
                actors.AddRange(this.Children);

            actors = actors.OrderBy(o => o.Position.Y).ToList();

            for (int i = 0; i < actors.Count; i++)
                actors[i].Draw(target, combine);
        }

        private List<Actor> GetAllTiles()
        {
            List<Actor> tiles = new List<Actor>();
            if (tileMap == null)
                return tiles;

            var tileArray = tileMap.TileArray;
            for (int y = 0; y < tileArray.GetLength(0); ++y)
                for (int x = 0; x < tileArray.GetLength(1); ++x)
                {
                    if (tileArray[y, x] == -1)
                        continue;

                    tiles.Add(tileMap.TileActors[y, x]);
                }
            return tiles;
        }
    }
}

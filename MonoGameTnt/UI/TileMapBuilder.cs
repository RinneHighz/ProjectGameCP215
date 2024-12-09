using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ThanaNita.MonoGameTnt
{
    public class TileMapBuilder
    {
        Vector2 tileSize;
        TextureRegion[] tileSet;
        int[,] tileArray;
        TileMap tileMap;
        public TileMap TileMap => tileMap;

        public TileMap CreateSimple(string imageName, Vector2 tileSize, int countX, int countY, string csvName)
        {
            PrepareTileSet(imageName, tileSize, countX, countY);
            LoadTileArrayCSV(csvName);
            return CreateTileMap();
        }

        public TileMap CreateTileMap(TileMap.CreateTileDelegate createTile = null)
        {
            createTile = createTile ?? CreateTile;
            tileMap = new TileMap(tileSize, tileArray, createTile);
            return tileMap;
        }

        public void LoadTileArrayCSV(string filename)
        {
            var lines = File.ReadAllLines(filename);
            Debug.Assert(lines.Length > 0);
            int row = lines.Length;

            var firstLine = lines[0].Split(',');
            int col = firstLine.Length;
            int[,] arr = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                var line = lines[i].Split(',');
                for(int j = 0; j < col; ++j)
                {
                    arr[i, j] = int.Parse(line[j]);
                }
            }
            tileArray = arr;
        }

        public void PrepareTileSet(string imageName, Vector2 tileSize, int countX, int countY)
        {
            this.tileSize = tileSize;
            var texture = TextureCache.Get(imageName);
            var tiles2d = RegionCutter.Cut(texture, tileSize, countX, countY);
            tileSet = RegionSelector.SelectAll(tiles2d);
        }
        private Actor CreateTile(int tileCode)
        {
            if (tileCode == -1)
                return new SpriteActor();

            var sprite = new SpriteActor(tileSet[tileCode]);
            sprite.Origin = sprite.RawSize / 2;
            return sprite;
        }

        // usually for side-scrolling game
        public static void AddCollisions(TileMap tileMap1, int[] prohibitTiles, int collisionGroup)
        {
            var tileArray = tileMap1.TileArray;
            for (int y = 0; y < tileArray.GetLength(0); ++y)
                for (int x = 0; x < tileArray.GetLength(1); ++x)
                {
                    if (!prohibitTiles.Contains(tileArray[y, x]))
                        continue;

                    var tile = tileMap1.TileActors[y, x];
                    var collisionObj = CollisionObj.CreateWithRect(tile, collisionGroup);
                    //collisionObj.DebugDraw = true;
                    tile.Add(collisionObj);
                }
        }

    }
}

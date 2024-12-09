using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using ThanaNita.MonoGameTnt;

namespace ProjectGameCP215
{
    public class PlayState : Actor
    {

        ExitNotifier exitNotifier;

        public PlayState(CameraMan cameraMan, Vector2 screenSize, ExitNotifier exitNotifier)
        {
            this.exitNotifier = exitNotifier;


            var builder = new TileMapBuilder();
            // 1. tileMap1
            var tileMap1 = builder.CreateSimple("Content/Resource/TileMap/TileSet.png", new Vector2(32, 32), 14, 25,
                                                "Content/Resource/TileMap/TileMap1.csv");

            // 2. tree
            var tree = CreateTree();
            tree.Position = tileMap1.TileCenter(13, 11);

            int[] prohibitTiles = { 104, 105, 118, 119 };

            // 4. girl
            var girl = new Girl(tileMap1.TileCenter(10, 0));
            TileMapBuilder.AddCollisions(tileMap1, prohibitTiles, 2);

            // 5. cameraMan
            girl.Add(cameraMan);

            var visual = new Actor() { Position = new Vector2(100, 100) };
            visual.Scale = new Vector2(2, 2);
            visual.Add(tileMap1);

            var sorter = new TileMapSorter();
            sorter.Add(girl);
            sorter.Add(tree);
            visual.Add(sorter);

            Add(visual);
        }



        private Actor CreateTree()
        {
            var treeRegion = new TextureRegion(TextureCache.Get("Content/Resource/TileMap/TileSet.png"),
                                                new RectF(0, 32 * 15, 32, 32 * 3));
            var tree = new SpriteActor(treeRegion);
            tree.Origin = new Vector2(32 / 2, tree.RawSize.Y - 32 / 2);
            return tree;
        }
    }

}
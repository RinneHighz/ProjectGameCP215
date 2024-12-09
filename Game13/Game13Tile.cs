
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

namespace Game13
{
    public class Game13Tile : Game2D
    {
        CameraMan cameraMan;


        public Game13Tile()
            : base(startAsFullScreen: true)
        {
            
        }

        protected override void LoadContent()
        {
            BackgroundColor = Color.LightGray;

            var builder = new TileMapBuilder();
            // 1. tileMap1
            var tileMap1 = builder.CreateSimple("TileSet.png", new Vector2(32, 32), 14, 25,
                                                "TileMap1.csv");

            // 2. tree
            var tree = CreateTree();
            tree.Position = tileMap1.TileCenter(13, 11);

            // 3. guy
            var guy = new Guy(tileMap1);
            int[] prohibitTiles = [104, 105, 118, 119];
            guy.ProhibitTiles = prohibitTiles;
            guy.Position = tileMap1.TileCenter(0, 0);

            // 4. girl
            var girl = new Girl(tileMap1.TileCenter(10, 0));
            TileMapBuilder.AddCollisions(tileMap1, prohibitTiles, 2);
            CollisionDetectionUnit.AddDetector(1, 2);

            // 5. cameraMan
            cameraMan = new CameraMan(Camera, ScreenSize);
            cameraMan.FrameLimit = new RectF(ScreenSize).CreateExpand(new Vector2(-500, -400));
            girl.Add(cameraMan); // girl

            var visual = new Actor() { Position = new Vector2(100, 100) };
            visual.Scale = new Vector2(2, 2);
            visual.Add(tileMap1);

            var sorter = new TileMapSorter();
            //sorter.Add(guy);
            sorter.Add(girl);
            sorter.Add(tree);
            visual.Add(sorter);

            All.Add(visual);
        }
        protected override void AfterUpdateAndCollision()
        {
            if (cameraMan != null)
                cameraMan.AdjustCamera();
        }
        private Actor CreateTree()
        {
            var treeRegion = new TextureRegion( TextureCache.Get("TileSet.png"), 
                                                new RectF(0, 32 * 15, 32, 32 * 3));
            var tree = new SpriteActor(treeRegion);
            tree.Origin = new Vector2(32 / 2, tree.RawSize.Y - 32 / 2);
            return tree;
        }
    }
}

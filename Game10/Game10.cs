using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using ThanaNita.MonoGameTnt;

namespace Game10
{
    public class Game10 : Game2D
    {
        TextureRegion[] tiles;
        TileMap tileMap;
        Player player;
        protected override void LoadContent()
        {
            var tileSize = new Vector2(128, 128);
            BackgroundColor = Color.White;
            player = new Player() { Position = tileSize / 2 };

            PrepareTileSet();
            var tileArray = new int [3, 4] {
                {2,2,3,2 },
                {3,1,1,3 },
                {2,2,1,2 }
            };
            tileMap = new TileMap(tileSize, tileArray, CreateTile);

            var visual = new Actor() { Position = new Vector2(200, 200) };
            visual.Add(tileMap);
            visual.Add(player);
            All.Add(visual);
        }

        private void PrepareTileSet()
        {
            var texture = TextureCache.Get("TileSet.png");
            var tiles2d = RegionCutter.Cut(texture, new Vector2(32, 32), countX: 14, countY: 25);
            tiles = RegionSelector.SelectAll(tiles2d);
        }

        private Actor CreateTile(int tileCode)
        {
            var sprite = new SpriteActor(tiles[tileCode]);
            sprite.Origin = sprite.RawSize / 2;
            sprite.Scale = new Vector2(4, 4);
            return sprite;
        }
        
        KeyQueue keyQueue = new KeyQueue();
        LinearMotion motion = LinearMotion.Empty();

        protected override void Update(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            keyQueue.EnqueueAll(keyInfo.GetPressedKeys());

            motion.Act(deltaTime);
            SmoothMovement();

            //StepJumpMovement();
        }

        private void SmoothMovement()
        {
            if (!motion.IsFinished())
            {
                var command1 = keyQueue.PeekCommand();
                if (command1.IsOpposite(motion.Direction))
                    UnstableMoveOpposite(keyQueue.GetCommand().Direction);

                return;
            }

            var command = keyQueue.GetCommand();
            var direction = Vector2.Zero; // new Vector2();
            if (command.HasCommand())
                direction = command.Direction;
/*            else
            {
                direction = DirectionKey.Direction4;  // 2.B key down ค้างไว้ได้

                if(direction == Vector2.Zero)           // 2.C เคลื่อนที่ตามทิศเดิม จนกว่าจะชน
                    direction = motion.Direction;
            }*/

            StableMove(direction);
        }

        private void UnstableMoveOpposite(Vector2 direction)
        {
            CreateMotion(motion.TargetPosition, direction);
        }

        private void StableMove(Vector2 direction)
        {
            if(!IsAllowMove(direction))
                direction = Vector2.Zero;

            if (motion.Direction == Vector2.Zero && direction == Vector2.Zero)
                return;

            CreateMotion(player.Position, direction);
        }

        private void CreateMotion(Vector2 oldPosition, Vector2 direction)
        {
            var targetPosition = tileMap.TileCenter(oldPosition, direction);
            motion = new LinearMotion(player, speed: 300, targetPosition, direction);
        }

        private void StepJumpMovement()
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            if (!keyInfo.IsAnyKeyPressed())
                return;

            var key = keyInfo.GetPressedKeys()[0];
            var direction = DirectionKey.DirectionOf(key);
            if (!IsAllowMove(direction))
                return;

            player.Position += direction * tileMap.TileSize;
        }

        private bool IsAllowMove(Vector2 direction)
        {
            Vector2i index = tileMap.CalcIndex(player.Position, direction);
            return tileMap.IsInside(index) && IsAllowTile(index);
        }

        private bool IsAllowTile(Vector2i index)
        {
            var tileCode = tileMap.GetTileCode(index);
            var notAllowedTiles = new int[] {2};
            return !notAllowedTiles.Contains(tileCode); //tileCode != 2;
        }
    }
}
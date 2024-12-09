using Game10;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using System.Linq;
using ThanaNita.MonoGameTnt;

namespace Game13
{
    public class Guy : SpriteActor
    {
        public Guy(TileMap tileMap)
        {
            var texture = TextureCache.Get("Guy.png");
            SetTextureRegion(new TextureRegion(texture, new RectF(0, 0, 32, 48)));
            Origin = new Vector2(16, 40);
            player = this;
            this.tileMap = tileMap;
        }

        KeyQueue keyQueue = new KeyQueue();
        LinearMotion motion = LinearMotion.Empty();
        Actor player;
        TileMap tileMap;
        public int[] ProhibitTiles { get; set; }

        public override void Act(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            keyQueue.EnqueueAll(keyInfo.GetPressedKeys());

            motion.Act(deltaTime);
            SmoothMovement();

            //StepJumpMovement();
            base.Act(deltaTime);
        }
        private void SmoothMovement()
        {
            if (!motion.IsFinished()) // ถ้าไม่มีบรรทัดนี้ จะกดคีย์ใหม่ก่อนเคลื่อนที่เสร็จได้ 
            {
                var command1 = keyQueue.PeekCommand();
                if (command1.IsOpposite(motion.Direction))
                    UnstableMoveOpposite(keyQueue.GetCommand().Direction);
                return;
            }

            var command = keyQueue.GetCommand();
            Vector2 direction = Vector2.Zero;
            if (command.HasCommand())
                direction = command.Direction;
            else
            {
                direction = DirectionKey.Direction4;
            //    if (DirectionKey.Direction4 == Vector2.Zero)
            //        direction = motion.Direction;
            }

            StableMove(direction);
        }

        private void StableMove(Vector2 direction)
        {
            if (!IsAllowMove(direction))
                direction = new Vector2(0, 0);

            if (motion.Direction == Vector2.Zero && direction == Vector2.Zero)
                return; // ถ้าหยุดอยู่แล้ว ไม่ต้องหยุดซ้ำ

            if (direction != motion.Direction)
                motion.ToPreciseTarget(); // ปรับตำแหน่งให้พอดีศูนย์กลาง (กรณี FPS ต่ำๆ)

            CreateMotion(player.Position, direction);
        }

        private void UnstableMoveOpposite(Vector2 direction)
        {
            CreateMotion(motion.TargetPosition, direction);
        }
        private void CreateMotion(Vector2 oldPosition, Vector2 direction)
        {
            var targetPosition = tileMap.TileCenter(oldPosition, direction);
            motion = new LinearMotion(player, speed: 150, targetPosition, direction);
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
            if (ProhibitTiles == null)
                return true;

            int tileCode = tileMap.GetTileCode(index);
            return !ProhibitTiles.Contains(tileCode);
        }
    }
}

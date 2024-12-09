using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ThanaNita.MonoGameTnt;

namespace Game04
{
    public class Puppy : SpriteActor
    {
        Vector2 screenSize;
        public Puppy(Vector2 position, Vector2 screenSize, string fileName = "Puppy.jpg")
        {
            var texture = TextureCache.Get(fileName);
            SetTexture(texture);
            Origin = RawSize / 2;
            Position = position;
            this.screenSize = screenSize;
        }

        public override void Act(float deltaTime)
        {
            var keyInfo = GlobalKeyboardInfo.Value;
            var mouseInfo = GlobalMouseInfo.Value;

            // 1. การเปลี่ยนตำแหน่ง a) มีแบบปรับ position โดยตรง    b)กับแบบให้ mover ปรับให้
            float speed = 1500; // pixel/second
            Position += new Vector2(0, DirectionKey.Direction.Y) * speed * deltaTime;
            Rotation += DirectionKey.Direction.X * 360 * deltaTime;

            // 2. เช็คการกดปุ่ม Tab แล้วกระพริบ
            if (keyInfo.IsKeyPressed(Keys.Tab))
                Blink();


            // 3. เช็คการกดเมาส์ปุ่มซ้าย แล้วเปลี่ยนตำแหน่งตามจุดที่คลิก
            if (mouseInfo.IsLeftButtonPressed())
                GotoPosition(mouseInfo.WorldPosition);

            // 4. หากเคลื่อนที่ข้ามเส้น X=300 ให้ค่อยๆ เปลี่ยนสี
            CheckPositionThenChangeColor();

            // 5. หากกด R ให้ปรับค่าสีกลับทันที
            if (keyInfo.IsKeyPressed(Keys.R))
                Color = Color.White;

            // 6. หากกด Space ให้เคลื่อนที่เป็นรูปสี่เหลี่ยม (ด้วย MoveByAction)
            if (keyInfo.IsKeyPressed(Keys.Space))
                MoveAround();

            //------------------โจทย์-------------------
            // 2.
            if (keyInfo.IsKeyPressed(Keys.F1))
                Color = RandomUtil.Color();

            base.Act(deltaTime);
        }

        // Action 2
        private void Blink()
        {
            AddAction(Actions.Repeat(2,
                    Actions.FadeOut(0.5f, this),
                    Actions.FadeIn(0.5f, this)));
        }
        // Action 3
        private void GotoPosition(Vector2 position)
        {
            AddAction(new MoveToAction(1.5f, position, this));
        }
        // Action 4
        Vector2 oldPosition;
        private void CheckPositionThenChangeColor()
        {
            if (Position.X < 300 && oldPosition.X >= 300)
                ChangeColor(RandomUtil.Color());
            if (Position.X >= 300 && oldPosition.X < 300)
                ChangeColor(Color.White);

                oldPosition = Position;
        }
        private void ChangeColor(Color color)
        {
            AddAction(new ColorAction(1f, color, this));
        }
        // Action 6
        private void MoveAround()
        {
            AddAction(new RepeatAction(2, new SequenceAction(
                new MoveByAction(0.25f, new Vector2(0, 250), this),
                new MoveByAction(0.25f, new Vector2(250, 0), this),
                new MoveByAction(0.25f, new Vector2(0, -250), this),
                new MoveByAction(0.25f, new Vector2(-250, 0), this)
                )));
        }
    }
}

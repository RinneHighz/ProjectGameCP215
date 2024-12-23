using System;
using Microsoft.Xna.Framework;
using ThanaNita.MonoGameTnt;

public class Boss : SpriteActor
{

    Animation[] animationArray;
    Vector2 V;
    private float timePassed;
    private float duration;
    public int hp;

    public Boss(Vector2 position)
    {
        var size = new Vector2(48, 96);
        Position = position;
        Origin = size / 2;
        Scale = new Vector2(2, 2);


        hp = 100;

        var texture = TextureCache.Get("Content/Resource/SpriteSheet/Boss.png");
        var regions2d = RegionCutter.Cut(texture, size);
        var selector = new RegionSelector(regions2d);
        var stay1 = new Animation(this, 1.0f, selector.Select1by1(6, 7, 8));

        animationArray = [stay1];
        AddAction(stay1);

        var collisionObj = CollisionObj.CreateWithRect(this, 2);
        collisionObj.OnCollide = OnCollide;
        collisionObj.DebugDraw = true;
        Add(collisionObj);
    }

    // public override void Act(float deltaTime)
    // {
    //     base.Act(deltaTime);
    //     timePassed += deltaTime;
    //     if (timePassed >= duration)
    //     {
    //         timePassed = 0;
    //         duration = RandomUtil.NextSingle() * 4 + 0.5f; // สุ่มเวลา 0.5 ถึง 4.5 วินาที

    //         float speed = 100;
    //         float angle = RandomUtil.NextSingle() * 2 * MathF.PI;
    //         V = speed * new Vector2(MathF.Cos(angle), MathF.Sin(angle));
    //     }

    //     // คำนวณตำแหน่งใหม่
    //     Vector2 newPosition = Position + V * deltaTime;

    //     // ตรวจสอบขอบหน้าจอ
    //     var screenBounds = new Rectangle(0, 0, 1920, 1080); // แทนที่ 1920x1080 ด้วยขนาดหน้าจอของคุณ
    //     var bossBounds = new Rectangle(
    //         (int)(newPosition.X - Origin.X * Scale.X),
    //         (int)(newPosition.Y - Origin.Y * Scale.Y),
    //         (int)(RawSize.X * Scale.X),
    //         (int)(RawSize.Y * Scale.Y)
    //     );

    //     if (screenBounds.Contains(bossBounds))
    //     {
    //         // หากตำแหน่งใหม่อยู่ในขอบเขตหน้าจอ ให้ย้ายไปยังตำแหน่งใหม่
    //         Position = newPosition;
    //     }
    //     else
    //     {
    //         // หากตำแหน่งใหม่ออกนอกหน้าจอ ให้เปลี่ยนทิศทางของความเร็ว
    //         if (bossBounds.Left < screenBounds.Left || bossBounds.Right > screenBounds.Right)
    //         {
    //             V.X = -V.X; // กลับทิศทางในแนวนอน
    //         }
    //         if (bossBounds.Top < screenBounds.Top || bossBounds.Bottom > screenBounds.Bottom)
    //         {
    //             V.Y = -V.Y; // กลับทิศทางในแนวตั้ง
    //         }
    //     }
    // }


    public void OnCollide(CollisionObj objB, CollideData data)
    {
        var direction = data.objA.RelativeDirection(data.OverlapRect);

        if ((direction.Y > 0 && V.Y > 0) ||
            (direction.Y < 0 && V.Y < 0))
        {
            V.Y = 0;
            Position -= new Vector2(0, data.OverlapRect.Height * direction.Y);
        }
        if ((direction.X > 0 && V.X > 0) ||
            (direction.X < 0 && V.X < 0))
        {
            V.X = 0;
            Position -= new Vector2(data.OverlapRect.Width * direction.X, 0);
        }


    }

}
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
        // collisionObj.DebugDraw = true;
        Add(collisionObj);
    }


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
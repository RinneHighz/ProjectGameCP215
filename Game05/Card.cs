using System;
using ThanaNita.MonoGameTnt;

namespace Game05
{
    public class Card : SpriteActor
    {
        private static float width = 98.5f;
        private static float height = 153.2f;
        private int index; // 0..12

        private TextureRegion front;
        private TextureRegion back;

        public Card(int index, int suit) // Coding
        {
            this.index = index;
            var texture = TextureCache.Get("Cards.png");
            front = new TextureRegion(texture, GetCardRect(index, suit));
            back = new TextureRegion(texture, GetCardRect(2, 4));
            SetTextureRegion(front);
        }
        public void Show(bool bFront)
        {
            SetTextureRegion(bFront ? front : back);
        }
        public int GetScore()
        {
            if (index <= 9)
                return index + 1;
            return 10;
        }
        private static RectF GetCardRect(int index, int suit)
        {
            return new RectF(
                MathF.Round(index * width),
                MathF.Round(suit * height), 
                MathF.Round(width),
                MathF.Round(height)
                );
        }
    }
}

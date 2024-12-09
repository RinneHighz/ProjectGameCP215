using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework;

namespace Game05
{
    public class Hand : Actor
    {
        public Hand(Vector2 position)
        {
            Position = position;
        }
        public void AddCard(Card card)
        {
            card.Position = new Vector2(ChildCount * stepWidth, 0);
            Add(card);
        }
        public void ClearHand()
        {
            Clear();
        }
        private const int stepWidth = 100;
        public Vector2 NextPosition()
        {
            return this.Position + new Vector2(ChildCount * stepWidth, 0);
        }

        public bool IsBust() { return GetScore() > 21; }
        public int GetScore() // black jack score
        {
            int countAce = 0;
            int sum = 0;
            for (int i = 0; i < ChildCount; i++)
            {
                sum += GetCardScore(i);
                if (GetCardScore(i) == 1) // found Ace
                    countAce++;
            }

            while (countAce > 0)
            {
                if (sum + 10 > 21)
                    break;

                sum += 10;
                countAce--;
            }

            return sum;
        }
        private int GetCardScore(int index)
        {
            return ((Card)GetChild(index)).GetScore();
        }
    }
}

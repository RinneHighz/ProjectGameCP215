using System.Collections.Generic;
using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework;

namespace Game05
{
    public class Deck : Actor
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
            for (int i = 0; i < 13; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    var card = new Card(i, j);
                    cards.Add(card);
                    card.Show(false);
                }

            Random();
        }

        public override void Draw(DrawTarget target, DrawState state)
        {
            state = CombineState(state);
            int stepWidth = 23;
            for (int i = 0; i < cards.Count; ++i)
            {
                cards[i].Position = new Vector2(i * stepWidth, 0);
                cards[i].Draw(target, state);
            }
        }

        public void Random() // Coding
        {
            List<Card> newDecks = new List<Card>();
            int max = cards.Count;
            for (int i = 0; i < max; ++i)
            {
                int rand = RandomUtil.Next(cards.Count);
                newDecks.Add(cards[rand]);
                cards.RemoveAt(rand);
            }

            cards = newDecks;
        }

        public void Refill()
        {
            if (cards.Count > 10)
                return;

            var deck2 = new Deck();
            cards.AddRange(deck2.cards);
        }

        public Card GetTopCard()
        {
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}

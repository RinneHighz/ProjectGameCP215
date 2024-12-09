using ThanaNita.MonoGameTnt;
using System.Collections.Generic;

namespace Game05
{
    public class DealCardAction : CoroutineAction
    {
        private bool finished = false;
        private Actor all;
        private Deck deck;
        private Hand hand;
        public DealCardAction(Deck deck, Hand hand)
        {
            this.all = hand.Parent;
            this.deck = deck;
            this.hand = hand;
        }
        public override IEnumerator<Action> Coroutine()
        {
            // 1. เอา card ออกมาจาก Deck; เอามาใส่ใน parent (คือ all)
            deck.Refill();
            Card card = deck.GetTopCard();
            all.Add(card);
                // 2. ตั้ง Animation ให้ card เคลื่อนที่
            yield return new MoveToAction(duration: 0.2f, hand.NextPosition(), card);
            
                // 3. เอา card ออกจาก all; เอาไปใส่ใน hand แทน
            all.Remove(card);
            card.Show(true);
            hand.AddCard(card);
            finished = true;
        }
        public bool IsFinished() => finished;
    }
}

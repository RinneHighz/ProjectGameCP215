
using System.Reflection.Metadata.Ecma335;

namespace Game05
{
    public class ComputerPlay
    {
        Deck deck;
        Hand hand1;
        Hand hand2;
        DealCardAction dealCardAction;
        public ComputerPlay(Deck deck, Hand hand1, Hand hand2)
        {
            this.deck = deck;
            this.hand1 = hand1;
            this.hand2 = hand2;
        }
        public bool Act(float deltaTime)
        {
            if (hand2.IsBust())
                return true; // finished

            if (hand2.GetScore() < hand1.GetScore())
                return true;

            if (hand1.GetScore() >= 17)
                return true; // finished

            if (dealCardAction == null || dealCardAction.IsFinished())
            {
                dealCardAction = new DealCardAction(deck, hand1);
                hand1.AddAction(dealCardAction);
            }
            return false;
        }
    }
}

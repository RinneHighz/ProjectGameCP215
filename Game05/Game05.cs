using ThanaNita.MonoGameTnt;
using Microsoft.Xna.Framework;

namespace Game05
{
    public class Game05 : Game2D
    {
        private enum State { Setup, PlayerPlay, ComputerPlay, Score, Wait };
        private State state = State.Setup;
        Label label;
        Deck deck;
        Hand hand1;
        Hand hand2;
        ComputerPlay computerPlay;
        protected override void LoadContent()
        {
            All.Add(deck = new Deck());
            All.Add(hand1 = new Hand(new Vector2(0, 400)));
            All.Add(hand2 = new Hand(new Vector2(0, 600)));
            computerPlay = new ComputerPlay(deck, hand1 , hand2);

            label = new Label("consola.ttf", 150, Color.Orange, "");
            label.Position = ScreenSize / 2;
            All.Add(label);

            ChangeState(State.Setup);
        }

        protected override void Update(float deltaTime)
        {   // --- Coding Activity ---
            var mouseInfo = GlobalMouseInfo.Value;


            if (state == State.PlayerPlay)
            {
                if (mouseInfo.IsLeftButtonPressed())
                    deck.AddAction(DealCard(hand2));
                if (mouseInfo.IsRightButtonPressed() || hand2.IsBust())
                    ChangeState(State.ComputerPlay);
            }
            else if (state == State.ComputerPlay)
            {
                if (computerPlay.Act(deltaTime))
                    ChangeState(State.Score);
            }
            else if (state == State.Wait)
            {
                if (mouseInfo.IsAnyButtonPressed())
                    ChangeState(State.Setup);
            }
        }

        private void ChangeState(State newState)
        {
            state = newState;
            if(newState == State.Setup)
                Setup();
            else if (newState == State.Score)
                CheckScore();
        }

        private void Setup()
        {
            hand1.ClearHand();
            hand2.ClearHand();
            label.Text = "";
            All.AddAction(new SequenceAction(
                new DelayAction(1.0f),
                DealCard(hand1),
                DealCard(hand2),
                DealCard(hand1),
                DealCard(hand2),
                new RunAction(() => ChangeState(State.PlayerPlay))
                ));

        }

        private void CheckScore()
        {
            if (hand2.IsBust())
                ShowLabel("You Lose", Color.Red);
            else if (hand1.IsBust())
                ShowLabel("You Win", Color.Green);
            else
            {
                int score1 = hand1.GetScore();
                int score2 = hand2.GetScore();
                if (score1 > score2)
                    ShowLabel("You Lose", Color.Red);
                else if (score1 < score2)
                    ShowLabel("You Win", Color.Green);
                else
                    ShowLabel("Draw", Color.Black);
            }
            ChangeState(State.Wait);
        }
        private void ShowLabel(string text, Color color)
        {
            label.Text = text;
            label.Color = color;
        }
        private Action DealCard(Hand hand) // สร้าง Action แจกไพ่
        {
            return new DealCardAction(deck, hand);
        }
    }
}

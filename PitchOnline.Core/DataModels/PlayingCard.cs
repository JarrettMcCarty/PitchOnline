using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PitchOnline.Core
{
    public class PlayingCard : BaseViewModel
    {
        private CardType cardType = CardType.SA;
        public CardType CardType
        {
            get { return cardType; }
            set { cardType = value; OnPropertyChanged(nameof(CardType)); }
        }

        private bool isFaceDown = false;
        public bool IsFaceDown
        {
            get { return isFaceDown; }
            set { isFaceDown = value; OnPropertyChanged(nameof(IsFaceDown)); }
        }

        private bool isPlayable = false;
        public bool IsPlayable
        {
            get { return isPlayable; }
            set { isPlayable = value; OnPropertyChanged(nameof(IsPlayable)); }
        }

        private double faceDownOffset;
        public double FaceDownOffset
        {
            get { return faceDownOffset; }
            set { faceDownOffset = value; OnPropertyChanged(nameof(FaceDownOffset)); }
        }

        private double faceUpOffset;
        public double FaceUpOffset
        {
            get { return faceUpOffset; }
            set { faceUpOffset = value; OnPropertyChanged(nameof(FaceUpOffset)); }
        }

        public CardSuit Suit
        {
            get
            {
                //  The suit can be worked out from
                //  the numeric value of the CardType enum.
                int enumVal = (int)CardType;
                if (enumVal < 13)
                    return CardSuit.Hearts;
                else if (enumVal < 26)
                    return CardSuit.Diamonds;
                else if (enumVal < 39)
                    return CardSuit.Clubs;
                else if (enumVal < 52)
                    return CardSuit.Spades;
                else if (enumVal == 52)
                    return CardSuit.LittleJoker;
                else
                    return CardSuit.BigJoker;
            }
        }

        public int Value => ((int)CardType) % 13;

        public int PitchValue
        {
            get
            {
                var val = (int)CardType;
                if (val % 13 == 0 || val % 13 == 1 || val % 13 == 9 || val % 13 == 10) return 1;
                if (val % 13 == 2) return 3;
                return 0;
            }
        }

        public CardColor Color => ((int)CardType) < 26 ? CardColor.Red : CardColor.Black;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataModel
{

    public enum PlayingCardValue
    {
        _2C, _2D, _2H, _2S,
        _3C, _3D, _3H, _3S,
        _4C, _4D, _4H, _4S,
        _5C, _5D, _5H, _5S,
        _6C, _6D, _6H, _6S,
        _7C, _7D, _7H, _7S,
        _8C, _8D, _8H, _8S,
        _9C, _9D, _9H, _9S,
        _10C, _10D, _10H, _10S,
        _JC, _JD, _JH, _JS,
        _QC, _QD, _QH, _QS,
        _KC, _KD, _KH, _KS,
        _AC, _AD, _AH, _AS,
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class PlayingCard : Card
    {
        private PlayingCardValue _value;
        public PlayingCardValue Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _rank = (Rank)((int)_value / 4);
                _suit = (Suit)((int)_value % 4);
            }
        }

        private Rank _rank;
        public Rank Rank {
            get { return _rank; }
            set {
                _rank = value;
                _value = (PlayingCardValue)((int)_rank * (int)_suit);
            }
        }

        private Suit _suit;
        public Suit Suit {
            get { return _suit; }
            set
            {
                _suit = value;
                _value = (PlayingCardValue)((int)_rank * (int)_suit);
            }
        }

        public override string ToString()
        {
            return _value.ToString().Substring(1);
        }
    }
}

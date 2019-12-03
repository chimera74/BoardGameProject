using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.DataModel
{
    /*
     * Deck is presented as a linked list where first element is the cardData with visible face and last element 
     * is the cardData with visible back. List is not reordered every time IsFaceUp property is changed. Most methods do
     * respect IsFaceUp property to produce their result, e.g., TakeTopCard().
     */
    public class Deck : TwoSidedObject
    {

        public event Action OnCardAdded;
        public event Action OnCardRemoved;
        public event Action OnShuffle;

        private LinkedList<Card> _cardList = new LinkedList<Card>();

        public int CardCount => _cardList.Count;
        public Card VisibleFaceCard => _cardList.First.Value;
        public Card VisibleBackCard => _cardList.Last.Value;

        public void Shuffle()
        {
            Shuffle(new Random());
        }

        public void Shuffle(Random rng)
        {
            if (_cardList == null || _cardList.Count == 0)
                return;

            // Make a copy to not work on an actual list
            LinkedList<Card> oldList = new LinkedList<Card>();
            foreach (Card card in _cardList)
            {
                oldList.AddLast(card);
            }

            LinkedList<Card> newList = new LinkedList<Card>();
            int n = oldList.Count;
            for (int i = 0; i < n; i++)
            {
                int fromPos = rng.Next(0, oldList.Count);
                var currentNode = oldList.First;
                for (int j = 0; j < fromPos; j++)
                {
                    currentNode = currentNode.Next;
                }

                newList.AddLast(currentNode.Value);
                oldList.Remove(currentNode);
            }
            _cardList = newList;

            OnShuffle?.Invoke();
        }

        public Card TakeTopCard()
        {
            Card card = null;
            if (IsFaceUp)
            {
                card = _cardList.First.Value;
                _cardList.RemoveFirst();
            }
            else
            {
                card = _cardList.Last.Value;
                _cardList.RemoveLast();
            }

            card.IsFaceUp = IsFaceUp;
            OnCardRemoved?.Invoke();
            return card;
        }

        public Card TakeBottomCard()
        {
            Card card = null;
            if (!IsFaceUp)
            {
                card = _cardList.First.Value;
                _cardList.RemoveFirst();
            }
            else
            {
                card = _cardList.Last.Value;
                _cardList.RemoveLast();
            }

            card.IsFaceUp = IsFaceUp;
            OnCardRemoved?.Invoke();
            return card;
        }

        public void AddToTheTop(Card card)
        {
            card.IsFaceUp = IsFaceUp;
            if (IsFaceUp)
            {
                _cardList.AddFirst(card);
            }
            else
            {
                _cardList.AddLast(card);
            }
            OnCardAdded?.Invoke();
        }

        public void AddToTheTop(Deck deck)
        {
            if (!IsFaceUp)
            {
                foreach (Card card in deck.GetCardList())
                {
                    _cardList.AddLast(card);
                }
            }
            else
            {
                foreach (Card card in deck.GetCardList().Reverse())
                {
                    _cardList.AddFirst(card);
                }
            }
            OnCardAdded?.Invoke();
        }

        public void AddToTheBottom(Card card)
        {
            if (!IsFaceUp)
            {
                _cardList.AddFirst(card);
            }
            else
            {
                _cardList.AddLast(card);
            }
            OnCardAdded?.Invoke();
        }

        public void AddToTheBottom(Deck deck)
        {
            if (IsFaceUp)
            {
                foreach (Card card in deck.GetCardList())
                {
                    _cardList.AddLast(card);
                }
            }
            else
            {
                foreach (Card card in deck.GetCardList().Reverse())
                {
                    _cardList.AddFirst(card);
                }
            }
            OnCardAdded?.Invoke();
        }

        public Card PeekBottomCard()
        {
            return IsFaceUp ? _cardList.First.Value : _cardList.Last.Value;
        }

        public Card PeekTopCard()
        {
            return !IsFaceUp ? _cardList.First.Value : _cardList.Last.Value;
        }

        public LinkedList<Card> GetCardList()
        {
            return _cardList;
        }
    }
}
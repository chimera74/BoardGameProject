using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataModel
{
    public class Hand : BaseObject
    {

        public event Action OnCardAdded;
        public event Action OnCardRemoved;
        public event Action OnRearranged;

        private List<Card> _cardList = new List<Card>();

        public int CardCount => _cardList.Count;

        public void AddCard(Card card)
        {
            _cardList.Add(card);
            OnCardAdded?.Invoke();
        }

        public void AddCard(Card card, int pos)
        {
            _cardList.Insert(pos, card);
            OnCardAdded?.Invoke();
        }

        public void RemoveCard(Card card)
        {
            _cardList.Remove(card);
            OnCardRemoved?.Invoke();
        }

        public void RemoveCard(int pos)
        {
            _cardList.RemoveAt(pos);
            OnCardRemoved?.Invoke();
        }

        public virtual void Sort(IComparer<Card> c)
        {
            _cardList.Sort(c);
            OnRearranged?.Invoke();
        }

        public int GetCardPosition(Card card)
        {
            return _cardList.IndexOf(card);
        }

    }
}

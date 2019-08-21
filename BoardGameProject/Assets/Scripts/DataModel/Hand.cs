using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataModel
{
    public class Hand : BaseObject
    {
        private List<Card> _cardList = new List<Card>();

        public int CardCount => _cardList.Count;

        public void AddCard(Card card)
        {
            _cardList.Add(card);
        }

        public void AddCard(Card card, int pos)
        {
            _cardList.Insert(pos, card);
        }

        public void RemoveCard(Card card)
        {
            _cardList.Remove(card);
        }

        public void RemoveCard(int pos)
        {
            _cardList.RemoveAt(pos);
        }

        public virtual void Sort(IComparer<Card> c)
        {
            _cardList.Sort(c);
        }

    }
}

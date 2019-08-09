using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class ZIndexManager : MonoBehaviour
    {

        public float yDelta = 0.0001f;

        private LinkedList<CardBehaviour> cards;

        public void Awake()
        {
            cards = new LinkedList<CardBehaviour>();
        }

        public void StartTracking(CardBehaviour card)
        {
            cards.AddLast(card);
            SetCardYPosition(card, cards.Count());
        }

        public void StopTracking(CardBehaviour card)
        {
            cards.Remove(card);
        }

        public void PutOnTop(CardBehaviour card)
        {
            cards.Remove(card);
            cards.AddLast(card);
            RefreshAllPositions();
        }

        private void SetCardYPosition(CardBehaviour card, int order)
        {   
            float yPos = yDelta * order;
            if (yPos < yDelta)
                yPos = yDelta;
            card.transform.localPosition = new Vector3(card.transform.localPosition.x, yPos, card.transform.localPosition.z);
        }

        public void RefreshAllPositions()
        {
            int pos = 0;
            foreach (var card in cards)
            {
                pos++;
                SetCardYPosition(card, pos);
            }
        }

    }
}

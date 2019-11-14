using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.DataModel;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts
{
    public class ZIndexManager : MonoBehaviour
    {

        public float yDelta = 0.0001f;

        private LinkedList<CardBehaviour> cards;

        protected Transform table;
        protected Transform handPlane;

        public void Awake()
        {
            cards = new LinkedList<CardBehaviour>();
            table = GameObject.Find("Table").transform;
            handPlane = GameObject.Find("HandPlane").transform;
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
            switch (card.ModelData.Area)
            {
                case Area.Hand:
                    yPos += handPlane.position.y;
                    break;
                case Area.Table:
                default:
                    yPos += table.position.y;
                    break;
            }
            card.transform.parent.position = new Vector3(card.transform.parent.position.x, yPos, card.transform.parent.position.z);
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

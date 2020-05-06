using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts
{
    public class ZIndexManager : MonoBehaviour
    {

        public float yDelta = 0.0001f;
        public float baseY = 0.0002f;

        private LinkedList<CardBehaviour> cards;

        protected Table table;
        protected HandPlane handPlane;
        protected Transform tableTransform;
        protected Transform handPlaneTransform;

        public void Awake()
        {
            cards = new LinkedList<CardBehaviour>();
            table = FindObjectOfType<Table>();
            tableTransform = table.transform;
            handPlane = FindObjectOfType<HandPlane>();
            handPlaneTransform = handPlane.transform;
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
            float yPos = baseY + yDelta * order;
            if (yPos < yDelta)
                yPos = yDelta;
            if (card.ModelData.parentUID == handPlane.ModelData.uid)
            {
                yPos += handPlaneTransform.position.y;
            }
            else if (card.ModelData.parentUID == table.ModelData.uid)
            {
                yPos += tableTransform.position.y;
            }
            else
            {
                yPos += tableTransform.position.y;
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

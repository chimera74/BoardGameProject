using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DropZones
{
    public class CardSlotDropZone : DropZone
    {

        protected CardSlotBehaviour cardSlotBehaviour;

        public void Awake()
        {
            cardSlotBehaviour = transform.parent.parent.GetComponentInChildren<CardSlotBehaviour>();
        }

        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            CardBehaviour droppingCard = droppingObj as CardBehaviour;
            if (droppingCard == null)
            {
                return false;
            }

            if (cardSlotBehaviour.ModelData.cardInSlot == null
                && cardSlotBehaviour.ModelData.CheckCardEligibility(droppingCard.ModelData))
            {
                cardSlotBehaviour.ModelData.PutCard(droppingCard.ModelData);
                return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    [Serializable]
    public class CardSlot : BaseObject
    {
        public event Action OnPutCard;
        public event Action OnRemoveCard;

        public bool isActive;
        public bool lockCard;
        public List<long> allowedCardIDs;

        [HideInInspector]
        [NonSerialized]
        public Card cardInSlot;

        public void PutCard(Card card)
        {
            cardInSlot = card;
            card.parentUID = uid;
            card.Position = Position;
            OnPutCard?.Invoke();
            card.OnPositionChanged += RemoveCard;
        }

        public void RemoveCard()
        {
            var card = cardInSlot;
            cardInSlot = null;
            OnRemoveCard?.Invoke();
            card.OnPositionChanged -= RemoveCard;
        }

        /// <summary>
        /// Checks if this card could be put in this slot.
        /// </summary>
        /// <param name="card">Card to be checked.</param>
        /// <returns>True if the card is eligible to be put in the slot.</returns>
        public virtual bool CheckCardEligibility(Card card)
        {
            var waCard = card as WACard;
            if (waCard == null)
                return false;
            return CheckWACardEligibility(waCard);
        }

        protected bool CheckWACardEligibility(WACard card)
        {
            if (card == null)
                return false;
            return allowedCardIDs.Contains(card.id);
        }
    }
}

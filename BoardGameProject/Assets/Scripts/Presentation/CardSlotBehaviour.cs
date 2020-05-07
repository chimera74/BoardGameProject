using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class CardSlotBehaviour: BaseObjectBehaviour
    {

        protected override Type ModelType => typeof(CardSlot);
        public new CardSlot ModelData
        {
            get => (CardSlot)_modelData;
            set => _modelData = value;
        }

        public CardSlot slotPreset;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            if (slotPreset != null)
            {
                slotPreset.uid = ModelData.uid;
                slotPreset.parentUID = ModelData.parentUID;
                ModelData = slotPreset;
                ModelData.Position = new Vector2(transform.position.x, transform.position.z);
            }
            ModelData.OnPutCard += OnPutCard;
            ModelData.OnRemoveCard += OnRemoveCard;
        }

        protected virtual void OnPutCard()
        {
            
        }

        protected virtual void OnRemoveCard()
        {

        }

        public virtual bool CheckCardEligibility(CardBehaviour card)
        {
            var waCard = card as WACardBehaviour;
            if (waCard == null)
                return false;
            return ModelData.allowedCardIDs.Contains(waCard.ModelData.id);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ModelData.OnPutCard -= OnPutCard;
            ModelData.OnRemoveCard -= OnRemoveCard;
        }
    }
}

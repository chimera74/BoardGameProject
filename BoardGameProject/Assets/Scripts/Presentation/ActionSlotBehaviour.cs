using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public abstract class ActionSlotBehaviour: BaseObjectBehaviour
    {

        protected override Type ModelType => typeof(WAActionSlot);
        public new WAActionSlot ModelData
        {
            get => (WAActionSlot)_modelData;
            set => _modelData = value;
        }

        public WAActionSlot slotPreset;

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
                uidm.GetRootParentUIDByHierarchy(this);
            }
        }

        public abstract void PutCard(WACardBehaviour card);

        public virtual bool CheckCardEligibility(WACardBehaviour card)
        {
            return ModelData.allowedCardIDs.Contains(card.ModelData.id);
        }
    }
}

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
    public class ActionSlotDropZone : DropZone
    {

        protected ActionSlotBehaviour actionSlotBehaviour;

        public void Awake()
        {
            actionSlotBehaviour = transform.parent.parent.GetComponentInChildren<ActionSlotBehaviour>();
        }

        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            WACardBehaviour droppingCard = droppingObj as WACardBehaviour;
            if (droppingCard == null)
            {
                return false;
            }

            if (actionSlotBehaviour.CheckCardEligibility(droppingCard))
            {
                actionSlotBehaviour.PutCard(droppingCard);
                return true;
            }
            return false;
        }
    }
}

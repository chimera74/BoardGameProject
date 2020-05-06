using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class DoorBehaviour : ActionSlotBehaviour
    {

        public override void PutCard(WACardBehaviour card)
        {
            card.ModelData.parentUID = ModelData.uid;
            ModelData.cardInSlot = card.ModelData;
            card.ModelData.Position = ModelData.Position;
            // TODO open building
        }
    }
}

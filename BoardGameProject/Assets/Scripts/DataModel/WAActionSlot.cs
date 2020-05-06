using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    [Serializable]
    public class WAActionSlot : BaseObject
    {
        public bool isActive;
        public bool lockCard;
        public List<long> allowedCardIDs;

        [HideInInspector]
        public WACard cardInSlot;
    }
}

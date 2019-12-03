using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    [Serializable]
    public class WACard : Card
    {
        public long id;
        public string name;
        public string description;
        public WACardType type;
    }

    

    public enum WACardType
    {
        UNDEFINED,
        STAT,
        ITEM,
        CHARACTER
    }
}
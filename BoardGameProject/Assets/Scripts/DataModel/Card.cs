using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataModel
{
    [Serializable]
    public class Card : TwoSidedObject
    {
        public bool allowStacking;
    }
}

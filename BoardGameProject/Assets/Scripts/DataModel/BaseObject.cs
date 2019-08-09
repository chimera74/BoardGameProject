using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    public class BaseObject
    {

        public Guid GUID { get; }
        public Vector2 Position { get; set; }
        public Player Owner { get; set; }

        public BaseObject()
        {
            GUID = Guid.NewGuid();
            Position = Vector2.zero;
            Owner = null;
        }
    }
}

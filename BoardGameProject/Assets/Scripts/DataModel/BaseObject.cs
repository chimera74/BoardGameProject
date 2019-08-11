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
        public event Action OnPositionChanged;

        public Guid GUID { get; }

        private Vector2 _position;
        public Vector2 Position {
            get { return _position; }
            set
            {
                _position = value;
                OnPositionChanged?.Invoke();
            }
        }
        public Player Owner { get; set; }

        public BaseObject()
        {
            GUID = Guid.NewGuid();
            Position = Vector2.zero;
            Owner = null;
        }
    }
}

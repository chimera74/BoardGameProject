using System;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    [Serializable]
    public class BaseObject
    {
        public event Action OnPositionChanged;

        public long UID { get; }

        private Vector2 _position;
        public Vector2 Position {
            get { return _position; }
            set
            {
                _position = value;
                OnPositionChanged?.Invoke();
            }
        }

        /// <summary>
        /// Area which this object belongs to. I.e cardData being in hand or on a table.
        /// </summary>
        public Area Area { get; set; }
        public Player Owner { get; set; }

        public BaseObject()
        {
            UID = 0; // TODO ID generator
            Position = Vector2.zero;
            Owner = null;
        }
    }

    public enum Area
    {
        Table,
        Hand
    }
}

using System;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    [Serializable]
    public class BaseObject
    {
        
        public event Action OnPositionChanged;

        public long uid;

        private Vector2 _position;
        public Vector2 Position {
            get => _position;
            set
            {
                _position = value;
                OnPositionChanged?.Invoke();
            }
        }

        /// <summary>
        /// UID of the parent that this object belongs to.
        /// </summary>
        public long parentUID;
        public Player Owner { get; set; }

        public BaseObject()
        {
            uid = 0; // TODO ID generator
            Position = Vector2.zero;
            Owner = null;
        }

        protected bool Equals(BaseObject other)
        {
            return uid == other.uid;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseObject)obj);
        }

        public override int GetHashCode()
        {
            return uid.GetHashCode();
        }
    }
}

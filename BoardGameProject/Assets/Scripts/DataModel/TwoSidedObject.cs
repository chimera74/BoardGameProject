using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataModel
{
    /// <summary>
    /// Implements flipping functionality
    /// </summary>
    public class TwoSidedObject : BaseObject
    {

        public event Action OnFaceUpChanged;

        private bool _isFaceUp;

        public bool IsFaceUp
        {
            get { return _isFaceUp; }
            set
            {
                var old = _isFaceUp;
                _isFaceUp = value;
                if (old != _isFaceUp)
                    OnFaceUpChanged?.Invoke();
            }
        }

        public TwoSidedObject()
        {
            IsFaceUp = true;
        }

        public void Flip()
        {
            IsFaceUp = !IsFaceUp;
        }
    }
}

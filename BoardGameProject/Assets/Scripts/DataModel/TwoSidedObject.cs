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
        public bool IsFaceUp { get; set; }

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

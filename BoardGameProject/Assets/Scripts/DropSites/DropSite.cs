using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class DropSite : MonoBehaviour
    {
        /// <summary>
        /// Method that processes the droping of a card here.
        /// </summary>
        /// <returns>
        /// False if card object was destroyed, true if it still exisits.
        /// </returns>
        public abstract bool DropCard(CardBehaviour droppingCard, Vector3 position);

        /// <summary>
        /// Method that processes the droping of a deck here.
        /// </summary>
        /// <returns>
        /// False if card object was destroyed, true if it still exisits.
        /// </returns>
        public abstract bool DropDeck(DeckBehaviour droppingDeck, Vector3 position);
    }
}

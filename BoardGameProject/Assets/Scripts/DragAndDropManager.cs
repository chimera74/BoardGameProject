using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class DragAndDropManager : MonoBehaviour
    {
        public event Action OnDragStart;
        public event Action OnDragStop;

        public void TriggerOnDragStart()
        {
            if (OnDragStart != null)
                OnDragStart();
        }

        public void TriggerOnDragStop()
        {
            if (OnDragStop != null)
                OnDragStop();
        }

        /// <summary>
        /// Tries to put card at the spot. If there is another card or deck (within delta)
        /// then cards/decks are combined.
        /// </summary>
        /// <returns>
        /// False if card object was destroyed, true if it still exisits.
        /// </returns>
        public bool PutCardAt(CardBehaviour card, Vector3 pos, Vector3 originalPos)
        {
            TriggerOnDragStart();
            bool res = RaycastingHelper.RaycastToDropSites(out var hit, pos);
            TriggerOnDragStop();

            if (res)
            {   
                DropSite ds = hit.transform.GetComponent<DropSite>();
                return ds.DropCard(card, hit.point);
            }
            else
            {
                // revert to original position
                card.transform.parent.position = originalPos;
                return true;
            }
        }

        /// <summary>
        /// Tries to put deck at the spot. If there is another card or deck (within delta)
        /// then cards/decks are combined.
        /// </summary>
        /// <returns>
        /// False if deck object was destroyed, true if it still exisits.
        /// </returns>
        public bool PutDeckAt(DeckBehaviour deck, Vector3 pos, Vector3 originalPos)
        {
            RaycastHit hit;
            if (RaycastingHelper.RaycastToDropSites(out hit, pos))
            {
                DropSite ds = hit.transform.GetComponent<DropSite>();
                return ds.DropDeck(deck, hit.point);
            }
            else
            {
                deck.transform.parent.position = originalPos;
                return true;
            }
        }
    }
}

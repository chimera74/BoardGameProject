using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.DropZones
{
    public class DeckDropZone : DropZone
    {

        protected CardGenerator cg;
        protected DeckBehaviour deckBehaviour;

        public void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            deckBehaviour = transform.parent.GetComponentInChildren<DeckBehaviour>();
        }

        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            CardBehaviour droppingCard = droppingObj as CardBehaviour;
            if (droppingCard != null)
            {
                DropCard(droppingCard, position);
                return true;
            }

            DeckBehaviour droppingDeck = droppingObj as DeckBehaviour;
            if (droppingDeck != null)
            {
                DropDeck(droppingDeck, position);
                return true;
            }

            return false;
        }

        public void DropCard(CardBehaviour droppingCard, Vector3 position)
        {   
            // destroy dropping card
            Destroy(droppingCard.transform.parent.gameObject);

            // add card to the laying deck
            if (Input.GetKey(KeyCode.LeftAlt))
                deckBehaviour.ModelData.AddToTheBottom(droppingCard.ModelData);
            else
                deckBehaviour.ModelData.AddToTheTop(droppingCard.ModelData);
        }

        public void DropDeck(DeckBehaviour droppingDeck, Vector3 position)
        {
            // destroy dropping deck
            Destroy(droppingDeck.transform.parent.gameObject);

            // add deck to the laying deck
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                for (int i = droppingDeck.ModelData.CardCount; i > 0; i--)
                {
                    var card = droppingDeck.ModelData.TakeTopCard();
                    deckBehaviour.ModelData.AddToTheBottom(card);
                }
            }   
            else
            {
                deckBehaviour.ModelData.AddToTheTop(droppingDeck.ModelData);
            }
        }
    }
}

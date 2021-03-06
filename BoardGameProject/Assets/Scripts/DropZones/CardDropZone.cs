using System.Collections.Generic;
using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts.DropZones
{
    public class CardDropZone : DropZone
    {

        protected CardGenerator cg;
        protected CardBehaviour cardBehaviour;

        public void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            cardBehaviour = transform.parent.GetComponentInChildren<CardBehaviour>();
        }

        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            if (!cardBehaviour.ModelData.allowStacking)
                return false;

            CardBehaviour droppingCard = droppingObj as CardBehaviour;
            if (droppingCard != null)
            {
                if (!droppingCard.ModelData.allowStacking)
                    return false;

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

        public void DropCard(CardBehaviour droppingCard, Vector3 position) { 
            // the position of the laying cardData will be position of the deck
            Vector3 pos = transform.parent.position;

            // destroy laying cardData (this)
            Destroy(transform.parent.gameObject);

            // destroy dropping cardData (parameter)
            Destroy(droppingCard.transform.parent.gameObject);

            // create a deck with this 2 cards on a position of laying cardData
            List<Card> cards = new List<Card>();
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                cards.Add(cardBehaviour.ModelData);
                cards.Add(droppingCard.ModelData);
            }
            else
            {
                cards.Add(droppingCard.ModelData);
                cards.Add(cardBehaviour.ModelData);
            }
            
            cg.SpawnDeck(cards, pos, cardBehaviour.ModelData.IsFaceUp);
        }

        public void DropDeck(DeckBehaviour droppingDeck, Vector3 position)
        {
            var pos = cardBehaviour.transform.parent.position;

            // destroy laying cardData
            Destroy(cardBehaviour.transform.parent.gameObject);

            // add cardData to the dropping deck
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                droppingDeck.ModelData.AddToTheTop(cardBehaviour.ModelData);
            }
            else
            {
                droppingDeck.ModelData.AddToTheBottom(cardBehaviour.ModelData);
            }

            droppingDeck.transform.parent.position = pos;
        }
    }
}

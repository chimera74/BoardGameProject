using Assets.Scripts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class CardDropSite : DropSite
    {

        protected CardGenerator cg;
        protected CardBehaviour cardBehaviour;

        public void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            cardBehaviour = transform.parent.GetComponentInChildren<CardBehaviour>();
        }

        public override bool DropCard(CardBehaviour droppingCard, Vector3 position)
        {
            // the position of the laying card will be position of the deck
            Vector3 pos = transform.parent.position;

            // destroy laying card (this)
            Destroy(transform.parent.gameObject);

            // destroy dropping card (parameter)
            Destroy(droppingCard.transform.parent.gameObject);

            // create a deck with this 2 cards on a position of laying card
            List<Card> cards = new List<Card>();
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                cards.Add(cardBehaviour.cardData);
                cards.Add(droppingCard.cardData);
            }
            else
            {
                cards.Add(droppingCard.cardData);
                cards.Add(cardBehaviour.cardData);
            }
            
            cg.SpawnDeck(cards, pos, cardBehaviour.cardData.IsFaceUp);

            return true;
        }

        public override bool DropDeck(DeckBehaviour droppingDeck, Vector3 position)
        {
            var pos = cardBehaviour.transform.parent.position;
            // destroy lcaying card
            Destroy(cardBehaviour.transform.parent.gameObject);

            // add card to the dropping deck
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                droppingDeck.deckData.AddToTheTop(cardBehaviour.cardData);
            }
            else
            {
                droppingDeck.deckData.AddToTheBottom(cardBehaviour.cardData);
            }

            droppingDeck.transform.parent.position = pos;
            droppingDeck.AdjustSize();
            droppingDeck.UpdateTextures();

            return false;
        }
    }
}

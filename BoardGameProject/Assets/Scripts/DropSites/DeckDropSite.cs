using Assets.Scripts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class DeckDropSite : DropSite
    {

        protected CardGenerator cg;
        protected DeckBehaviour deckBehaviour;

        public void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            deckBehaviour = transform.parent.GetComponentInChildren<DeckBehaviour>();
        }

        public override bool DropCard(CardBehaviour droppingCard, Vector3 position)
        {   
            // destroy dropping card
            Destroy(droppingCard.transform.parent.gameObject);

            // add card to the laying deck
            if (Input.GetKey(KeyCode.LeftAlt))
                deckBehaviour.deckData.AddToTheBottom(droppingCard.cardData);
            else
                deckBehaviour.deckData.AddToTheTop(droppingCard.cardData);
            deckBehaviour.AdjustSize();
            deckBehaviour.UpdateTextures();

            return true;
        }

        public override bool DropDeck(DeckBehaviour droppingDeck, Vector3 position)
        {
            // destroy dropping deck
            Destroy(droppingDeck.transform.parent.gameObject);

            // add deck to the laying deck
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                for (int i = droppingDeck.deckData.CardCount; i > 0; i--)
                {
                    var card = droppingDeck.deckData.TakeTopCard();
                    deckBehaviour.deckData.AddToTheBottom(card);
                }
            }   
            else
            {
                deckBehaviour.deckData.AddToTheTop(droppingDeck.deckData);
            }
                
            deckBehaviour.AdjustSize();
            deckBehaviour.UpdateTextures();

            return true;
        }
    }
}

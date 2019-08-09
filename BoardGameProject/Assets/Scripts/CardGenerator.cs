using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.DataModel;

namespace Assets.Scripts
{
    public class CardGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject CardPrefab;
        public GameObject DeckPrefab;

        protected Transform table;
        protected DragAndDropManager dndm;

        public void Awake()
        {
            dndm = FindObjectOfType<DragAndDropManager>();
        }

        // Use this for initialization
        public void Start()
        {
            table = FindObjectOfType<Table>().transform;
        }

        public GameObject SpawnCard(Card card, Vector3 pos)
        {
            GameObject go = Instantiate(CardPrefab, pos, Quaternion.identity, table);
            var pc = go.GetComponentInChildren<PlayingCardBehaviour>();
            pc.cardData = card;
            if (dndm.PutCardAt(pc, pos, pos))
                pc.UpdateTextures();
            return go;
        }

        public GameObject SpawnDeck(ICollection<Card> cards, Vector3 pos, bool faceUp)
        {
            Deck newDeck = new Deck() {IsFaceUp = faceUp};
            foreach (Card card in cards)
            {
                newDeck.AddToTheBottom(card);
            }

            GameObject go = Instantiate(DeckPrefab, pos, Quaternion.identity, table);
            var deckBhvr = go.GetComponentInChildren<DeckBehaviour>();
            deckBhvr.deckData = newDeck;
            deckBhvr.AdjustSize();
            deckBhvr.UpdateTextures();
            return go;
        }
    }
}

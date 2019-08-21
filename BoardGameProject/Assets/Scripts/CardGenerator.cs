using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.DataModel;
using Assets.Scripts.Objects;

namespace Assets.Scripts
{
    public class CardGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject cardPrefab;

        public GameObject deckPrefab;

        protected Transform table;
        protected DragAndDropManager dndm;

        public void Awake()
        {
            dndm = FindObjectOfType<DragAndDropManager>();
        }

        // Use this for initialization
        public virtual void Start()
        {
            table = FindObjectOfType<Table>().transform;
        }

        public virtual Texture2D GetCardFaceTexture(Card card)
        {
            return null; //TODO return placeholder texture
        }

        public virtual Texture2D GetCardBackTexture(Card card)
        {
            return null; //TODO return placeholder texture
        }

        public GameObject SpawnCard(Card card, Vector3 pos)
        {
            GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity, table);
            var pc = go.GetComponentInChildren<CardBehaviour>();
            pc.ModelData = card;
            dndm.PutAt(pc, pos);
            return go;
        }

        public GameObject SpawnDeck(ICollection<Card> cards, Vector3 pos, bool faceUp)
        {
            Deck newDeck = new Deck() {IsFaceUp = faceUp};
            foreach (Card card in cards)
            {
                newDeck.AddToTheBottom(card);
            }

            GameObject go = Instantiate(deckPrefab, pos, Quaternion.identity, table);
            var deckBhvr = go.GetComponentInChildren<DeckBehaviour>();
            deckBhvr.ModelData = newDeck;
            return go;
        }
    }
}
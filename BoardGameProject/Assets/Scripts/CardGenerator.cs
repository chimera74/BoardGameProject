using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;

namespace Assets.Scripts
{
    public class CardGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject cardPrefab;

        public GameObject deckPrefab;

        protected Transform table;
        protected DragAndDropManager dndm;
        protected Transform hand;

        protected virtual void Awake()
        {
            dndm = FindObjectOfType<DragAndDropManager>();
            table = FindObjectOfType<Table>().transform;
            hand = FindObjectOfType<HandBehaviour>().transform;
        }

        protected virtual void Start()
        {
            
        }

        public virtual Texture2D GetCardFaceTexture(Card card)
        {
            return null; //TODO return placeholder texture
        }

        public virtual Texture2D GetCardBackTexture(Card card)
        {
            return null; //TODO return placeholder texture
        }

        public GameObject SpawnCard(Card card, Area area, Vector3 pos)
        {
            GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity);
            var pc = go.GetComponentInChildren<CardBehaviour>();
            pc.ModelData = card;
            dndm.PutAt(pc, area, pos);
            return go;
        }

        public GameObject SpawnCard(Card card)
        {
            return SpawnCard(card, Area.Table, Vector3.zero);
        }

        public GameObject SpawnDeck(ICollection<Card> cards, Vector3 pos, bool faceUp)
        {
            Deck newDeck = new Deck() {IsFaceUp = faceUp};
            foreach (Card card in cards)
            {
                newDeck.AddToTheBottom(card);
            }

            GameObject go = Instantiate(deckPrefab, pos, Quaternion.identity);
            var deckBhvr = go.GetComponentInChildren<DeckBehaviour>();
            deckBhvr.ModelData = newDeck;
            return go;
        }
    }
}
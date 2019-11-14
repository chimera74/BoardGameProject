using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.DataModel;
using Assets.Scripts.Objects;
using Assets.Scripts.Presentation;

namespace Assets.Scripts
{
    public class CardGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject cardPrefab;
        public GameObject cardInHandPrefab;

        public GameObject deckPrefab;

        protected Transform table;
        protected DragAndDropManager dndm;
        protected Transform hand;

        public void Awake()
        {
            dndm = FindObjectOfType<DragAndDropManager>();
            table = FindObjectOfType<Table>().transform;
            hand = FindObjectOfType<HandBehaviour>().transform;
        }

        public virtual void Start()
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

        public GameObject SpawnCard(Card card, Vector3 pos, long areaId)
        {
            if (areaId == 1)
                return SpawnCardInHand(card);
            else
                return SpawnCardOnTable(card, pos);
        }

        public GameObject SpawnCardOnTable(Card card, Vector3 pos)
        {
            GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity);
            var pc = go.GetComponentInChildren<CardBehaviour>();
            pc.ModelData = card;
            dndm.PutAt(pc, pos);
            return go;
        }

        public GameObject SpawnCardInHand(Card card)
        {
            Quaternion rot = hand.rotation * Quaternion.Euler(-90, 0, 0);
            GameObject go = Instantiate(cardInHandPrefab, hand.position, rot, hand);
            var pc = go.GetComponentInChildren<CardInHandBehaviour>();
            pc.ModelData = card;
            return go;
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
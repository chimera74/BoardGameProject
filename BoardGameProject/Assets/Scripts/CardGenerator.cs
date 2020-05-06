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

        protected Table table;
        protected DragAndDropManager dndm;
        protected HandPlane handPlane;
        protected UIDManager uidm;

        protected virtual void Awake()
        {
            uidm = FindObjectOfType<UIDManager>();
            dndm = FindObjectOfType<DragAndDropManager>();
            table = FindObjectOfType<Table>();
            handPlane = FindObjectOfType<HandPlane>();
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

        public GameObject SpawnCard(Card card, long parentUID, Vector3 pos)
        {
            GameObject go = Instantiate(cardPrefab, pos, Quaternion.identity);
            var beh = go.GetComponentInChildren<CardBehaviour>();
            beh.ModelData = card;
            //uidm.RegisterUID(card, beh);
            go.transform.parent = uidm.GetBehaviourByUID(parentUID).transform;
            dndm.PutAt(beh, parentUID, pos);
            return go;
        }

        public GameObject SpawnCard(Card card)
        {
            return SpawnCard(card, table.ModelData.uid, Vector3.zero);
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
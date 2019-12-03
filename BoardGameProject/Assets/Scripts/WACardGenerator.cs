using Assets.Scripts;
using Assets.Scripts.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Objects;
using Assets.Scripts.Scriptables;
using UnityEngine;

namespace Assets.Scripts
{
    public class WACardGenerator : CardGenerator
    {
        public WACardSO[] cards;

        private Dictionary<long, WACardSO> cardsDictionary;

        public override void Start()
        {
            base.Start();
            LoadCards();
        }

        public void LoadCards()
        {
            cardsDictionary = new Dictionary<long, WACardSO>();
            foreach (WACardSO c in cards)
            {
                cardsDictionary[c.id] = c;
            }
        }

        public void Update()
        {
            if (Input.GetButtonDown("Generate Card"))
            {
                GenerateCard(CreateCardFromSO(cardsDictionary[0]), new Vector3());
            }
        }

        public WACard CreateCardFromSO(WACardSO so)
        {
            var card = new WACard() { id = so.id, name = so.name, description = so.description, type = so.type};
            return card;
        }

        public override Texture2D GetCardFaceTexture(Card card)
        {
            if (card is WACard c)
            {
                return cardsDictionary[c.id].image;
            }

            return base.GetCardFaceTexture(card);
        }

        public void GenerateCard(WACard card, Vector3 pos)
        {
            SpawnCardOnTable(card, pos);
        }
    }
}
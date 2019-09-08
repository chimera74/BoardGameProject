﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class HandBehaviour : ModelContainerBehaviour
    {
        public float cardXOffset = 0.3f;
        public float cardZOffset = 0.0001f;

        public new Hand ModelData
        {
            get { return (Hand) _modelData; }
            set { _modelData = value; }
        }

        protected CardGenerator cg;

        protected virtual void Awake()
        {
            _modelData = new Hand();
            cg = FindObjectOfType<CardGenerator>();
        }

        protected virtual void Start()
        {
            ModelData.OnCardAdded += RearrangeCardsToModel;
            ModelData.OnCardRemoved += RearrangeCardsToModel;
            ModelData.OnRearranged += RearrangeCardsToModel;

            GenerateMockCards();
        }

        private void GenerateMockCards()
        {
            // Test Objects
            cg.SpawnCardInHand(new PlayingCard() {Value = PlayingCardValue._2H});
            cg.SpawnCardInHand(new PlayingCard() {Value = PlayingCardValue._10S});
            cg.SpawnCardInHand(new PlayingCard() {Value = PlayingCardValue._AD});

            foreach (var cihb in GetComponentsInChildren<CardInHandBehaviour>())
            {
                ModelData.AddCard(cihb.ModelData);
            }
        }

        protected virtual void OnDestroy()
        {
            ModelData.OnCardAdded -= RearrangeCardsToModel;
            ModelData.OnCardRemoved -= RearrangeCardsToModel;
            ModelData.OnRearranged -= RearrangeCardsToModel;
        }

        public void AddCard(CardInHandBehaviour card)
        {
            ModelData.AddCard(card.ModelData);
            // stop drag for card
            // position it in hand
        }

        public void RearrangeCardsToModel()
        {
            int count = ModelData.CardCount;
            float startXPos = cardXOffset * (-(count / 2) + 0.5f - (count % 2) * 0.5f);

            var cihbs = GetComponentsInChildren<CardInHandBehaviour>();
            foreach (CardInHandBehaviour cihb in cihbs)
            {
                // find it's position
                int pos = ModelData.GetCardPosition(cihb.ModelData);
                Vector3 newPos = new Vector3(startXPos + pos * cardXOffset, 0, -cardZOffset * pos);

                // move to corresponding pos
                cihb.transform.parent.localPosition = newPos;
            }
        }
    }
}
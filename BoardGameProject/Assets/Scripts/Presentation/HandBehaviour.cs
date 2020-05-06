using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class HandBehaviour : ModelContainerBehaviour
    {
        public float cardXOffset = 0.3f;
        public float cardZOffset = 0.0001f;

        protected override Type ModelType => typeof(Hand);
        public new Hand ModelData
        {
            get { return (Hand) _modelData; }
            set { _modelData = value; }
        }

        protected CardGenerator cg;

        protected override void Awake()
        {
            base.Awake();
            _modelData = new Hand();
            cg = FindObjectOfType<CardGenerator>();
        }

        protected override void Start()
        {
            base.Start();
            ModelData.OnCardAdded += RearrangeCardsToModel;
            ModelData.OnCardRemoved += RearrangeCardsToModel;
            ModelData.OnRearranged += RearrangeCardsToModel;

            GenerateMockCards();
        }

        private void GenerateMockCards()
        {
            // Test Objects
            cg.SpawnCard(new PlayingCard() {Value = PlayingCardValue._2H});
            cg.SpawnCard(new PlayingCard() {Value = PlayingCardValue._10S});
            cg.SpawnCard(new PlayingCard() {Value = PlayingCardValue._AD});

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
            // stop drag for cardData
            // position it in hand
        }

        public void RearrangeCardsToModel()
        {
            var cihbs = GetComponentsInChildren<CardInHandBehaviour>();
            foreach (CardInHandBehaviour cihb in cihbs)
            {
                cihb.transform.parent.localPosition = GetCardIdlePosition(cihb);
            }
        }

        public Vector3 GetCardIdlePosition(CardInHandBehaviour cihb)
        {
            int count = ModelData.CardCount;
            float startXPos = cardXOffset * (-(count / 2) + 0.5f - (count % 2) * 0.5f);
            int pos = ModelData.GetCardPosition(cihb.ModelData);
            Vector3 localPos = new Vector3(startXPos + pos * cardXOffset, 0, -cardZOffset * pos);

            return localPos;
        }
    }
}
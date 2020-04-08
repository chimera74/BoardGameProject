﻿using Assets.Scripts;
using Assets.Scripts.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Scriptables;
using IngameDebugConsole;
using UnityEngine;

namespace Assets.Scripts
{
    public class WACardGenerator : CardGenerator
    {
        public WACardSO[] cards;

        private Dictionary<long, WACardSO> cardsDictionary;

        protected DebugLogManager debugConsole;

        protected override void Awake()
        {
            base.Awake();
            debugConsole = FindObjectOfType<DebugLogManager>();
        }

        protected override void Start()
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
            if (debugConsole.IsOpen())
                return;

            if (Input.GetButtonDown("Generate Card"))
            {
                GenerateCard(0, new Vector3());
            }
        }

        public WACard CreateCardFromSO(WACardSO so)
        {
            var card = new WACard() { id = so.id, name = so.name, description = so.description, type = so.type, allowStacking = so.allowStacking};
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

        public void GenerateCard(WACard card, Area area, Vector3 pos)
        {
            SpawnCard(card, area, pos);
        }

        public void GenerateCard(long id, Vector3 pos)
        {
            if (cardsDictionary.ContainsKey(id))
            {
                GenerateCard(CreateCardFromSO(cardsDictionary[id]), Area.Hand, pos);
            }
        }

        public void GenerateCard(long id, Vector3 pos, Area area)
        {
            if (cardsDictionary.ContainsKey(id))
            {
                GenerateCard(CreateCardFromSO(cardsDictionary[id]), area, pos);
            }
        }
    }
}
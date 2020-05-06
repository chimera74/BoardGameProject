using Assets.Scripts;
using Assets.Scripts.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayingCardGenerator : CardGenerator
    {
        [Header("Textures")]
        public Texture2D[] faceTextures;

        protected override void Start()
        {
            base.Start();
            faceTextures = new Texture2D[52];
            foreach (PlayingCardValue pc in Enum.GetValues(typeof(PlayingCardValue)))
            {
                string path = "Textures/" + pc.ToString("g").Substring(1);

                faceTextures[(int) pc] = Resources.Load<Texture2D>(path);
                if (faceTextures[(int) pc] == null)
                    UnityEngine.Debug.Log(path + " is null");
            }
        }

        public void Update()
        {
            if (Input.GetButtonDown("Generate Card"))
            {
                GenerateRandomPlayingCard(new Vector3(), true);
            }

            if (Input.GetButtonDown("Generate Deck"))
            {
                GeneratePlayingCardDeck(new Vector3(), true, false);
            }
        }

        public override Texture2D GetCardFaceTexture(Card card)
        {
            if (card is PlayingCard pc)
            {
                return faceTextures[(int) pc.Value];
            }

            return base.GetCardFaceTexture(card);
        }

        public void GenerateRandomPlayingCard(Vector3 pos, bool isFaceUp)
        {
            var value = (PlayingCardValue) UnityEngine.Random.Range(0, 52);
            PlayingCard cardData = new PlayingCard() {Value = value};
            SpawnCard(cardData, table.ModelData.uid, pos);
        }


        public GameObject GeneratePlayingCardDeck(Vector3 pos, bool faceUp, bool shuffle)
        {
            Deck newDeck = new Deck();
            foreach (PlayingCardValue pcv in Enum.GetValues(typeof(PlayingCardValue)))
            {
                PlayingCard pc = new PlayingCard() {Value = pcv};
                newDeck.AddToTheTop(pc);
            }

            if (shuffle)
                newDeck.Shuffle();

            GameObject go = Instantiate(deckPrefab, pos, Quaternion.identity, table.transform);
            var pcDeck = go.GetComponentInChildren<PlayingCardDeckBehaviour>();
            pcDeck.ModelData = newDeck;
            return go;
        }
    }
}
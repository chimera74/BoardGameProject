using Assets.Scripts;
using Assets.Scripts.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayingCardGenerator : CardGenerator
    {

        [Header("Textures")]
        public Texture2D[] faceTextures;

        public new void Start()
        {
            base.Start();
            faceTextures = new Texture2D[52];
            foreach (PlayingCardValue pc in Enum.GetValues(typeof(PlayingCardValue)))
            {
                string path = "Textures/" + pc.ToString("g").Substring(1);

                faceTextures[(int)pc] = Resources.Load<Texture2D>(path);
                if (faceTextures[(int)pc] == null)
                    UnityEngine.Debug.Log(path + " is null");
            }
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.G))
            {
                GenerateRandomPlayingCard(new Vector3(), true);
            }
            if (Input.GetKeyUp(KeyCode.H))
            {
                GeneratePlayingCardDeck(new Vector3(), true, false);
            }
        }

        public void GenerateRandomPlayingCard(Vector3 pos, bool isFaceUp)
        {
            var value = (PlayingCardValue)UnityEngine.Random.Range(0, 52);
            PlayingCard cardData = new PlayingCard() { Value = value };
            SpawnCard(cardData, pos);
        }


        public GameObject GeneratePlayingCardDeck(Vector3 pos, bool faceUp, bool shuffle)
        {
            Deck newDeck = new Deck();
            foreach (PlayingCardValue pcv in Enum.GetValues(typeof(PlayingCardValue)))
            {
                PlayingCard pc = new PlayingCard() { Value = pcv };
                newDeck.AddToTheTop(pc);
            }
            if (shuffle)
                newDeck.Shuffle();

            GameObject go = Instantiate(DeckPrefab, pos, Quaternion.identity, table);
            var pcDeck = go.GetComponentInChildren<PlayingCardDeckBehaviour>();
            pcDeck.deckData = newDeck;
            pcDeck.UpdateTextures();
            return go;
        }
    }
}
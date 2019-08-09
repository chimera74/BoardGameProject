using Assets.Scripts.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayingCardDeckBehaviour : DeckBehaviour
    {

        protected PlayingCardGenerator pcg;

        public new void Awake()
        {
            base.Awake();
            pcg = FindObjectOfType<PlayingCardGenerator>();
        }

        public override void UpdateTextures()
        {
            int n = (int)((PlayingCard)deckData.PeekBottomCard()).Value;
            rend.materials[1].mainTexture = pcg.faceTextures[n];
        }

    }
}
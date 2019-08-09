using Assets.Scripts.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{

    public class PlayingCardBehaviour : CardBehaviour
    {   

        protected PlayingCardGenerator pcg;

        public PlayingCardBehaviour() : base()
        {
            
        }

        protected new void Awake()
        {
            base.Awake();
            pcg = FindObjectOfType<PlayingCardGenerator>();
        }

        protected new void Start()
        {
            base.Start();
        }

        override public void UpdateTextures()
        {
            int n = (int)((PlayingCard)cardData).Value;
            rend.materials[1].mainTexture = pcg.faceTextures[n];
        }

    }
}

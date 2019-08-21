using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class CardAppearance : BaseObjectAppearance
    {
        [HideInInspector]
        public Texture2D faceTexture;
        [HideInInspector]
        public Texture2D backTexture;

        protected CardBehaviour cb;
        protected CardGenerator cg;
        protected Renderer rend;

        protected void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            cb = GetComponent<CardBehaviour>();
            rend = GetComponent<Renderer>();
        }

        public override void UpdateAppearance()
        {
            faceTexture = cg.GetCardFaceTexture(cb.ModelData);
            backTexture = cg.GetCardBackTexture(cb.ModelData);

            rend.materials[1].mainTexture = faceTexture;
            // TODO Assign cardback
        }
    }
}
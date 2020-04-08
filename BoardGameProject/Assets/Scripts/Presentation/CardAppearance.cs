using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class CardAppearance : BaseObjectAppearance
    {
        [HideInInspector]
        public Texture2D faceTexture;
        [HideInInspector]
        public Texture2D backTexture;

        protected ModelContainerBehaviour mc;
        protected CardGenerator cg;
        protected Renderer rend;

        protected void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            mc = GetComponent<ModelContainerBehaviour>();
            rend = GetComponent<Renderer>();
        }

        public override void UpdateAppearance()
        {
            faceTexture = cg.GetCardFaceTexture((Card) mc.ModelData);
            backTexture = cg.GetCardBackTexture((Card) mc.ModelData);

            rend.materials[1].mainTexture = faceTexture;
            // TODO Assign cardback
        }
    }
}
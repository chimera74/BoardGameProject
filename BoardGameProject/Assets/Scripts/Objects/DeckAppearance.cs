using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class DeckAppearance : BaseObjectAppearance
    {
        [HideInInspector]
        public Texture2D faceTexture;
        [HideInInspector]
        public Texture2D backTexture;

        [Tooltip("Number of cardData in the deck for it to appear as max size.")]
        public int fullDeckCount = 52;

        protected DeckBehaviour db;
        protected CardGenerator cg;
        protected Renderer rend;

        protected float baseYScale;
        protected float baseYPos;

        protected virtual void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            db = GetComponent<DeckBehaviour>();
            rend = GetComponent<Renderer>();

            baseYScale = transform.localScale.y;
            baseYPos = transform.localPosition.y;
        }

        protected override void Start()
        {
            base.Start();
            db.ModelData.OnCardAdded += UpdateAppearance;
            db.ModelData.OnCardRemoved += UpdateAppearance;
            db.ModelData.OnShuffle += UpdateAppearance;
        }

        protected virtual void OnDestroy()
        {
            db.ModelData.OnCardAdded -= UpdateAppearance;
            db.ModelData.OnCardRemoved -= UpdateAppearance;
            db.ModelData.OnShuffle -= UpdateAppearance;
        }

        public override void UpdateAppearance()
        {
            faceTexture = cg.GetCardFaceTexture(db.ModelData.VisibleFaceCard);
            backTexture = cg.GetCardBackTexture(db.ModelData.VisibleBackCard);

            rend.materials[1].mainTexture = faceTexture;
            // TODO Assign cardback

            AdjustSize();
        }

        public void AdjustSize()
        {
            if (db.ModelData.CardCount >= fullDeckCount)
            {
                // Full deck
                transform.localScale = new Vector3(transform.localScale.x, baseYScale, transform.localScale.z);
            }
            else
            {
                float yScale = (float)db.ModelData.CardCount / fullDeckCount;
                transform.localScale = new Vector3(transform.localScale.x, baseYScale * yScale, transform.localScale.z);
                transform.localPosition = new Vector3(0, baseYPos * yScale, 0);
            }
        }
    }
}

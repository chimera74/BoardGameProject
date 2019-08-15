using Assets.Scripts.DataModel;

namespace Assets.Scripts.Objects
{
    public class PlayingCardDeckBehaviour : DeckBehaviour
    {

        protected PlayingCardGenerator pcg;

        protected override void Awake()
        {
            base.Awake();
            pcg = FindObjectOfType<PlayingCardGenerator>();
        }

        public override void UpdateTextures()
        {
            int n = (int)((PlayingCard)ModelData.PeekBottomCard()).Value;
            rend.materials[1].mainTexture = pcg.faceTextures[n];
        }

    }
}
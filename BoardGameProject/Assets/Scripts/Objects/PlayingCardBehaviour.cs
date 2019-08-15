using Assets.Scripts.DataModel;

namespace Assets.Scripts.Objects
{

    public class PlayingCardBehaviour : CardBehaviour
    {

        public new PlayingCard ModelData
        {
            get { return (PlayingCard)_modelData; }
            set { _modelData = value; }
        }

        protected PlayingCardGenerator pcg;

        protected override void Awake()
        {
            base.Awake();
            pcg = FindObjectOfType<PlayingCardGenerator>();
        }

        public override void UpdateTextures()
        {
            int n = (int)ModelData.Value;
            rend.materials[1].mainTexture = pcg.faceTextures[n];
        }

    }
}

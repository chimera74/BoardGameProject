using Assets.Scripts.DataModel;

namespace Assets.Scripts.Presentation
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
    }
}

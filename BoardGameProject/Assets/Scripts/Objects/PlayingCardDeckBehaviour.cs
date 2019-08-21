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
    }
}
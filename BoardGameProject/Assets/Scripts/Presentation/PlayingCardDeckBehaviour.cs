namespace Assets.Scripts.Presentation
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
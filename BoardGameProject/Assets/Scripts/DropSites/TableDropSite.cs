using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.DropSites
{
    public class TableDropSite : DropSite
    {

        protected CardGenerator cg;

        public void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
        }

        public override bool DropCard(CardBehaviour droppingCard, Vector3 position)
        {
            droppingCard.cardData.Position = new Vector2(position.x, position.z);
            return true;
        }

        public override bool DropDeck(DeckBehaviour droppingDeck, Vector3 position)
        {
            droppingDeck.transform.parent.position = position;
            return true;
        }
    }
}
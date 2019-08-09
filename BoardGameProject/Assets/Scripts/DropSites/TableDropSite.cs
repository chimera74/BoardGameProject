using Assets.Scripts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
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
            droppingCard.transform.parent.position = position;
            return true;
        }

        public override bool DropDeck(DeckBehaviour droppingDeck, Vector3 position)
        {
            droppingDeck.transform.parent.position = position;
            return true;
        }
    }
}
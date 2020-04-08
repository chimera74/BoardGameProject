using Assets.Scripts.DataModel;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Presentation
{
    public class DeckBehaviour : TSOBehaviour
    {
        public new Deck ModelData
        {
            get { return (Deck)_modelData; }
            set { _modelData = value; }
        }

        protected CardGenerator cg;


        protected override void Awake()
        {
            base.Awake();
            cg = FindObjectOfType<CardGenerator>();
        }

        protected override void OnMouseOver()
        {
            base.OnMouseOver();
            if (Input.GetButtonDown("DebugInfo"))
            {
                DebugPrinter.PrintDeckInfo(ModelData);
            }
        }

        public void SpawnTopCardOnTheRight()
        {
            Card c = ModelData.TakeTopCard();
            Vector3 pos = new Vector3(root.position.x + 1.2f, 0, root.position.z);
            cg.SpawnCard(c, Area.Table, pos);
            if (ModelData.CardCount < 2)
            {
                // Remove deck and replace with one cardData
                Card lastCard = ModelData.TakeTopCard();
                dndm.OnDragStart -= EnableDropSite;
                cg.SpawnCard(lastCard, Area.Table, new Vector3(root.position.x, table.transform.position.y, root.position.z));
                Despawn();
            }
        }

        protected override void OnLeftMouseDoubleClick(PointerEventData pointerEventData)
        {
            SpawnTopCardOnTheRight();
        }

    }
}
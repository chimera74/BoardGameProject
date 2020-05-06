using Assets.Scripts.DataModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Presentation
{
    public class CardInHandBehaviour : ModelContainerBehaviour, IPointerClickHandler
    {
        public new Card ModelData
        {
            get { return (Card) _modelData; }
            set { _modelData = value; }
        }

        protected Renderer rend;
        protected BaseObjectAppearance apprn;
        protected CardInHandAnimation animScr;

        protected override void Awake()
        {
            base.Awake();
            rend = GetComponent<Renderer>();
            animScr = GetComponent<CardInHandAnimation>();
            apprn = GetComponent<BaseObjectAppearance>();
        }

        public void OnPointerClick(PointerEventData data)
        {
            PointerEventData pointerEventData = data as PointerEventData;
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                if (pointerEventData.clickCount > 1)
                    OnLeftMouseDoubleClick(pointerEventData);
            }
            else if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseClick(pointerEventData);
            }
        }

        protected virtual void OnLeftMouseDoubleClick(PointerEventData pointerEventData)
        {
        }

        protected virtual void OnRightMouseClick(PointerEventData pointerEventData)
        {
        }

        protected virtual void OnMouseEnter()
        {
            //animScr.StartHover();
        }

        protected virtual void OnMouseExit()
        {
            //animScr.StopHover();
        }
    }
}
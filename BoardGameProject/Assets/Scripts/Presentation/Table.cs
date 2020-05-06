using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Presentation
{
    public class Table : ModelContainerBehaviour, IPointerClickHandler
    {

        public Collider outsideTableCollider;

        [HideInInspector]
        public Collider tableCollider;

        private WACardGenerator cg;

        protected override void Awake()
        {
            base.Awake();
            tableCollider = GetComponent<Collider>();
        }
        
        protected override void Start()
        {
            base.Start();
            cg = FindObjectOfType<WACardGenerator>();
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

        private void OnLeftMouseDoubleClick(PointerEventData pointerEventData)
        {

        }

        private void OnRightMouseClick(PointerEventData pointerEventData)
        {
            cg.GenerateCard(0, ModelData.uid, pointerEventData.pointerPressRaycast.worldPosition);
        }
    }
}
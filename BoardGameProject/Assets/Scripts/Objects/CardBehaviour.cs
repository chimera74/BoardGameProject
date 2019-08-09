using Assets.Scripts;
using Assets.Scripts.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class CardBehaviour : MonoBehaviour, IPointerClickHandler
    {
        public Card cardData;

        [Header("Dragging Settings")]
        public float hoverHeight = 0.05f;
        public float dragTriggerDelta = 0.05f;

        [Header("Components")]
        public GameObject dropSite;

        [Header("Other")]

        protected Table table;
        protected Transform root;
        protected Renderer rend;
        protected ZIndexManager zim;
        protected DragAndDropManager dndm;



        protected void Awake()
        {
            table = FindObjectOfType<Table>();
            root = transform.parent.GetComponent<Transform>();
            rend = GetComponent<Renderer>();
            zim = FindObjectOfType<ZIndexManager>();
            dndm = FindObjectOfType<DragAndDropManager>();
        }

        protected void Start()
        {
            SetFaceUp(cardData.IsFaceUp);
            zim.StartTracking(this);
            dndm.OnDragStart += EnableDropSite;
            dndm.OnDragStop += DisableDropSite;
            DisableDropSite();
        }

        void Update()
        {

        }

        public void OnDestroy()
        {
            zim.StopTracking(this);
            dndm.OnDragStart -= EnableDropSite;
            dndm.OnDragStop -= DisableDropSite;
        }

        void OnMouseOver()
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                DebugPrinter.PrintCardInfo(cardData);
            }
        }

        void OnMouseDown()
        {
            Drag_OnMouseDown();
        }

        private void OnMouseDrag()
        {
            Drag_OnMouseDrag();
        }

        void OnMouseUp()
        {
            StopDrag();
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
            FlipCard();
        }

        private void OnRightMouseClick(PointerEventData pointerEventData)
        {

        }

        #region Card dragging
        // Card Dragging
        private bool isDragMode;
        private Vector3 dragOffset;
        private Vector3 dragStartPosition;
        private Vector3 dragStartObjPosition;

        private void Drag_OnMouseDown()
        {
            // save offset of click point and object transform position

            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit != null)
            {
                // hitpoint + offset is a new position of an object
                dragStartPosition = hit.Value.point;
                dragStartObjPosition = root.position;
                dragOffset = root.position - dragStartPosition;
            }
        }

        private void Drag_OnMouseDrag()
        {
            // raycast to table
            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit == null)
                return;

            var hitPoint = hit.Value.point;

            if (!isDragMode)
            {
                if (Vector3.Distance(hitPoint, dragStartPosition) > dragTriggerDelta)
                {
                    StartDrag();
                }
            }
            else
            {
                // hitpoint + offset is a new position of an object
                var newPos = hitPoint + dragOffset;
                root.position = new Vector3(newPos.x, newPos.y + hoverHeight, newPos.z);
            }
        }

        private void StartDrag()
        {
            isDragMode = true;
            dndm.TriggerOnDragStart();
            DisableDropSite();
        }

        private void StopDrag()
        {
            if (!isDragMode)
                return;

            var dropPos = new Vector3(root.position.x, table.transform.position.y, root.position.z);
            if (dndm.PutCardAt(this, dropPos, dragStartObjPosition))
            {
                isDragMode = false;
                zim.PutOnTop(this);
                EnableDropSite();
            }
            dndm.TriggerOnDragStop();
        }

        public void EnableDropSite()
        {
            if (!isDragMode)
                dropSite.SetActive(true);
        }

        public void DisableDropSite()
        {
            dropSite.SetActive(false);
        }

        #endregion

        /// <summary>
        /// Set new card's face orientation in the model and adjust visual orientation.
        /// </summary>
        /// <param name="isFaceUp">Face orientation.</param>
        public void SetFaceUp(bool isFaceUp)
        {
            cardData.IsFaceUp = isFaceUp;
            if (isFaceUp)
                PutFaceUp();
            else
                PutFaceDown();
        }

        public void FlipCard()
        {
            SetFaceUp(!cardData.IsFaceUp);
        }

        private void PutFaceUp()
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        private void PutFaceDown()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        public virtual void UpdateTextures()
        {

        }
    }
}
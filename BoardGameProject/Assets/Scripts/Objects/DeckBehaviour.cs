using Assets.Scripts;
using Assets.Scripts.DataModel;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class DeckBehaviour : MonoBehaviour, IPointerClickHandler
    {
        public Deck deckData;

        [Header("Dragging Settings")]
        public float hoverHeight = 0.05f;
        public float dragTriggerDelta = 0.05f;

        [Header("Components")]
        public GameObject dropSite;

        [Header("Other")]
        public int fullDeckCount = 52;

        protected Renderer rend;
        protected CardGenerator cg;
        protected Table table;
        protected Transform root;
        protected DragAndDropManager dndm;

        protected float baseYScale;
        protected float baseYPos;

        public void Awake()
        {
            cg = FindObjectOfType<CardGenerator>();
            table = FindObjectOfType<Table>();
            rend = GetComponent<Renderer>();
            root = transform.parent.GetComponent<Transform>();
            dndm = FindObjectOfType<DragAndDropManager>();

            baseYScale = transform.localScale.y;
            baseYPos = transform.localPosition.y;
        }

        public void Start()
        {
            SetFaceUp(deckData.IsFaceUp);
            dndm.OnDragStart += EnableDropSite;
            dndm.OnDragStop += DisableDropSite;
            DisableDropSite();
        }

        public void Update()
        {
            
        }

        public void OnDestroy()
        {
            dndm.OnDragStart -= EnableDropSite;
            dndm.OnDragStop -= DisableDropSite;
        }

        void OnMouseOver()
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                DebugPrinter.PrintDeckInfo(deckData);
            }
        }

        void OnMouseDown()
        {
            Drag_OnMouseDown();
        }

        void OnMouseUp()
        {
            StopDrag();
        }

        private void OnMouseDrag()
        {
            Drag_OnMouseDrag();
        }

        public void SpawnTopCardOnTheRight()
        {
            Card c = deckData.TakeTopCard();
            Vector3 pos = new Vector3(root.position.x + 1.2f, 0, root.position.z);
            cg.SpawnCard(c, pos);
            if (deckData.CardCount < 2)
            {
                // Remove deck and replace with one card
                Card lastCard = deckData.TakeTopCard();
                dndm.OnDragStart -= EnableDropSite;
                cg.SpawnCard(lastCard, new Vector3(root.position.x, table.transform.position.y, root.position.z));
                Despawn();
            }
            else
            {
                AdjustSize();
                UpdateTextures();
            }
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
                //OnRightMouseClick(pointerEventData);
            }
        }

        private void OnLeftMouseDoubleClick(PointerEventData pointerEventData)
        {
            SpawnTopCardOnTheRight();
        }

        /// <summary>
        /// Set new deck's face orientation in the model and adjust visual orientation.
        /// </summary>
        /// <param name="isFaceUp">Face orientation.</param>
        public void SetFaceUp(bool isFaceUp)
        {
            this.deckData.IsFaceUp = isFaceUp;
            if (isFaceUp)
                PutFaceUp();
            else
                PutFaceDown();
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

        public void AdjustSize()
        {
            if (deckData.CardCount >= fullDeckCount)
            {
                // Full deck
                transform.localScale = new Vector3(transform.localScale.x, baseYScale, transform.localScale.z);
            }
            else
            {
                float yScale = (float)deckData.CardCount / fullDeckCount;
                transform.localScale = new Vector3(transform.localScale.x, baseYScale * yScale, transform.localScale.z);
                transform.localPosition = new Vector3(0, baseYPos * yScale, 0);
            }
        }

        public void Despawn()
        {
            Destroy(root.gameObject);
        }

        #region Deck dragging

        private bool isDragMode;
        private Vector3 offset;
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
                offset = root.position - dragStartPosition;
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
                var newPos = hitPoint + offset;
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
            if (dndm.PutDeckAt(this, dropPos, dragStartObjPosition))
            {
                isDragMode = false;
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
    }
}
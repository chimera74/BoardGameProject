using Assets.Scripts.DataModel;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Objects
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
        protected CardAnimationScript cas;

//        protected SimpleActionQueue _lateUpdateAQ;

        protected void Awake()
        {
//            _lateUpdateAQ = new SimpleActionQueue();
            table = FindObjectOfType<Table>();
            root = transform.parent.GetComponent<Transform>();
            rend = GetComponent<Renderer>();
            zim = FindObjectOfType<ZIndexManager>();
            dndm = FindObjectOfType<DragAndDropManager>();
            cas = GetComponent<CardAnimationScript>();


        }

        protected void Start()
        {
            SetFaceUp(cardData.IsFaceUp);
            zim.StartTracking(this);
            dndm.OnDragStart += EnableDropSite;
            dndm.OnDragStop += DisableDropSite;
            DisableDropSite();
        }

        protected void Update()
        {

        }

        protected void LateUpdate()
        {
//            _lateUpdateAQ.InvokeAll();
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
        private bool _isDragMode;
        private Vector3 _dragOffset;
        private Vector3 _dragStartPosition;
        private Vector3 _dragStartObjPosition;

        private void Drag_OnMouseDown()
        {
            // save offset of click point and object transform position

            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit != null)
            {
                // hitpoint + offset is a new position of an object
                _dragStartPosition = hit.Value.point;
                _dragStartObjPosition = root.position;
                _dragOffset = root.position - _dragStartPosition;
            }
        }

        private void Drag_OnMouseDrag()
        {
            // raycast to table
            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit == null)
                return;

            var hitPoint = hit.Value.point;

            if (!_isDragMode)
            {
                if (Vector3.Distance(hitPoint, _dragStartPosition) > dragTriggerDelta)
                {
                    cas.targetPosition = new Vector3(hitPoint.x, root.position.y, hitPoint.z);
                    StartDrag();
                }
            }
            else
            {
                cas.targetPosition = new Vector3(hitPoint.x, root.position.y, hitPoint.z);
            }
        }

        private void StartDrag()
        {
            _isDragMode = true;
            dndm.TriggerOnDragStart();
            DisableDropSite();
            cas.StartHover();
            cas.StartCursorFollow();
        }

        private void StopDrag()
        {
            if (!_isDragMode)
                return;

            // raycast to table
            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit == null)
                return;

            var hitPoint = hit.Value.point;

            var dropPos = new Vector3(hitPoint.x, table.transform.position.y, hitPoint.z);
            if (dndm.PutCardAt(this, dropPos, _dragStartObjPosition))
            {
                _isDragMode = false;
                zim.PutOnTop(this);
                cas.MoveToModelPosition();
                EnableDropSite();
            }
            dndm.TriggerOnDragStop();
            cas.StopHover();
            cas.StopCursorFollow();
        }

        public void EnableDropSite()
        {
            if (!_isDragMode)
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
            cas.Flip();
        }

        public void FlipCard()
        {
            SetFaceUp(!cardData.IsFaceUp);
        }

        public virtual void UpdateTextures()
        {

        }

        #region Animations



        #endregion
    }
}
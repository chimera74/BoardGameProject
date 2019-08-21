using Assets.Scripts.DataModel;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Objects
{
    public class BaseObjectBehaviour : MonoBehaviour, IPointerClickHandler
    {
        protected BaseObject _modelData;

        public BaseObject ModelData
        {
            get { return _modelData; }
            set { _modelData = value; }
        }

        [Header("Dragging Settings")]
        public float dragTriggerDelta = 0.05f;

        [Header("Components")]
        public GameObject dropSite;

        [Header("Other")]
        protected Table table;
        protected Transform root;
        protected Renderer rend;
        protected DragAndDropManager dndm;
        protected BaseObjectAnimation animScr;
        protected BaseObjectAppearance apprn;

        protected virtual void Awake()
        {
            table = FindObjectOfType<Table>();
            root = transform.parent.GetComponent<Transform>();
            rend = GetComponent<Renderer>();
            dndm = FindObjectOfType<DragAndDropManager>();
            animScr = GetComponent<BaseObjectAnimation>();
            apprn = GetComponent<BaseObjectAppearance>();
        }

        protected virtual void Start()
        {
            dndm.OnDragStart += EnableDropSite;
            dndm.OnDragStop += DisableDropSite;
            DisableDropSite();
            apprn.UpdateAppearance();
        }

        protected virtual void Update()
        {
        }

        protected virtual void LateUpdate()
        {
        }

        protected virtual void OnDestroy()
        {
            dndm.OnDragStart -= EnableDropSite;
            dndm.OnDragStop -= DisableDropSite;
        }

        protected virtual void OnMouseOver()
        {
        }

        protected virtual void OnMouseDown()
        {
            Drag_OnMouseDown();
        }

        protected virtual void OnMouseDrag()
        {
            Drag_OnMouseDrag();
        }

        protected virtual void OnMouseUp()
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

        protected virtual void OnLeftMouseDoubleClick(PointerEventData pointerEventData)
        {
        }

        protected virtual void OnRightMouseClick(PointerEventData pointerEventData)
        {
        }

        public virtual void Despawn()
        {
            Destroy(root.gameObject);
        }

        #region Object dragging

        protected bool _isDragMode;
        protected Vector3 _dragStartPosition;
        protected Vector3 _dragStartObjPosition;

        protected virtual void Drag_OnMouseDown()
        {
            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit != null)
            {
                _dragStartPosition = hit.Value.point;
                _dragStartObjPosition = root.position;
            }
        }

        protected virtual void Drag_OnMouseDrag()
        {
            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit == null)
                return;

            var hitPoint = hit.Value.point;

            if (!_isDragMode)
            {
                if (Vector3.Distance(hitPoint, _dragStartPosition) > dragTriggerDelta)
                {
                    animScr.targetPosition = new Vector3(hitPoint.x, root.position.y, hitPoint.z);
                    StartDrag();
                }
            }
            else
            {
                animScr.targetPosition = new Vector3(hitPoint.x, root.position.y, hitPoint.z);
            }
        }

        protected virtual void StartDrag()
        {
            _isDragMode = true;
            dndm.TriggerOnDragStart();
            DisableDropSite();
            animScr.StartHover();
            animScr.StartCursorFollow();
        }

        protected virtual void StopDrag()
        {
            if (!_isDragMode)
                return;

            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(table.outsideTableCollider);
            if (hit == null)
                return;

            var hitPoint = hit.Value.point;

            var dropPos = new Vector3(hitPoint.x, table.transform.position.y, hitPoint.z);
            if (!dndm.PutAt(this, dropPos))
                animScr.MoveToModelPosition();
            _isDragMode = false;
            dndm.TriggerOnDragStop();
            animScr.StopHover();
            animScr.StopCursorFollow();
        }

        public void EnableDropSite()
        {
            if (!_isDragMode)
                dropSite?.SetActive(true);
        }

        public void DisableDropSite()
        {
            dropSite?.SetActive(false);
        }

        #endregion
    }
}
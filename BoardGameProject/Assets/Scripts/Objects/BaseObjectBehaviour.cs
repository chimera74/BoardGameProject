using System.Numerics;
using Assets.Scripts.DataModel;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Objects
{
    public class BaseObjectBehaviour : ModelContainerBehaviour, IPointerClickHandler
    {
        [Header("Dragging Settings")]
        public float dragTriggerDelta = 0.05f;

        [Header("Components")]
        public GameObject dropSite;

        [Header("Other")]
        protected Table table;

        protected Collider handPlaneCollider;
        protected Transform root;
        protected Renderer rend;
        protected DragAndDropManager dndm;
        protected BaseObjectAnimation animScr;
        protected BaseObjectAppearance apprn;
        protected Collider coll;

        protected virtual void Awake()
        {
            table = FindObjectOfType<Table>();
            root = transform.parent.GetComponent<Transform>();
            rend = GetComponent<Renderer>();
            dndm = FindObjectOfType<DragAndDropManager>();
            animScr = GetComponent<BaseObjectAnimation>();
            apprn = GetComponent<BaseObjectAppearance>();
            handPlaneCollider = GameObject.Find("HandPlane").GetComponent<Collider>();
            coll = GetComponent<Collider>();

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
        protected bool _isInHandPlane;

        protected virtual void Drag_OnMouseDown()
        {
            RaycastHit? hit = RaycastingHelper.instance.RaycastCursorFromBothCamerasTo(coll);
            if (hit != null)
            {
                _dragStartPosition = hit.Value.point;
                _dragStartObjPosition = root.position;
            }
        }

        protected virtual void Drag_OnMouseDrag()
        {
            bool isSwitching = false;
            RaycastHit? handPlaneHit = RaycastingHelper.instance.RaycastCursorFromHandCameraTo(handPlaneCollider);
            Vector3 hitPoint;
            if (handPlaneHit != null)
            {
                hitPoint = handPlaneHit.Value.point;
                if (!_isInHandPlane)
                    isSwitching = true;
                _isInHandPlane = true;
            }
            else
            {
                RaycastHit? hit = RaycastingHelper.instance.RaycastCursorTo(table.tableCollider);
                if (hit == null)
                    return;
                hitPoint = hit.Value.point;
                if (_isInHandPlane)
                    isSwitching = true;
                _isInHandPlane = false;
            }

            if (!_isDragMode)
            {
                RaycastHit? hit = RaycastingHelper.instance.RaycastCursorFromBothCamerasTo(coll);

                if (hit == null || Vector3.Distance(hit.Value.point, _dragStartPosition) > dragTriggerDelta)
                {
                    animScr.targetPosition = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);
                    StartDrag();
                }
            }
            else
            {
                var newPos = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);
                if (isSwitching)
                {
                    root.position = newPos;
                    root.SetParent(_isInHandPlane ? handPlaneCollider.transform : null);
                }

                animScr.targetPosition = newPos;
            }
        }

        protected virtual void StartDrag()
        {
            _isDragMode = true;
            _isInHandPlane = ModelData.Area == Area.Hand;
            dndm.TriggerOnDragStart();
            DisableDropSite();
            animScr.StartHover();
            animScr.StartCursorFollow();
        }

        protected virtual void StopDrag()
        {
            if (!_isDragMode)
                return;

            Area area;
            RaycastHit? handPlaneHit = RaycastingHelper.instance.RaycastCursorFromHandCameraTo(handPlaneCollider);
            Vector3 hitPoint;
            if (handPlaneHit != null)
            {
                hitPoint = handPlaneHit.Value.point;
                area = Area.Hand;
            }
            else
            {
                RaycastHit? tableHit = RaycastingHelper.instance.RaycastCursorTo(table.tableCollider);
                if (tableHit != null)
                {
                    hitPoint = tableHit.Value.point;
                    area = Area.Table;
                }
                else
                {
                    animScr.MoveToModelPosition();
                    return;
                }
            }

            var dropPos = new Vector3(hitPoint.x, table.transform.position.y, hitPoint.z);
            if (!dndm.PutAt(this, area, dropPos))
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
using System;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Objects
{

    public enum MoveType
    {
        None,
        Snap,
        CursorFollow
    }

    public class BaseObjectAnimation : MonoBehaviour
    {
        [Header("Dragging")]
        public float cursorFollowSpeed = 0.4f;
        public float minSpeed = 0.005f;
        [Header("Tilting while drag")]
        public float tiltPower = 5.0f;
        public float maxTiltAngle = 30.0f;
        [Header("Snapping to place")]
        public float snapSpeed = 1.0f;

        [HideInInspector]
        public Vector3 targetPosition;

        protected MoveType movementType = MoveType.None;

        protected BaseObjectBehaviour beh;
        protected Animator anim;
        protected Table table;
        protected Transform root;
        protected Transform handPlane;
        
        protected SimpleActionQueue afterSnapActions;

        protected bool allowTilt = true;

        private BaseObject _model;

        protected virtual void Awake()
        {
            afterSnapActions = new SimpleActionQueue();
            root = transform.parent;
            anim = GetComponent<Animator>();
            table = FindObjectOfType<Table>();
            beh = GetComponent<BaseObjectBehaviour>();
            handPlane = GameObject.Find("HandPlane").transform;
        }

        protected virtual void Start()
        {
            _model = beh.ModelData;
            _model.OnPositionChanged += MoveToModelPosition;
        }

        protected virtual void OnDestroy()
        {
            if (_model != null)
                _model.OnPositionChanged -= MoveToModelPosition;
        }

        protected virtual void Update()
        {
            ProcessSnap();
            ProcessCursorFollow();
        }

        protected virtual void LateUpdate()
        {
            ProcessTilt();
        }

        protected virtual void ProcessSnap()
        {
            if (movementType == MoveType.Snap)
            {
                var direction = (targetPosition - root.position).normalized;
                var delta = direction * snapSpeed;

                if ((direction.magnitude < 0.001f) || (targetPosition - root.position).magnitude < delta.magnitude)
                {
                    root.position = targetPosition;
                    movementType = MoveType.None;
                    afterSnapActions.InvokeAll();
                }
                else
                {
                    root.position += delta;
                }
            }
        }

        public void AddAfterSnapAction(Action action)
        {
            afterSnapActions.Enqueue(action);
        }

        protected virtual void ProcessCursorFollow()
        {
            if (movementType == MoveType.CursorFollow)
            {
                var speed = cursorFollowSpeed * (targetPosition - root.position).magnitude;
                if (speed < minSpeed)
                    speed = minSpeed;
                var direction = (targetPosition - root.position).normalized;
                var delta = direction * speed;

                if ((targetPosition - root.position).magnitude < 0.005)
                {
                    root.position = targetPosition;
                }
                else
                {
                    root.position += delta;
                }
            }
        }

        protected virtual void TiltCorrection(ref Vector3 direction)
        {
            direction.z = -direction.z;
        }

        protected virtual void ProcessTilt()
        {

            if (allowTilt && movementType == MoveType.CursorFollow)
            {
                var tiltAngle = tiltPower * (targetPosition - root.position).magnitude;
                var direction = (targetPosition - root.position).normalized;
                TiltCorrection(ref direction);

                if (tiltAngle > maxTiltAngle)
                    tiltAngle = maxTiltAngle;

                var axis = Vector3.Cross(direction, transform.up);

                ResetRotation();
                transform.Rotate(axis, tiltAngle);
            }
        }

        public virtual void MoveToModelPosition()
        {
            MoveToModelCameraSpace();
            float yPos = _model.Area == Area.Table ? table.transform.position.y : handPlane.position.y;
            targetPosition = new Vector3(_model.Position.x, yPos, _model.Position.y);
            movementType = MoveType.Snap;
        }

        /// <summary>
        /// Moves object to other camera space if needed.
        /// </summary>
        private void MoveToModelCameraSpace()
        {
            // if current camera space does not match model - fix
            if ((root.parent == null) != (_model.Area == Area.Table))
            {
                // get current camera
                Camera currentCamera;
                Camera otherCamera;
                if (root.parent == null)
                {
                    currentCamera = RaycastingHelper.instance.mainCamera;
                    otherCamera = RaycastingHelper.instance.handCamera;
                }
                else
                {
                    currentCamera = RaycastingHelper.instance.handCamera;
                    otherCamera = RaycastingHelper.instance.mainCamera;
                }

                // get relative position to current camera
                var relPos = currentCamera.WorldToViewportPoint(root.position);

                // calculate absolute position within other camera
                var newPos = otherCamera.ViewportToWorldPoint(relPos);

                // move
                root.position = newPos;
                
                root.SetParent((_model.Area == Area.Table) ? null : handPlane);
            }
        }

        public virtual void StartCursorFollow()
        {
            movementType = MoveType.CursorFollow;
        }

        public virtual void StopCursorFollow()
        {
            if (movementType == MoveType.CursorFollow)
                movementType = MoveType.None;
            ResetRotation();
        }

        public virtual void StartHover()
        {
            anim.SetBool("IsHovering", true);
        }

        public virtual void StopHover()
        {
            anim.SetBool("IsHovering", false);
        }

        protected virtual void ResetRotation()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
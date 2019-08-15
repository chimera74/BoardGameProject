using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Objects
{
    public class CardAnimationScript : MonoBehaviour
    {
        public float snapSpeed = 1.0f;
        public float cursorFollowSpeed = 0.4f;
        public float minSpeed = 0.005f;
        public float tiltPower = 5.0f;
        public float maxTiltAngle = 30.0f;
        public float flipSpeed = 10.0f;
        public float flipHeight = 1.0f; // should be at least half width of the card

        public AnimationCurve testAC;

        public MoveType movementType = MoveType.None;

        protected Animator anim;
        protected CardBehaviour cb;
        protected Table table;
        protected Transform root;

        [HideInInspector] public Vector3 targetPosition;

        private Quaternion _endFlipRot;
        private bool _isFlipping = false;

        protected readonly Quaternion FACE_UP_ROTATION = Quaternion.Euler(0, 0, 180);
        protected readonly Quaternion FACE_DOWN_ROTATION = Quaternion.Euler(0, 0, 0);

        protected void Awake()
        {
            root = transform.parent;
            anim = GetComponent<Animator>();
            cb = GetComponent<CardBehaviour>();
            table = FindObjectOfType<Table>();
        }

        protected void Start()
        {
            cb.cardData.OnPositionChanged += MoveToModelPosition;
            cb.cardData.OnFaceUpChanged += Flip;
        }

        protected void OnDestroy()
        {
            cb.cardData.OnPositionChanged -= MoveToModelPosition;
            cb.cardData.OnFaceUpChanged -= Flip;
        }

        protected void Update()
        {
            ProcessSnap();
            ProcessCursorFollow();
        }

        protected void LateUpdate()
        {
            ProcessFlip();
            ProcessTilt();
        }

        private void ProcessSnap()
        {
            if (movementType == MoveType.Snap)
            {
                var direction = (targetPosition - root.position).normalized;
                var delta = direction * snapSpeed;

                if ((targetPosition - root.position).magnitude < delta.magnitude)
                {
                    root.position = targetPosition;
                    movementType = MoveType.None;
                }
                else
                {
                    root.position += delta;
                }
            }
        }

        private void ProcessCursorFollow()
        {
            if (movementType == MoveType.CursorFollow)
            {
                var speed = cursorFollowSpeed * (targetPosition - root.position).magnitude;
                if (speed < minSpeed)
                    speed = minSpeed;
                var direction = (targetPosition - root.position).normalized;
                var delta = direction * speed;

                if ((targetPosition - root.position).magnitude < delta.magnitude)
                {
                    root.position = targetPosition;
                }
                else
                {
                    root.position += delta;
                }
            }
        }

        private void ProcessTilt()
        {
            if (_isFlipping)
                return;

            if (movementType == MoveType.CursorFollow)
            {
                var tiltAngle = tiltPower * (targetPosition - root.position).magnitude;
                var direction = (targetPosition - root.position).normalized;
                direction.z = -direction.z;
                if (!cb.cardData.IsFaceUp)
                    direction.x = -direction.x;

                if (tiltAngle > maxTiltAngle)
                    tiltAngle = maxTiltAngle;

                var axis = Vector3.Cross(direction, transform.up);

                SetFaceUpToModel();
                transform.Rotate(axis, tiltAngle);
            }
        }

        public void MoveToModelPosition()
        {
            targetPosition = new Vector3(cb.cardData.Position.x, root.position.y, cb.cardData.Position.y);
            movementType = MoveType.Snap;
        }

        public void StartCursorFollow()
        {
            movementType = MoveType.CursorFollow;
        }

        public void StopCursorFollow()
        {
            if (movementType == MoveType.CursorFollow)
                movementType = MoveType.None;
            SetFaceUpToModel();
        }

        public void StartHover()
        {
            anim.SetBool("IsHovering", true);
        }

        public void StopHover()
        {
            anim.SetBool("IsHovering", false);
        }

        private void SetFaceUpToModel()
        {
            if (cb.cardData.IsFaceUp)
                SetFaceUp();
            else
                SetFaceDown();
        }

        private void SetFaceUp()
        {
            transform.rotation = FACE_UP_ROTATION;
        }

        private void SetFaceDown()
        {
            transform.rotation = FACE_DOWN_ROTATION;
        }

        public void Flip()
        {
            _endFlipRot = cb.cardData.IsFaceUp ? FACE_UP_ROTATION : FACE_DOWN_ROTATION;
            _isFlipping = true;
        }

        private void ProcessFlip_Update()
        {

        }

        private void ProcessFlip()
        {
            if (!_isFlipping)
                return;

            if (transform.rotation != _endFlipRot)
            {
                var step = flipSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _endFlipRot, step);

                // rise and lower the card during flip
                float startHeight = transform.position.y;
                float maxHeight = flipHeight > startHeight ? flipHeight : startHeight;

                var devAngle = Quaternion.Angle(transform.rotation, FACE_DOWN_ROTATION);
                var t = Mathf.Pow(devAngle - 90, 2) / -8100 + 1; // Parabola
                float height = Mathf.Lerp(startHeight, maxHeight, t);
                transform.position = new Vector3(transform.position.x, height, transform.position.z);
            }
            else
            {
                _isFlipping = false;
                SetFaceUpToModel();
            }
        }
    }

    
    public enum MoveType
    {
        None,
        Snap,
        CursorFollow
    }
}
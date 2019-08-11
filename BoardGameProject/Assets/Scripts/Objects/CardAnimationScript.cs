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

        public MoveType movementType = MoveType.None;

        protected Animator anim;
        protected CardBehaviour cb;
        protected Table table;
        protected Transform root;

        public Vector3 targetPosition;
        public float tiltAngle;

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
        }

        protected void OnDestroy()
        {
            cb.cardData.OnPositionChanged -= MoveToModelPosition;
        }

        protected void Update()
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

        protected void LateUpdate()
        {
            ProcessTilt();
        }

        public void ProcessTilt()
        {
            if (movementType == MoveType.CursorFollow)
            {
                tiltAngle = tiltPower * (targetPosition - root.position).magnitude;
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

        public void SetFaceUpToModel()
        {
            if (cb.cardData.IsFaceUp)
                SetFaceUp();
            else
                SetFaceDown();
        }

        private void SetFaceUp()
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        private void SetFaceDown()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public enum MoveType
    {
        None,
        Snap,
        CursorFollow
    }
}
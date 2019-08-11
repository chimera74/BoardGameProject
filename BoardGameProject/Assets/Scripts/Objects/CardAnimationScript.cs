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

        public MoveType movementType = MoveType.None;

        protected Animator anim;
        protected CardBehaviour cb;
        protected Table table;
        protected Transform root;
            
        public Vector3 targetPosition;

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

        public void MoveToModelPosition()
        {
            // Define a target position above and behind the target transform
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
        }

        public void StartHover()
        {
            anim.SetBool("IsHovering", true);
        }

        public void StopHover()
        {
            anim.SetBool("IsHovering", false);
        }

        
    }

    public enum MoveType
    {
        None,
        Snap,
        CursorFollow
    }
}

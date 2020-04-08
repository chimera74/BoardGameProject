using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class CardInHandAnimation : MonoBehaviour
    {
        [Header("Hovering")]
        public float hoverYPos = 0.85f;
        public float hoverZPos = -0.01f;


        protected CardInHandBehaviour beh;
        protected Transform root;
        protected HandBehaviour hand;
        protected Animator anim;

        private BaseObject _model;

        protected virtual void Awake()
        {
            root = transform.parent;
            anim = GetComponent<Animator>();
            hand = FindObjectOfType<HandBehaviour>();
            beh = GetComponent<CardInHandBehaviour>();
        }

        protected virtual void Start()
        {
            _model = beh.ModelData;
        }

        protected virtual void Update()
        {

        }

        public void StartHover()
        {
            // if sitting in hand

            // move to hover position
            root.localPosition = new Vector3(root.localPosition.x, hoverYPos, hoverZPos);

            // start hover animation
            anim.SetBool("IsHovering", true);
            
        }

        public void StopHover()
        {
            // move to original pos
            root.localPosition = hand.GetCardIdlePosition(beh);

            anim.SetBool("IsHovering", false);
        }
    }
}
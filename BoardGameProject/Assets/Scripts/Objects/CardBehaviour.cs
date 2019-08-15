using Assets.Scripts.DataModel;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Objects
{
    public class CardBehaviour : TSOBehaviour
    {
        public new Card ModelData
        {
            get { return (Card) _modelData; }
            set { _modelData = value; }
        }

        protected ZIndexManager zim;


        protected override void Awake()
        {
            base.Awake();
            zim = FindObjectOfType<ZIndexManager>();
        }

        protected override void Start()
        {
            base.Start();
            zim.StartTracking(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            zim.StopTracking(this);
        }

        protected override void OnMouseOver()
        {
            base.OnMouseOver();
            if (Input.GetButtonDown("DebugInfo"))
            {
                DebugPrinter.PrintCardInfo(ModelData);
            }
        }

        protected override void StopDrag()
        {
            base.StopDrag();
            zim.PutOnTop(this);
        }
    }
}
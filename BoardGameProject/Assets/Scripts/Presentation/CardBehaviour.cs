using System;
using Assets.Scripts.DataModel;
using Assets.Scripts.Debug;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class CardBehaviour : TSOBehaviour
    {
        protected override Type ModelType => typeof(Card);
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
            animScr.AddAfterSnapAction(() =>
            {
                zim.PutOnTop(this);
            });
            
        }
    }
}
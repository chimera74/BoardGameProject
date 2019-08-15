using Assets.Scripts.DataModel;
using Assets.Scripts.Debug;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Objects
{
    public class TSOBehaviour : BaseObjectBehaviour
    {

        public new TwoSidedObject ModelData
        {
            get { return (TwoSidedObject) _modelData; }
            set { _modelData = value; }
        }

        protected override void Start()
        {
            base.Start();
            SetFaceUp(ModelData.IsFaceUp);
        }

        protected override void OnMouseOver()
        {
            base.OnMouseOver();
            if (!_isDragMode && Input.GetButtonDown("Flip"))
            {
                Flip();
            }
        }

        protected override void Drag_OnMouseDrag()
        {
            base.Drag_OnMouseDrag();
            if (_isDragMode && Input.GetButtonDown("Flip"))
            {
                Flip();
            }
        }

        /// <summary>
        /// Set new object's face orientation in the model.
        /// </summary>
        /// <param name="isFaceUp">Face orientation.</param>
        public void SetFaceUp(bool isFaceUp)
        {
            ModelData.IsFaceUp = isFaceUp;
        }

        public void Flip()
        {
            SetFaceUp(!ModelData.IsFaceUp);
        }
    }
}
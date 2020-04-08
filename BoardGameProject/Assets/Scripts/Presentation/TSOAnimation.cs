using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class TSOAnimation : BaseObjectAnimation
    {
        [Header("Flipping")]
        public float flipSpeed = 10.0f;
        public float flipHeight = 1.0f; // should be at least half width of the cardData

        private TwoSidedObject _model;
        private Quaternion _endFlipRot;
        private bool _isFlipping = false;

        protected readonly Quaternion FACE_UP_ROTATION = Quaternion.Euler(0, 0, 180);
        protected readonly Quaternion FACE_DOWN_ROTATION = Quaternion.Euler(0, 0, 0);

        protected override void Awake()
        {
            base.Awake();
            beh = beh as TSOBehaviour;
        }

        protected override void Start()
        {
            base.Start();
            _model = beh.ModelData as TwoSidedObject;
            _model.OnFaceUpChanged += Flip;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_model != null)
                _model.OnFaceUpChanged -= Flip;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            ProcessFlip();
        }

        protected override void ResetRotation()
        {
            SetFaceUpToModel();
        }

        private void SetFaceUpToModel()
        {
            if (_model.IsFaceUp)
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

        protected override void TiltCorrection(ref Vector3 direction)
        {
            base.TiltCorrection(ref direction);
            if (!_model.IsFaceUp)
                direction.x = -direction.x;
        }

        public void Flip()
        {
            _endFlipRot = _model.IsFaceUp ? FACE_UP_ROTATION : FACE_DOWN_ROTATION;
            _isFlipping = true;
            allowTilt = false;
        }

        private void ProcessFlip()
        {
            if (!_isFlipping)
                return;

            if (transform.rotation != _endFlipRot)
            {
                var step = flipSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _endFlipRot, step);

                // rise and lower the cardData during flip
                float startHeight = transform.position.y;
                float maxHeight = (flipHeight > startHeight) ? flipHeight : startHeight;

                var devAngle = Quaternion.Angle(transform.rotation, FACE_DOWN_ROTATION);
                var t = Mathf.Pow(devAngle - 90, 2) / -8100 + 1; // Parabola
                float height = Mathf.Lerp(startHeight, maxHeight, t);
                transform.position = new Vector3(transform.position.x, height, transform.position.z);
            }
            else
            {
                _isFlipping = false;
                allowTilt = true;
                SetFaceUpToModel();
            }
        }
    }
}
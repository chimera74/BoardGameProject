using System;
using System.Diagnostics;
using Assets.Scripts;
using Assets.Scripts.Presentation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class CameraDragHandle : MonoBehaviour, IPointerDownHandler
    {

        public float dragCoefficient = 0.01f;

        protected Collider outsideTableCollider;
        protected Camera mainCamera;

        protected Vector3 dragStartPosition;
        protected Vector3 cameraStartPosition;
        protected CameraControls cc;

        protected bool isMoving = false;

        protected void Awake()
        {
            mainCamera = Camera.main;
            outsideTableCollider = FindObjectOfType<Table>().outsideTableCollider;
            cc = FindObjectOfType<CameraControls>();
        }

        public void OnPointerDown(PointerEventData data)
        {
            RaycastHit? hit = RaycastingHelper.instance.RaycastCursorTo(outsideTableCollider);
            if (hit != null)
            {
                dragStartPosition = Input.mousePosition;
                cameraStartPosition = mainCamera.transform.position;
                isMoving = true;
            }
        }

        protected void OnMouseDrag()
        {
            if (isMoving)
            {
                var offset = dragStartPosition - Input.mousePosition;
                offset.z = offset.y;
                offset.y = 0;
                var targetCameraPos = cameraStartPosition + (offset * dragCoefficient);
                cc.SmoothMoveCamera(targetCameraPos);
            }
        }

        protected void OnMouseUp()
        {
            isMoving = false;
        }
    }
}
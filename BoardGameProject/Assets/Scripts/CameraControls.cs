using System;
using IngameDebugConsole;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraControls : MonoBehaviour
    {

        public Vector3 defaultCameraPosition;
        public float defaultHeight;
        public float defaultXOffset;
        public float defaultZOffset;

        public float zoomSpd = 2.0f;
        public float xSpeed = 5.0f;
        public float ySpeed = 5.0f;

        public float mouseSensitivity = 1.0f;

        public float minCameraDistance = 6.0f;
        public float maxCameraDistance = 10.0f;

        public bool enableDirectionalButtonsPanning = true;

        [Header("Camera smooth")]
        public float minSpeed = 0.005f;
        public float targetFollowSpeed = 0.5f;

        protected Vector3 lastPosition;
        protected bool isMoving;
        protected Vector3 targetCameraPos;

        protected DebugLogManager debugConsole;
        protected Camera cam;

        protected void Awake()
        {
            transform.position = defaultCameraPosition;
            targetCameraPos = defaultCameraPosition;
            cam = GetComponent<Camera>();
            debugConsole = FindObjectOfType<DebugLogManager>();
        }

        protected void LateUpdate()
        {
            ProcessKeyControls();
            ProcessCameraZoom();
            ProcessCameraSmoothMove();
        }

        protected void ProcessMouseControls()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = -(Input.mousePosition - lastPosition);
                transform.Translate(delta.x * mouseSensitivity, 0, delta.y * mouseSensitivity, Space.World);
                lastPosition = Input.mousePosition;
            }
        }

        protected void ProcessKeyControls()
        {

            if (debugConsole.IsOpen())
                return;

            if (!enableDirectionalButtonsPanning)
                return;

            float dx = Input.GetAxis("Horizontal") * xSpeed * 0.02f;
            float dz = Input.GetAxis("Vertical") * ySpeed * 0.02f;

            var dVector = new Vector3(dx, 0, dz);
            if (dVector.magnitude < 0.005f)
                return;

            Vector3 position = transform.position + dVector;

            SmoothMoveCamera(position);
        }

        public void LookAt(Vector3 lookPos)
        {
            Vector3 newPosition = lookPos;
            newPosition.y += defaultHeight;
            newPosition.x += defaultXOffset;
            newPosition.z += defaultZOffset;

            transform.position = newPosition;
            transform.LookAt(lookPos);
        }

        protected void ProcessCameraSmoothMove()
        {
            if (!isMoving)
                return;

            var speed = targetFollowSpeed * (targetCameraPos - transform.position).magnitude;
            if (speed < minSpeed)
                speed = minSpeed;
            var direction = (targetCameraPos - transform.position).normalized;
            var delta = direction * speed;

            if (speed < 0.001f || (targetCameraPos - transform.position).magnitude < 0.005)
            {
                transform.position = targetCameraPos;
                isMoving = false;
            }
            else
            {
                transform.position += delta;
            }
        }

        protected void ProcessCameraZoom()
        {
            var delta = Input.GetAxis("Mouse ScrollWheel") * zoomSpd;
            var newSize = cam.orthographicSize + delta;
            if (newSize < minCameraDistance)
                newSize = minCameraDistance;
            if (newSize > maxCameraDistance)
                newSize = maxCameraDistance;
            cam.orthographicSize = newSize;
        }

        public void SmoothMoveCamera(Vector3 target)
        {
            targetCameraPos = target;
            isMoving = true;
        }

        public void SetCameraPos(Vector3 target)
        {
            isMoving = false;
            transform.position = target;
        }
    }
}
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraControls : MonoBehaviour
    {

        public float defaultHeight;
        public float defaultXOffset;
        public float defaultZOffset;

        public float zoomSpd = 2.0f;
        public float xSpeed = 5.0f;
        public float ySpeed = 5.0f;

        public float mouseSensitivity = 1.0f;

        public float minCameraHeight = 5.0f;
        public float maxCameraHeight = 15.0f;

        private Vector3 lastPosition;

        public bool enableDirectionalButtonsPanning = true;

        public void Start()
        {
            transform.position = new Vector3(defaultXOffset, defaultHeight, defaultZOffset);
        }

        public void LateUpdate()
        {
            ProcessKeyControls();
            //ProcessMouseControls();
        }

        private void ProcessMouseControls()
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

        private void ProcessKeyControls()
        {

            if (!enableDirectionalButtonsPanning)
                return;

            float dx = Input.GetAxis("Horizontal") * xSpeed * 0.02f;
            float dz = Input.GetAxis("Vertical") * ySpeed * 0.02f;

            float dy = Input.GetAxis("Mouse ScrollWheel") * zoomSpd;

            //print(Input.GetAxis("Mouse ScrollWheel"));

            Vector3 position = transform.position;
            position.x += dx;
            position.z += dz;
            position.y += dy;

            if (position.y > maxCameraHeight)
                position.y = maxCameraHeight;
            if (position.y < minCameraHeight)
                position.y = minCameraHeight;

            transform.position = position;
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
    }
}
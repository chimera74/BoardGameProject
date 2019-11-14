using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class RaycastingHelper : MonoBehaviour
    {

        public static RaycastingHelper instance;
        public Camera mainCamera;
        public Camera handCamera;

        public RaycastHit? RaycastCursorTo(Collider targetObjectCollider)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            targetObjectCollider.Raycast(ray, out var hit, Mathf.Infinity);
            if (hit.transform == null)
                return null;
            return hit;
        }

        protected void Awake()
        {
            instance = this;
        }

        public RaycastHit? RaycastCursorFromHandCameraTo(Collider targetObjectCollider)
        {
            Ray ray = handCamera.ScreenPointToRay(Input.mousePosition);
            targetObjectCollider.Raycast(ray, out var hit, Mathf.Infinity);
            if (hit.transform == null)
                return null;
            return hit;
        }

        public static bool RaycastToTableDropZones(out RaycastHit hit, Vector3 pos)
        {
            int layerMask = 1 << 9; // DropZone layer
            Ray ray = new Ray(new Vector3(pos.x, 0.3f, pos.z), Vector3.down);
            return Physics.Raycast(ray, out hit, 1.0f, layerMask);
        }

        public static bool RaycastToHandDropZones(out RaycastHit hit, Vector3 pos, Vector3 handPos)
        {
            int layerMask = 1 << 9; // DropZone layer
            Ray ray = new Ray(new Vector3(pos.x, handPos.y + 0.3f, pos.z), Vector3.down);
            return Physics.Raycast(ray, out hit, 1.0f, layerMask);
        }
    }
}

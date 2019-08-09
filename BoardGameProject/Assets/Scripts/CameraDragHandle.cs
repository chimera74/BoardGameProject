using Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraDragHandle : MonoBehaviour
    {
        private Collider outsideTableCollider;

        // Use this for initialization
        void Start()
        {
            outsideTableCollider = FindObjectOfType<Table>().outsideTableCollider;
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Camera dragging (actually table dragging)
        Vector3 offset;
        Vector3 dragStartPosition;

        void OnMouseDown()
        {

            // save offset of click point and object transform position

            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(outsideTableCollider);
            if (hit != null)
            {
                dragStartPosition = hit.Value.point;
                offset = transform.position - dragStartPosition;
            }
        }

        private void OnMouseDrag()
        {
            // raycast to underlaying plane
            RaycastHit? hit = RaycastingHelper.RaycastCursorTo(outsideTableCollider);
            if (hit == null)
                return;

            // hitpoint + offset is a new position of an object
            var newPos = hit.Value.point + offset;
            transform.position = newPos;
        }
    }
}
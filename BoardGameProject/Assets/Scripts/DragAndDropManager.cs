using System;
using Assets.Scripts.DropZones;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts
{
    public class DragAndDropManager : MonoBehaviour
    {
        public event Action OnDragStart;
        public event Action OnDragStop;

        public void TriggerOnDragStart()
        {
            OnDragStart?.Invoke();
        }

        public void TriggerOnDragStop()
        {
            OnDragStop?.Invoke();
        }

        /// <summary>
        /// Tries to put object at the spot.
        /// </summary>
        /// <returns>
        /// True if object was accepted, otherwise - false.
        /// </returns>
        public bool PutAt(BaseObjectBehaviour obj, Vector3 pos)
        {
            TriggerOnDragStart();
            bool res = RaycastingHelper.RaycastToDropZones(out var hit, pos);
            TriggerOnDragStop();

            if (res)
            {
                DropZone dz = hit.transform.GetComponent<DropZone>();
                return dz.Drop(obj, hit.point);
            }

            return false;

        }
    }
}

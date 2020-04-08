using System;
using Assets.Scripts.DataModel;
using Assets.Scripts.DropZones;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts
{
    public class DragAndDropManager : MonoBehaviour
    {
        public event Action OnDragStart;
        public event Action OnDragStop;

        protected Transform handPlane;

        public void TriggerOnDragStart()
        {
            OnDragStart?.Invoke();
        }

        public void TriggerOnDragStop()
        {
            OnDragStop?.Invoke();
        }

        protected void Awake()
        {
            
            handPlane = GameObject.Find("HandPlane").transform;
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
            bool res = RaycastingHelper.RaycastToTableDropZones(out var hit, pos);
            TriggerOnDragStop();

            if (res)
            {
                DropZone dz = hit.transform.GetComponent<DropZone>();
                return dz.Drop(obj, hit.point);
            }

            return false;

        }

        /// <summary>
        /// Tries to put object at the spot.
        /// </summary>
        /// <returns>
        /// True if object was accepted, otherwise - false.
        /// </returns>
        public bool PutAt(BaseObjectBehaviour obj, Area area, Vector3 pos)
        {
            TriggerOnDragStart();
            bool res = false;
            RaycastHit hit;
            switch (area)
            {
                case Area.Hand:
                    res = RaycastingHelper.RaycastToHandDropZones(out hit, pos, handPlane.position);
                    break;
                case Area.Table:
                default:
                    res = RaycastingHelper.RaycastToTableDropZones(out hit, pos);
                    break;
            }
            
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

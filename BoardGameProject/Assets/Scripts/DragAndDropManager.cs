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
        protected UIDManager uidm;
        protected Table table;
        protected ModelContainerBehaviour hand;

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
            uidm = FindObjectOfType<UIDManager>();
            handPlane = GameObject.Find("HandPlane").transform;
            hand = handPlane.GetComponent<ModelContainerBehaviour>();
            table = FindObjectOfType<Table>();
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
        public bool PutAt(BaseObjectBehaviour obj, long parentUID, Vector3 pos)
        {
            TriggerOnDragStart();
            bool res = false;
            RaycastHit hit;
            var root = uidm.GetRootParentUID(parentUID);

            // TODO: find the root parent
            if (root == hand.ModelData.uid)
            {
                res = RaycastingHelper.RaycastToHandDropZones(out hit, pos, handPlane.position);
            } else if (root == table.ModelData.uid)
            {
                res = RaycastingHelper.RaycastToTableDropZones(out hit, pos);
            }
            else
            {
                res = RaycastingHelper.RaycastToTableDropZones(out hit, pos);
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

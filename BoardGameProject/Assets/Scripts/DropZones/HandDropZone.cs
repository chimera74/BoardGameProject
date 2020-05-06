using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts.DropZones
{
    public class HandDropZone : DropZone
    {
        protected ModelContainerBehaviour handArea;

        protected void Awake()
        {
            handArea = GetComponent<ModelContainerBehaviour>();
        }

        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            droppingObj.ModelData.parentUID = handArea.ModelData.uid;
            droppingObj.ModelData.Position = new Vector2(position.x, position.z);
            return true;
        }
    }
}
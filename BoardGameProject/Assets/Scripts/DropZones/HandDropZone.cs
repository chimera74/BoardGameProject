using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts.DropZones
{
    public class HandDropZone : DropZone
    {
        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            droppingObj.ModelData.Area = Area.Hand;
            droppingObj.ModelData.Position = new Vector2(position.x, position.z);
            return true;
        }
    }
}
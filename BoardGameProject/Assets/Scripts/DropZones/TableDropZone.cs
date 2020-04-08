using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts.DropZones
{
    public class TableDropZone : DropZone
    {
        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            droppingObj.ModelData.Area = Area.Table;
            droppingObj.ModelData.Position = new Vector2(position.x, position.z);
            return true;
        }
    }
}
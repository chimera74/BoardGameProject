using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts.DropZones
{
    public class TableDropZone : DropZone
    {

        protected Table table;

        protected void Awake()
        {
            table = GetComponent<Table>();
        }

        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            droppingObj.ModelData.parentUID = table.ModelData.uid;
            droppingObj.ModelData.Position = new Vector2(position.x, position.z);
            return true;
        }
    }
}
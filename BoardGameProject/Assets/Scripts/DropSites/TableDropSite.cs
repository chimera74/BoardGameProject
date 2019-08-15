using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.DropSites
{
    public class TableDropSite : DropSite
    {
        public override bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            droppingObj.ModelData.Position = new Vector2(position.x, position.z);
            return true;
        }
    }
}
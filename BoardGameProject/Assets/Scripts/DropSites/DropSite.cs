using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.DropSites
{
    public abstract class DropSite : MonoBehaviour
    {
        /// <summary>
        /// Method that processes dropping an object at this drop site.
        /// </summary>
        /// <returns>
        /// True if object was accepted, otherwise - false.
        /// </returns>
        public virtual bool Drop(BaseObjectBehaviour droppingObj, Vector3 position)
        {
            return false;
        }
    }
}

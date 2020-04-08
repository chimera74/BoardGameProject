using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public abstract class BaseObjectAppearance : MonoBehaviour
    {
        public abstract void UpdateAppearance();

        protected virtual void Start()
        {
            UpdateAppearance();
        }
    }
}
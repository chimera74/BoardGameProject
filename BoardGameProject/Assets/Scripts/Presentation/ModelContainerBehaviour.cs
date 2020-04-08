using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class ModelContainerBehaviour : MonoBehaviour
    {
        protected BaseObject _modelData;

        public BaseObject ModelData
        {
            get { return _modelData; }
            set { _modelData = value; }
        }
    }
}

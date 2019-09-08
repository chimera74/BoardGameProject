using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Objects
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

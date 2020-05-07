using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class BuildingBehaviour : ModelContainerBehaviour
    {
        protected override Type ModelType => typeof(Building);
        public new Building ModelData
        {
            get => (Building)_modelData;
            set => _modelData = value;
        }

        public GameObject[] hideableElements;

        protected override void Start()
        {
            base.Start();
            ModelData.OnOpen += OpenBuilding;
            ModelData.OnClose += CloseBuilding;
        }

        protected virtual void OnDestroy()
        {
            ModelData.OnOpen -= OpenBuilding;
            ModelData.OnClose -= CloseBuilding;
        }

        protected void OpenBuilding()
        {
            foreach (GameObject hideableElement in hideableElements)
            {
                hideableElement.GetComponent<Renderer>().enabled = false;
            }
        }

        protected void CloseBuilding()
        {
            foreach (GameObject hideableElement in hideableElements)
            {
                hideableElement.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}

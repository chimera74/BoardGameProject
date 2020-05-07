using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Presentation
{
    public class DoorBehaviour : ModelContainerBehaviour
    {

        protected override Type ModelType => typeof(Door);
        public new Door ModelData
        {
            get => (Door)_modelData;
            set => _modelData = value;
        }

        protected CardSlotBehaviour slotBeh;
        protected BuildingBehaviour building;

        protected override void Awake()
        {
            base.Awake();
            building = transform.parent.GetComponent<BuildingBehaviour>();
        }

        protected override void Start()
        {
            base.Start();
            slotBeh = GetComponentInChildren<CardSlotBehaviour>();
            slotBeh.ModelData.OnPutCard += OpenBuilding;
            slotBeh.ModelData.OnRemoveCard += CloseBuilding;
        }

        public void OpenBuilding()
        {
            building.ModelData.Open();
        }

        public void CloseBuilding()
        {
            building.ModelData.Close();
        }

        protected virtual void OnDestroy()
        {
            slotBeh.ModelData.OnPutCard -= OpenBuilding;
            slotBeh.ModelData.OnRemoveCard -= CloseBuilding;
        }
    }
}

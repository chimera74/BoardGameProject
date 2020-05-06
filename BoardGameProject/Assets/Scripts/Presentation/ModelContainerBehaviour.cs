using System;
using Assets.Scripts.DataModel;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Presentation
{
    public class ModelContainerBehaviour : MonoBehaviour
    {
        protected BaseObject _modelData;
        protected UIDManager uidm;

        protected virtual Type ModelType => typeof(BaseObject);

        public BaseObject ModelData
        {
            get => _modelData;
            set => _modelData = value;
        }

        protected virtual void Awake()
        {
            uidm = FindObjectOfType<UIDManager>();
        }

        protected virtual void Start()
        {
            GetUID();

            if (_modelData.parentUID == 0)
            {
                var components = GetComponentsInParent<ModelContainerBehaviour>();
                // UnityEngine.Debug.Log("For " + this);
                // foreach (var modelContainerBehaviour in components)
                // {
                //     UnityEngine.Debug.Log(modelContainerBehaviour);
                // }
                if (components.Length > 1)
                {
                    _modelData.parentUID = components[1].GetUID();
                }
            }
            uidm.RegisterUID(ModelData, this);
        }

        protected long GetUID()
        {
            if (_modelData == null)
                _modelData = CreateModelObject();

            if (_modelData.uid == 0)
            {
                _modelData.uid = uidm.GenerateUID();
                UnityEngine.Debug.Log(this + " got ID: " + _modelData.uid);
            }

            return _modelData.uid;
        }

        public override string ToString()
        {
            return gameObject.name; // + ", UID: " + ModelData.uid;
        }

        protected virtual BaseObject CreateModelObject()
        {
            return (BaseObject)Activator.CreateInstance(ModelType);
        }
    }
}

using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Table : MonoBehaviour, IPointerClickHandler
    {

        public Collider outsideTableCollider;

        [HideInInspector]
        public Collider tableCollider;

        private WACardGenerator cg;

        public void Awake()
        {
            tableCollider = GetComponent<Collider>();
        }

        // Use this for initialization
        void Start()
        {
            cg = FindObjectOfType<WACardGenerator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerClick(PointerEventData data)
        {
            PointerEventData pointerEventData = data as PointerEventData;
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                if (pointerEventData.clickCount > 1)
                    OnLeftMouseDoubleClick(pointerEventData);
            }
            else if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseClick(pointerEventData);
            }
        }

        private void OnLeftMouseDoubleClick(PointerEventData pointerEventData)
        {

        }

        private void OnRightMouseClick(PointerEventData pointerEventData)
        {
            cg.GenerateCard(0, pointerEventData.pointerPressRaycast.worldPosition);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Objects
{
    public class CardInHandBehaviour : ModelContainerBehaviour, IPointerClickHandler
    {
        public new Card ModelData
        {
            get { return (Card)_modelData; }
            set { _modelData = value; }
        }

        protected Renderer rend;
        protected BaseObjectAnimation animScr;
        protected BaseObjectAppearance apprn;

        protected virtual void Awake()
        {
            rend = GetComponent<Renderer>();
            animScr = GetComponent<BaseObjectAnimation>();
            apprn = GetComponent<BaseObjectAppearance>();
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

        protected virtual void OnLeftMouseDoubleClick(PointerEventData pointerEventData)
        {
        }

        protected virtual void OnRightMouseClick(PointerEventData pointerEventData)
        {
        }
    }
}

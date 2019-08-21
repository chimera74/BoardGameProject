using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects
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
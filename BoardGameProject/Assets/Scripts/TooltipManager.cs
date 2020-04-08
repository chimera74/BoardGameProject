using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TooltipManager : MonoBehaviour
    {

        public Text nameText;
        public Text descriptionText;
        protected CanvasGroup cg;

        protected void Awake()
        {
            cg = GetComponent<CanvasGroup>();
        }

        public void ShowTooltip()
        {
            cg.alpha = 1;
        }

        public void HideToolip()
        {
            cg.alpha = 0;
        }

        public void SetTooltip(string caption, string description)
        {
            nameText.text = caption;
            descriptionText.text = description;
        }
    }
}

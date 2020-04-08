using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Presentation
{
    public class WACardBehaviour : CardBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public new WACard ModelData
        {
            get { return (WACard)_modelData; }
            set { _modelData = value; }
        }

        protected TooltipManager tooltip;

        protected override void Awake()
        {
            base.Awake();
            tooltip = FindObjectOfType<TooltipManager>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetTooltip(ModelData.name, ModelData.description);
            tooltip.ShowTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.HideToolip();
        }
    }
}

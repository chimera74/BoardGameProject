using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using Assets.Scripts.Scriptables;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Presentation
{
    public class WACardBehaviour : CardBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        protected override Type ModelType => typeof(WACard);
        public new WACard ModelData
        {
            get { return (WACard)_modelData; }
            set { _modelData = value; }
        }

        public WACardSO cardPreset;

        protected WACardGenerator wacg;
        protected TooltipManager tooltip;

        protected override void Awake()
        {
            base.Awake();
            wacg = FindObjectOfType<WACardGenerator>();
            tooltip = FindObjectOfType<TooltipManager>();

            if (cardPreset != null)
            {
                ModelData = wacg.CreateCardFromSO(cardPreset);
            }
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

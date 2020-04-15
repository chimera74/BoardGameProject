using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GlobalTimerUI : MonoBehaviour
    {
        public Text timeText;
        public Text speedText;
        protected GlobalTimerBehaviour gtb;

        protected void Awake()
        {
            gtb = FindObjectOfType<GlobalTimerBehaviour>();
        }

        protected void Start()
        {
            gtb.model.OnTimerSpeedChanged += UpdateSpeedText;
            UpdateSpeedText();
        }

        protected void Update()
        {
            timeText.text = gtb.Hours + "h " + gtb.Minutes + "m " + gtb.SecondsFloat + "s";
        }

        protected void UpdateSpeedText()
        {
            speedText.text = gtb.model.isPaused ? "Paused" : ("Speed: " + gtb.model.speed);
        }

    }
}

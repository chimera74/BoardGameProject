using Assets.Scripts.DataModel;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GlobalTimerBehaviour : MonoBehaviour
    {

        public GlobalTimer model;

        public int Hours => model.Hours;
        public int Minutes => model.Minutes;
        public int Seconds => model.Seconds;
        public float SecondsFloat => model.SecondsFloat;

        protected void Awake()
        {
            model = new GlobalTimer();
        }

        protected void Update()
        {
            ProcessControls();
            model.Count(Time.deltaTime);
        }

        protected void ProcessControls()
        {
            if (Input.GetButtonDown("PausePlay"))
            {
                if (model.isPaused)
                    model.StartTimer();
                else
                    model.PauseTimer();
            }

            if (Input.GetButtonDown("Speed1"))
            {
                model.SetSpeed(TimerSpeed.Normal);
            }

            if (Input.GetButtonDown("Speed2"))
            {
                model.SetSpeed(TimerSpeed.Fast);
            }

            if (Input.GetButtonDown("Speed3"))
            {
                model.SetSpeed(TimerSpeed.Fastest);
            }
        }
    }
}
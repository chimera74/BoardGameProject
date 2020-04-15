using System.Collections;
using System;

namespace Assets.Scripts.DataModel
{
    public class GlobalTimer
    {

        public event Action OnTimerSpeedChanged;

        public float speed = 1.0f;
        public bool isPaused = true;

        public long timerValueMs = 0;

        public int Hours => (int) (timerValueMs / 3600000 % 24);
        public int Minutes => (int)(timerValueMs / 60000 % 60);
        public int Seconds => (int)(timerValueMs / 1000 % 60);
        public float SecondsFloat => timerValueMs / 1000 % 60;

        public void StartTimer()
        {
            isPaused = false;
            OnTimerSpeedChanged?.Invoke();
        }

        public void PauseTimer()
        {
            isPaused = true;
            OnTimerSpeedChanged?.Invoke();
        }

        public void SetSpeed(TimerSpeed newSpeed)
        {
            switch (newSpeed)
            {
                case TimerSpeed.Normal:
                    speed = 1.0f;
                    break;
                case TimerSpeed.Fast:
                    speed = 2.0f;
                    break;
                case TimerSpeed.Fastest:
                    speed = 3.0f;
                    break;
                default:
                    speed = 1.0f;
                    break;
            }
            OnTimerSpeedChanged?.Invoke();
        }

        public void Count(float deltaTime)
        {
            if (isPaused)
                return;

            timerValueMs += (long) (deltaTime * 1000 * speed);
        }


    }

    public enum TimerSpeed
    {
        Normal,
        Fast,
        Fastest
    }
}
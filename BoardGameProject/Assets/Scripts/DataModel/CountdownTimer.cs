using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataModel
{
    [Serializable]
    public class CountdownTimer
    {
        public event Action OnCountdownStart;
        public event Action OnCountdownStop;
        public event Action OnCountdownCompleted;

        public long startTimeMs;
        public long timeoutMs;
        public bool isPaused;

        protected GlobalTimer globalTimer;

        public CountdownTimer():this(10000)
        {
        }

        public CountdownTimer(long timeout)
        {
            timeoutMs = timeout;
            isPaused = true;
        }

        public void StartCountdown()
        {
            startTimeMs = globalTimer.timerValueMs;
            isPaused = false;
            OnCountdownStart?.Invoke();
        }

        public void StopCountdown()
        {
            isPaused = true;
            OnCountdownStop?.Invoke();
        }

        public void Count()
        {
            if (isPaused)
                return;

            if (globalTimer.timerValueMs >= startTimeMs + timeoutMs)
            {
                isPaused = true;
                OnCountdownCompleted?.Invoke();
            }
        }
    }
}

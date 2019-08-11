using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class SimpleActionQueue
    {
        private readonly System.Object _lock = new System.Object();
        private readonly Queue<Action> _actionQueue;

        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _actionQueue.Count;
                }
            }
        }

        public SimpleActionQueue()
        {
            _actionQueue = new Queue<Action>();
        }

        public void Enqueue(Action a)
        {
            lock (_lock)
            {
                _actionQueue.Enqueue(a);
            }
        }

        public void InvokeFirst()
        {
            Action ac = null;
            lock (_lock)
            {
                if (_actionQueue.Count == 0)
                    return;

                ac = _actionQueue.Dequeue();
            }

            ac?.Invoke();
        }

        public void InvokeAll()
        {
            int count = 0;
            lock (_lock)
            {
                count = _actionQueue.Count;
            }

            while (count > 0)
            {
                Action ac = null;
                lock (_lock)
                {
                    ac = _actionQueue.Dequeue();
                    count = _actionQueue.Count;
                }

                ac?.Invoke();
            }
        }
    }
}
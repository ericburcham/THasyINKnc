using System.Collections.Generic;
using System.Threading;

namespace WaitingAndPulsing
{
    public class ProducerConsumer<T>
    {
        public void Produce(T i)
        {
            lock (_listLock)
            {
                _queue.Enqueue(i);
                Monitor.Pulse(_listLock);
            }
        }

        public object Consume()
        {
            lock (_listLock)
            {
                while (_queue.Count == 0)
                {
                    // This releases _listLockand reacquires it
                    // after being woken up by a call to Pulse
                    Monitor.Wait(_listLock);
                }
                return _queue.Dequeue();
            }
        }

        private readonly object _listLock = new object();

        private readonly Queue<T> _queue = new Queue<T>();
    }
}
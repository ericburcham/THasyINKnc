using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;

namespace BasicObservableAndObserver
{
    public class SequenceOfIntegers : IObservable<int>
    {
        private readonly int _count;

        private readonly IList<IObserver<int>> _observers;

        public SequenceOfIntegers(int count)
        {
            _count = count;
            _observers = new List<IObserver<int>>();
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            _observers.Add(observer);
            return Disposable.Empty;
        }

        public void Iterate()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var threads = _observers.Select(BuildWorkerThread(random)).ToList();

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join());
        }

        private Func<IObserver<int>, Thread> BuildWorkerThread(Random random)
        {
            return observer => new Thread(
                                   () =>
                                       {
                                           for (var i = 0; i < _count; i++)
                                           {
                                               Thread.Sleep(random.Next(100));
                                               observer.OnNext(i);
                                           }
                                           observer.OnCompleted();
                                       });
        }
    }
}
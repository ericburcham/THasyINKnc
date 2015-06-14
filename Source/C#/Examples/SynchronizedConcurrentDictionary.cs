using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Examples
{
    public class SynchronizedConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>
    {
        public new TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            TValue result;

            _cacheLock.EnterWriteLock();
            try
            {
                result = base.GetOrAdd(key, valueFactory);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }

            return result;
        }

        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
    }
}
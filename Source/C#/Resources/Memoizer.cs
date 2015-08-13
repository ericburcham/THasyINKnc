using System;
using System.Collections.Generic;
using System.Threading;

namespace Resources
{
    public class Memoizer<TKey, TValue>
    {
        public Memoizer()
        {
            _cache = new Dictionary<TKey, TValue>();
            _cacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        public Memoizer(Func<TKey, TValue> func) : this()
        {
            _func = func;
        }

        public TValue GetOrInvoke(TKey key)
        {
            TValue result;

            _cacheLock.EnterWriteLock();
            try
            {
                if (!_cache.TryGetValue(key, out result))
                {
                    result = _func.Invoke(key);
                    _cache.Add(key, result);
                }
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }

            return result;
        }

        private readonly Dictionary<TKey, TValue> _cache;

        private readonly ReaderWriterLockSlim _cacheLock;
        
        private readonly Func<TKey, TValue> _func;
    }
}
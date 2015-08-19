using System;
using System.Collections.Generic;
using System.Threading;

namespace Resources
{
    public abstract class MemoizerBase<TKey, TValue, TDictionaryValue>
    {
        protected MemoizerBase(Func<TKey, TValue> func)
            : this()
        {
            Func = func;
        }

        private MemoizerBase()
        {
            Cache = new Dictionary<TKey, TDictionaryValue>();
            CacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        }

        public TValue GetOrInvoke(TKey key)
        {
            TValue result;

            CacheLock.EnterWriteLock();
            try
            {
                result = ValueExists(key) ? GetValue(key) : GenerateValue(key);
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }

            return result;
        }

        protected abstract TValue GenerateValue(TKey key);

        protected abstract TValue GetValue(TKey key);

        protected abstract bool ValueExists(TKey key);

        protected Dictionary<TKey, TDictionaryValue> Cache { get; private set; }

        protected ReaderWriterLockSlim CacheLock { get; private set; }

        protected Func<TKey, TValue> Func { get; private set; }
    }
}
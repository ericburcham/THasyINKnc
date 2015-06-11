﻿using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Examples
{
    public class SynchronizedConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>
    {
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();

        public new TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                return base.AddOrUpdate(key, addValue, updateValueFactory);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

        public new TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            _cacheLock.EnterWriteLock();
            try
            {
                return base.AddOrUpdate(key, addValueFactory, updateValueFactory);
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
        }

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
    }
}
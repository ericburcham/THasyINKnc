using System;
using System.Collections.Concurrent;

namespace Examples
{
    public static class FuncExtensions
    {
        public static Func<TArg, TResult> Memoize<TArg, TResult>(this Func<TArg, TResult> function)
        {
            var cache = new ConcurrentDictionary<TArg, TResult>();

            return key =>
                {
                    TResult result;

                    if (cache.TryGetValue(key, out result))
                    {
                        return result;
                    }

                    return cache.GetOrAdd(key, function);
                };
        }

        public static Func<TArg, TResult> ThreadSafeMemoize<TArg, TResult>(this Func<TArg, TResult> function)
        {
            var cache = new ConcurrentDictionary<TArg, TResult>();

            var cacheLock = new ConcurrentDictionary<TArg, object>();

            return key =>
                {
                    TResult result;

                    if (cache.TryGetValue(key, out result))
                    {
                        return result;
                    }

                    var lockObject = cacheLock.GetOrAdd(key, new object());
                    lock (lockObject)
                    {
                        result = cache.GetOrAdd(key, function);
                    }

                    cacheLock.TryRemove(key, out lockObject);

                    return result;
                };
        }

        public static Func<TArg, TResult> SynchronizedMemoize<TArg, TResult>(this Func<TArg, TResult> function)
        {
            var synchronizedCache = new SynchronizedConcurrentDictionary<TArg, TResult>();

            return key => synchronizedCache.GetOrAdd(key, function);
        }
    }
}

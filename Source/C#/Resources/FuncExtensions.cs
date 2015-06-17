using System;
using System.Collections.Concurrent;

namespace Resources
{
    public static class FuncExtensions
    {
        public static Func<TArgA, TArgB, TResult> Memoize<TArgA, TArgB, TResult>(
            this Func<TArgA, TArgB, TResult> function)
        {
            var example = new { argA = default(TArgA), argB = default(TArgB) };
            var tupled = CastByExample(t => function(t.argA, t.argB), example);
            var memoized = tupled.Memoize();
            return (a, b) => memoized(new { argA = a, argB = b });
        }

        private static Func<T, TR> CastByExample<T, TR>(this Func<T, TR> function, T example)
        {
            return function;
        }

        public static Func<TArg, TResult> Memoize<TArg, TResult>(this Func<TArg, TResult> function)
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
    }
}
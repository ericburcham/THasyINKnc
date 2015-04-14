using System;
using System.Collections.Concurrent;

namespace Examples
{
    public static class FuncExtensions
    {
        public static Func<TArg, TResult> Memoize<TArg, TResult>(this Func<TArg, TResult> function)
        {
            var cache = new ConcurrentDictionary<TArg, TResult>();

            var lockMap = new ConcurrentDictionary<TArg, object>();

            return key =>
                {
                    TResult result;

                    if (cache.TryGetValue(key, out result))
                    {
                        return result;
                    }

                    var lockObject = lockMap.GetOrAdd(key, new object());
                    lock (lockObject)
                    {
                        result = cache.GetOrAdd(key, function);
                    }

                    lockMap.TryRemove(key, out lockObject);

                    return result;
                };
        }

        public static Func<TArg1, TArg2, TResult> Memoize<TArg1, TArg2, TResult>(
            this Func<TArg1, TArg2, TResult> function)
        {
            var example = new { TA1 = default(TArg1), TA2 = default(TArg2) };
            var tuplified = CastByExample(t => function(t.TA1, t.TA2), example);
            var memoized = tuplified.Memoize();
            return (a, b) => memoized(new { TA1 = a, TA2 = b });
        }

        private static Func<TArg, TResult> CastByExample<TArg, TResult>(Func<TArg, TResult> function, TArg example)
        {
            return function;
        }
    }
}
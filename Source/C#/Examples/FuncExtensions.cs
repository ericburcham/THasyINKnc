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
    }
}
using System;
using System.Collections.Concurrent;

namespace Examples
{
    public static class FuncExtensions
    {
        public static Func<TA, TR> Memoize<TA, TR>(this Func<TA, TR> function)
        {
            var cache = new ConcurrentDictionary<TA, TR>();

            var lockMap = new ConcurrentDictionary<TA, object>();

            return key =>
                {
                    TR result;

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
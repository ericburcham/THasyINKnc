using System;

namespace Resources
{
    public class Memoizer<TArg, TResult> : MemoizerBase<TArg, TResult, TResult>
    {
        public Memoizer(Func<TArg, TResult> func)
            : base(func)
        {
        }

        protected override TResult SetCacheValue(TArg key)
        {
            var value = Func(key);
            Cache.Add(key, value);
            return value;
        }

        protected override TResult GetCacheValue(TArg key)
        {
            return Cache[key];
        }

        protected override bool ValueExists(TArg key)
        {
            return Cache.ContainsKey(key);
        }
    }
}
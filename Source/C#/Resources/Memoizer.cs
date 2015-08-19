using System;

namespace Resources
{
    public class Memoizer<TKey, TValue> : MemoizerBase<TKey, TValue, TValue>
    {
        public Memoizer(Func<TKey, TValue> func)
            : base(func)
        {
        }

        protected override TValue GenerateValue(TKey key)
        {
            var value = Func(key);
            Cache.Add(key, value);
            return value;
        }

        protected override TValue GetValue(TKey key)
        {
            return Cache[key];
        }

        protected override bool ValueExists(TKey key)
        {
            return Cache.ContainsKey(key);
        }
    }
}
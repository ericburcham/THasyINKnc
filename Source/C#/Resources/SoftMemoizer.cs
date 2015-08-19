using System;

namespace Resources
{
    public class SoftMemoizer<TKey, TValue> : MemoizerBase<TKey, TValue, WeakReference>
    {
        public SoftMemoizer(Func<TKey, TValue> func)
            : base(func)
        {
        }

        protected override TValue GenerateValue(TKey key)
        {
            var value = Func(key);
            Cache.Add(key, new WeakReference(value));
            return value;
        }

        protected override TValue GetValue(TKey key)
        {
            TValue value;
            var weakReference = Cache[key];
            if (weakReference.Target == null)
            {
                value = Func(key);
                Cache[key].Target = value;
            }
            else
            {
                value = (TValue)weakReference.Target;
            }

            return value;
        }

        protected override bool ValueExists(TKey key)
        {
            return Cache.ContainsKey(key);
        }
    }
}
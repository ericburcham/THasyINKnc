using System;

namespace Resources
{
    public class ExpirableMemoizer<TArg, TResult> : MemoizerBase<TArg, TResult, WeakReference>
    {
        public ExpirableMemoizer(Func<TArg, TResult> func)
            : base(func)
        {
        }

        protected override TResult SetCacheValue(TArg key)
        {
            var value = Func(key);
            Cache.Add(key, new WeakReference(value));
            return value;
        }

        protected override TResult GetCacheValue(TArg key)
        {
            TResult value;
            var weakReference = Cache[key];
            if (weakReference.Target == null)
            {
                value = Func(key);
                Cache[key].Target = value;
            }
            else
            {
                value = (TResult)weakReference.Target;
            }

            return value;
        }

        protected override bool ValueExists(TArg key)
        {
            return Cache.ContainsKey(key);
        }
    }
}
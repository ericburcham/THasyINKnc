using System;

namespace Resources
{
    public static class FuncExtensions
    {
        public static Func<TArgA, TArgB, TResult> Memoize<TArgA, TArgB, TResult>(this Func<TArgA, TArgB, TResult> func)
        {
            var example = new { argA = default(TArgA), argB = default(TArgB) };
            var tupled = CastByExample(t => func(t.argA, t.argB), example);
            var memoized = tupled.Memoize();
            return (a, b) => memoized(new { argA = a, argB = b });
        }

        // ReSharper disable once UnusedParameter.Local
        private static Func<T, TR> CastByExample<T, TR>(this Func<T, TR> function, T example)
        {
            return function;
        }

        public static Func<TArg, TResult> Memoize<TArg, TResult>(this Func<TArg, TResult> func)
        {
            var cache = new Memoizer<TArg, TResult>(func);
            return argument => cache.GetOrInvoke(argument);
        }
    }
}
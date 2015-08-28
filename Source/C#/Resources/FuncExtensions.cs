using Funky;
using System;

namespace Resources
{
    public static class FuncExtensions
    {
        public static Func<TArgA, TArgB, TResult> Memoize<TArgA, TArgB, TResult>(this Func<TArgA, TArgB, TResult> func)
        {
            var example = new { argA = default(TArgA), argB = default(TArgB) };
            var tupled = CastByExample(t => func(t.argA, t.argB), example);
            var memoized = tupled.Memoize(false);
            return (a, b) => memoized(new { argA = a, argB = b });
        }

        // ReSharper disable once UnusedParameter.Local
        private static Func<TArg, TResult> CastByExample<TArg, TResult>(this Func<TArg, TResult> function, TArg example)
        {
            return function;
        }
    }
}
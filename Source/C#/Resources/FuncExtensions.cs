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

        public static Func<TArg, TResult> Memoize<TArg, TResult>(
            this Func<TArg, TResult> func,
            bool isExpirable)
        {
            IMemoizeFunctions<TArg, TResult> memoizer;
            if (isExpirable)
            {
                memoizer = new ExpirableMemoizer<TArg, TResult>(func);
            }
            else
            {
                memoizer = new Memoizer<TArg, TResult>(func);
            }
            return argument => memoizer.GetOrInvoke(argument);
        }

        // ReSharper disable once UnusedParameter.Local
        private static Func<TArg, TResult> CastByExample<TArg, TResult>(this Func<TArg, TResult> function, TArg example)
        {
            return function;
        }
    }
}
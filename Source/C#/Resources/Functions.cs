using Funky;
using System;
using System.Threading;

namespace Resources
{
    public static class Functions
    {
        public static Func<int, int, int> Divide = (a, b) => a / b;

        public static Func<int, int> Fibonacci
        {
            get
            {
                if (_iterativeMemoized == null)
                {
                    _iterativeMemoized = n =>
                        {
                            var a = 0;
                            var b = 1;

                            for (var i = 0; i < n; ++i)
                            {
                                var temp = a;
                                a = b;
                                b = temp + b;
                            }
                            Thread.Sleep(500);
                            return a;
                        };

                    _iterativeMemoized = _iterativeMemoized.Memoize(false);
                }
                return _iterativeMemoized;
            }
        }

        public static Func<int, int, int> GreatestCommonFactor
        {
            get
            {
                if (_greatestCommonFactor == null)
                {
                    _greatestCommonFactor = (x, y) =>
                        {
                            if (y > x)
                            {
                                var temp = x;
                                x = y;
                                y = temp;
                            }

                            Thread.Sleep(500);
                            return y == 0 ? x : _greatestCommonFactor(y, x % y);
                        };

                    _greatestCommonFactor = _greatestCommonFactor.Memoize();
                }
                return _greatestCommonFactor;
            }
        }

        public static readonly Func<int, int, int> Multiply = (a, b) =>
            {
                Thread.Sleep(500);
                return a * b;
            };

        public static readonly Func<int, int> Square = i => i * i;

        private static Func<int, int, int> _greatestCommonFactor;

        private static Func<int, int> _iterativeMemoized;
    }
}
using System;

namespace Calculator.Views.Calculator.Services.Helpers.CalculateService
{
    public static class FactorialHelper
    {
        public static double CalculateFactorial(double value)
        {
            if (value % 1 == 0)
            {
                return Factorial((int)value);
            }

            return Gamma(value + 1);
        }

        private static double Factorial(int value)
        {
            if (value == 1)
            {
                return 1;
            }

            return value * Factorial(value - 1);
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Lanczos_approximation
        /// </summary>
        private static double Gamma(double z)
        {
            // p is the values of c[i] to plug into Lanczos' formula
            var p = new[] { 0.99999999999980993, 676.5203681218851, -1259.1392167224028, 771.32342877765313,
                -176.61502916214059, 12.507343278686905, -0.13857109526572012, 9.9843695780195716e-6, 1.5056327351493116e-7 };

            // It's represents the precision desired
            var g = 7;

            if (z < 0.5)
            {
                return Math.PI / (Math.Sin(Math.PI * z) * Gamma(1 - z));
            }
            else
            {
                z -= 1;

                var x = p[0];

                for (var i = 1; i < g + 2; i++)
                {
                    x += p[i] / (z + i);
                }

                var t = z + g + 0.5;

                return Math.Sqrt(2 * Math.PI) * Math.Pow(t, (z + 0.5)) * Math.Exp(-t) * x;
            }
        }
    }
}

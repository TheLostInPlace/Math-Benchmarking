using System;

namespace Math_Approximation
{
    class ModifiedTaylors
    {
        const double π = Math.PI;
        const double π2 = Math.PI / 2;
        const double π4 = Math.PI / 4;

        public double Sin(double x)
        {

            if (x == 0) { return 0; }
            if (x < 0) { return -Sin(-x); }
            if (x > π) { return -Sin(x - π); }
            if (x > π4) { return Cos(π2 - x); }

            double x2 = x * x;

            return x * (x2 / 6 * (x2 / 20 * (x2 / 42 * (x2 / 72 * (x2 / 110 * (x2 / 156 - 1) + 1) - 1) + 1) - 1) + 1);
        }

        public double Cos(double x)
        {
            if (x == 0) { return 1; }
            if (x < 0) { return Cos(-x); }
            if (x > π) { return -Cos(x - π); }
            if (x > π4) { return Sin(π2 - x); }

            double x2 = x * x;

            return x2 / 2 * (x2 / 12 * (x2 / 30 * (x2 / 56 * (x2 / 90 * (x2 / 132 - 1) + 1) - 1) + 1) - 1) + 1;
        }
    }
}

using System;

namespace Math_Approximation
{
    // Code from https://szakuljarzuk.wordpress.com/2015/12/20/problem-how-to-calculate-sin-cos-using-cordic-algorithm/
    class CordicMath
    {

        public double[] lookup;
        public int iterations = 32;
        public double K = 0.60725293510314;
        public double halfPi = 1.57079632679;

        public CordicMath()
        {
            createLookupTable();
        }

        public double sin(double theta)
        {
            return sinCos(theta)[1];
        }

        public double cos(double theta)
        {
            return sinCos(theta)[0];
        }

        public double[] sinCos(double theta)
        {
            if (theta < 0 || theta > halfPi)
            {
                throw new Exception("Required value 0 < x < Pi/2");
            }
            double x = K;
            double y = 0;
            double z = theta;
            double v = 1.0;
            for (int i = 0; i < iterations; i++)
            {
                double d = (z >= 0) ? +1 : -1;
                double tx = x - d * y * v;
                double ty = y + d * x * v;
                double tz = z - d * lookup[i];
                x = tx; y = ty; z = tz;
                v *= 0.5;
            }
            double[] result = { x, y };
            return result;
        }

        private void createLookupTable()
        {
            lookup = new double[iterations];
            for (int i = 0; i < iterations; i++)
            {
                lookup[i] = Math.Atan(1 / Math.Pow(2, i));
                //Console.WriteLine($"Tan (%02d): %.14f / %018.3f %n", i, lookup[i], Math.Pow(2, i));
            }
        }

    }
}

using System;

namespace Math_Approximation
{
    class CSine
    {
        public float sin(float x)
        {
            const float B = (float)(4 / Math.PI);
            const float C = (float)(-4 / (Math.PI * Math.PI));
            const float P = 0.225F;
            float y = B * x + C * x * Math.Abs(x);

            float z = P * (y * Math.Abs(y) - y) + y;
            return y;
        }

        public float cos(float x)
        {
            return sin((float)(x + Math.PI / 2));
        }
    }
}

using System;

namespace lab_3.Model
{
    static public class MathModel
    {
        static public double GetFunc(double x, double a)
        {
            return Math.Sqrt((Math.Pow(a, 2) * Math.Pow(x, 2) - Math.Pow(x, 4)) / Math.Pow(a, 2));
        }
    }
}

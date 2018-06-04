using LiftingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftingAPI.Helpers
{
    public class WilksCalculator
    {
        private LifterModel _lifter;

        public WilksCalculator(LifterModel lifter)
        {
            _lifter = lifter;
        }
        public double Calculate()
        {
            double totalweight = Double.Parse(_lifter.LiftingNumbers.Squat) +
                Double.Parse(_lifter.LiftingNumbers.Bench) +
                Double.Parse(_lifter.LiftingNumbers.Deadlift);
            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;
            double e = 0;
            double f = 0;
            double convert = 0.453592;
            double result = 0;
            double x = Double.Parse(_lifter.Weight) * convert;
            if (_lifter.Gender.Equals("Male"))
            {
                a = -216.0475144;
                b = 16.2606339;
                c = -0.002388645;
                d = -0.00113732;
                e = 7.01863E-06;
                f = -1.291E-08;
            }
            else
            {
                a = 594.31747775582;
                b = -27.23842536447;
                c = 0.82112226871;
                d = -0.00930733913;
                e = 4.731582E-05;
                f = -9.054E-08;
            }
            result = totalweight * convert;
            result *= (500 / ((a) + b * x + c * Math.Pow(x, 2) + d * Math.Pow(x, 3) +
                e * Math.Pow(x, 4) + f * Math.Pow(x, 5)));
            return result;
        }
    }
}

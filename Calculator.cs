//Creating a calculator class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    // public class CalculatorApp
    // {
    //     public decimal Add(decimal a, decimal b)
    //     {
    //         return a + b;
    //     }

    //     public decimal Subtract(decimal a, decimal b)
    //     {
    //         return a - b;
    //     }

    //     public decimal Multiply(decimal a, decimal b)
    //     {
    //         return a * b;
    //     }

    //     public decimal Divide(decimal a, decimal b)
    //     {
    //         return a / b;
    //     }

    // }

    public class CalculatorApp
    {
        public decimal Add(decimal a, decimal b) => a + b;
        public decimal Subtract(decimal a, decimal b) => a - b;
        public decimal Multiply(decimal a, decimal b) => a * b;
        public decimal Divide(decimal a, decimal b)
        {
            if (b == 0) throw new DivideByZeroException();
            return a / b;
        }
        public decimal Percentage(decimal a, decimal b)
        {
            if (b == 0) throw new DivideByZeroException();
            return (a / b) * 100;
        }
        public decimal SquareRoot(decimal a)
        {
            if (a < 0) throw new ArgumentOutOfRangeException("Cannot calculate square root of a negative number.");
            return (decimal)Math.Sqrt((double)a);
        }
        public decimal Power(decimal a, decimal b)
        {
            return (decimal)Math.Pow((double)a, (double)b);
        }
        public decimal Modulus(decimal a, decimal b)
        {
            return a % b;
        }
        public decimal Logarithm(decimal a, decimal b)
        {
            if (a <= 0 || b <= 0 || b == 1) throw new ArgumentOutOfRangeException("Invalid logarithm base or value.");
            return (decimal)Math.Log((double)a, (double)b);
        }

}

}

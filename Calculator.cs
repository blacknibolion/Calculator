using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CalculatorApp
{
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
            if (a <= 0 || b <= 0 || b == 1)
                throw new ArgumentOutOfRangeException("Invalid logarithm base or value.");
            return (decimal)Math.Log((double)a, (double)b);
        }

        // Parses and differentiates a polynomial string
        public string Differentiate(string function)
        {
            string pattern = @"([+-]?\s*\d*)x\^?(\d*)";
            var matches = Regex.Matches(function.Replace(" ", ""), pattern);
            string result = string.Empty;

            foreach (Match match in matches)
            {
                string coefficient = match.Groups[1].Value.Replace(" ", "");
                string exponent = match.Groups[2].Value;

                int coeff = string.IsNullOrEmpty(coefficient) || coefficient == "+" ? 1 :
                            coefficient == "-" ? -1 : int.Parse(coefficient);

                int power = string.IsNullOrEmpty(exponent) ? 1 : int.Parse(exponent);

                int newCoeff = coeff * power;
                int newPower = power - 1;

                if (newPower > 1)
                    result += $"{(newCoeff >= 0 && result != "" ? "+" : "")}{newCoeff}x^{newPower}";
                else if (newPower == 1)
                    result += $"{(newCoeff >= 0 && result != "" ? "+" : "")}{newCoeff}x";
                else if (newPower == 0)
                    result += $"{(newCoeff >= 0 && result != "" ? "+" : "")}{newCoeff}";
            }

            return string.IsNullOrEmpty(result) ? "0" : result;
        }

        // Evaluates a polynomial string at a given x value
        public double EvaluatePolynomial(string expression, double x)
        {
            double result = 0;

            // Normalize expression
            expression = expression.Replace("-", "+-");
            string[] terms = expression.Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string raw in terms)
            {
                string term = raw.Trim();
                if (term.Contains("x^"))
                {
                    var parts = term.Split("x^");
                    double coeff = double.Parse(parts[0]);
                    int power = int.Parse(parts[1]);
                    result += coeff * Math.Pow(x, power);
                }
                else if (term.Contains("x"))
                {
                    string coeffStr = term.Replace("x", "");
                    double coeff = string.IsNullOrEmpty(coeffStr) ? 1 : double.Parse(coeffStr);
                    result += coeff * x;
                }
                else
                {
                    result += double.Parse(term);
                }
            }

            return result;
        }
    }
}

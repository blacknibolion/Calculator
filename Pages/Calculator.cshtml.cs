using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using CalculatorApp; // Ensure this matches the namespace of PolynomialParser
using System.Collections.Generic;

namespace CalculatorApp.Pages
{
    public class CalculatorModel : PageModel
    {
        [BindProperty]
        public string FormType { get; set; } = string.Empty;

        [BindProperty]
        //[Required(ErrorMessage = "The FirstNumber field is required.")]
        public decimal? FirstNumber { get; set; }

        [BindProperty]
        //[Required(ErrorMessage = "The SecondNumber field is required.")]
        public decimal? SecondNumber { get; set; }

        [BindProperty]
       // [Required(ErrorMessage = "The Operation field is required.")]
        public string Operation { get; set; } = string.Empty;

        [BindProperty]
        public string Polynomial { get; set; } = string.Empty;

        public string? Result { get; set; }
        public string? Derivative { get; set; }

        public List<(double x, double fx)> PointsOriginal { get; set; } = new();
        public List<(double x, double fx)> PointsDerived { get; set; } = new();

        public IActionResult OnPost()
        {
            if (FormType == "Arithmetic")
            {
                if (!FirstNumber.HasValue || !SecondNumber.HasValue || string.IsNullOrWhiteSpace(Operation))
                {
                    ModelState.AddModelError(string.Empty, "All fields are required for arithmetic operations.");
                    return Page();
                }

                try
                {
                    Result = Operation switch
                    {
                        "+" => (FirstNumber + SecondNumber).ToString(),
                        "-" => (FirstNumber - SecondNumber).ToString(),
                        "*" => (FirstNumber * SecondNumber).ToString(),
                        "/" => SecondNumber != 0 ? (FirstNumber / SecondNumber).ToString() : "Cannot divide by zero",
                        "%" => (FirstNumber % SecondNumber).ToString(),
                        "^" => Math.Pow((double)FirstNumber, (double)SecondNumber).ToString(),
                        "sqrt" => FirstNumber >= 0 ? Math.Sqrt((double)FirstNumber).ToString() : "Invalid sqrt input",
                        "M" => (FirstNumber % SecondNumber).ToString(),
                        "log" => Math.Log((double)FirstNumber, (double)SecondNumber).ToString(),
                        _ => "Invalid operation"
                    };
                }
                catch (Exception ex)
                {
                    Result = $"Error: {ex.Message}";
                }
            }
            else if (FormType == "Derivative")
            {
                if (string.IsNullOrWhiteSpace(Polynomial))
                {
                    ModelState.AddModelError(nameof(Polynomial), "Polynomial is required.");
                    return Page();
                }

                var calculator = new CalculatorApp();

                // Get derivative string
                Derivative = calculator.Differentiate(Polynomial);

                // Evaluate original and derived polynomials at x = -10 to 10
                for (double x = -10; x <= 10; x += 1)
                {
                    PointsOriginal.Add((x, calculator.EvaluatePolynomial(Polynomial, x)));
                    PointsDerived.Add((x, calculator.EvaluatePolynomial(Derivative, x)));
                }

            }

            return Page();
        }
    }
}

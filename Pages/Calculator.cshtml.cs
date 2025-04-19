using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CalculatorApp.Pages
{
    public class CalculatorModel : PageModel
    {
        [BindProperty]
        [Required]
        public decimal FirstNumber { get; set; }

        [BindProperty]
        [Required]
        public decimal SecondNumber { get; set; }

        [BindProperty]
        [Required]
        public string Operation { get; set; } = string.Empty;

        public string? Result { get; set; }

        public void OnPost()
        {
            try
            {
                Result = Operation switch
                {
                    "+" => (FirstNumber + SecondNumber).ToString(),
                    "-" => (FirstNumber - SecondNumber).ToString(),
                    "*" => (FirstNumber * SecondNumber).ToString(),
                    "/" => SecondNumber != 0 ? (FirstNumber / SecondNumber).ToString() : "Cannot divide by zero",
                    "%" => (FirstNumber % SecondNumber).ToString(),
                    "sqrt" => FirstNumber >= 0 ? Math.Sqrt((double)FirstNumber).ToString() : "Cannot calculate square root of a negative number",
                    "^" => Math.Pow((double)FirstNumber, (double)SecondNumber).ToString(),
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
    }
}

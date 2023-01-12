using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTreckerApi.Models
{
	public class Incomes
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string? Name { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string? DescriptionIncomes { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal MoneyIncomes { get; set; }

        public int? IncomesCategoryId { get; set; }

        public IncomeCategory? IncomesCategory { get; set; }

    }
}

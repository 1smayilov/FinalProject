using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product> // 6.
    {
        public ProductValidator() 
        {
            // kim üçündürdür bu kural
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Produktlar A hərfi ilə başlamalıdır"); // Özümüzdən yazdığımız kod
        }

        private bool StartWithA(string arg) // Buradakı arg bizim göndərdiyimiz parametrdir (ProductName)
        {
            return arg.StartsWith("A");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafio.Business.Models.Produtos.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("A propriedade {PropertyName} é obrigatória")
                .Length(2,100).WithMessage("A propriedade {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("A propriedade {PropertyName} é obrigatória")
                .GreaterThan(0).WithMessage("A propriedade {PropertyName} precisa ser maior que {ComparisonValue}");


        }
    }
}

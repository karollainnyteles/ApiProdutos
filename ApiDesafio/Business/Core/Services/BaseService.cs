using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiDesafio.Business.Core.Models;
using ApiDesafio.Business.Core.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace ApiDesafio.Business.Core.Services
{
    // classe que executa a validacao e notifica os erros
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

       
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);
            if(validator.IsValid) return true ;

            Notificar(validator);

            return false;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));

        }
    }
}

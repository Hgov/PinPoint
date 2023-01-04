using FluentValidation;
using PinPoint.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinPoint.Core.FluentValidation
{
    public interface IFluentValidation<TEntity> where TEntity : class
    {
        AbstractValidator<User> GetRules();
        AbstractValidator<User> GetByIdRules();
        AbstractValidator<User> PostRules();
        AbstractValidator<User> PutRules();
    }
}

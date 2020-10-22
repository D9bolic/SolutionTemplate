using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ticketmanagement.BusinessLogic.CustomExceptions;

namespace Ticketmanagement.BusinessLogic.Services
{
    internal class BaseValidationService<T>
        where T : class
    {
        protected BaseValidationService()
        {
        }

        protected static void ValidateItemForNull(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException($"{typeof(T).Name} is not defined");
            }
        }

        protected static void ValidateIsIdMoreThanZero(int id)
        {
            if (id < Constants.BottomLimitOfId)
            {
                throw new NotPossitiveIdException();
            }
        }

        protected static void ValidateModel(object obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                throw new ModelValidationException(results.ElementAt(0).ErrorMessage);
            }
        }
    }
}

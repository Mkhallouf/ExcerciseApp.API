using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace ExcerciseApp.API.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary
                .SelectMany(m => m.Value.Errors)
                .Select(s => s.ErrorMessage)
                .ToList();
        }
    }
}

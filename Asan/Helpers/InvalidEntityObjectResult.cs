using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asan.Helpers
{
    public class InvalidEntityObjectResult : ObjectResult
    {
        public InvalidEntityObjectResult(ModelStateDictionary modelState) : base(new SerializableError(modelState))
        {
            StatusCode = 422;
        }
    }
}

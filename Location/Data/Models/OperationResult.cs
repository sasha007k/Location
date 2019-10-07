using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class OperationResult<T>
    {
        public T Location { get; set; }
        public bool Success { get; set; }

        public const string Message = "Please, input another address";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class OperationResult<TIn>
    {
        public TIn ResponseModel { get; set; }
        public bool Success { get; set; }

        public string Message { get; set; }

        public OperationResult()
        {

        }
        public OperationResult(TIn responseModel, bool success)
        {
            ResponseModel = responseModel;
            Success = success;
        }

        public OperationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}

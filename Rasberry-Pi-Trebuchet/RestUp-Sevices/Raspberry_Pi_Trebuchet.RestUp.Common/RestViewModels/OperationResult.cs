using System;

namespace Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels
{
    public class OperationResult<T> where T : struct
    {
        public OperationResult()
        {
        }

        public OperationResult(T Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
            this.ResultOccured = DateTime.Now;
        }

        public T Result { get; set; }
        public string Message { get; set; }

        public DateTime ResultOccured { get; set; }
    }
}
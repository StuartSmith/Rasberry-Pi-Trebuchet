using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels
{
    public class OperationResult<T> where T : struct
    {
         public OperationResult() { }

        public OperationResult(T Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
        }

        public T Result { get; set; }
        public string Message { get; set; }
    }
}

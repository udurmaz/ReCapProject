using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Read Only constructor dışında set edilemez ancak , constructor içinde set edilebilir , 
    //Do Not Repeat yourself
    public class Result : IResult
    {
        public Result(bool success , string message):this(success) //this demek bu classı kullanıyorum demek
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}

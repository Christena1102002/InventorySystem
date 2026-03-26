using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventorySystem.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess {  get; set; }

        public string? Message {  get; set; }
        public T? Data {  get; set; }
        public List<string>? Errors { get; set; }
        public ErrorType ErrorType { get; set; } = ErrorType.None;
        public static Result<T> Success(T? data,string? message=null) => new Result<T>
        {
            IsSuccess = true,
            Message = message,
          
            Data = data
        };
        public static  Result<T> Failure(string message,List<string>? errors = null)
          => new Result<T>
          {
              IsSuccess = false,
              Message = message,
              Errors = errors,
          };
    }
}

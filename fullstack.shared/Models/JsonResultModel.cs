using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fullstack.shared.Models
{
    public class JsonResultModel<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public JsonResultModel(T result, string message, bool success)
        {
            this.Result = result;
            this.Message = message;
            this.Success = success;
        }

        public JsonResultModel()
        {
        }
    }
}
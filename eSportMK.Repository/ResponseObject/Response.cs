using System;
using System.Collections.Generic;
using System.Text;

namespace eSportMK.Repository.ResponseObject
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}

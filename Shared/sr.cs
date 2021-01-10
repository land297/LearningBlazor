using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Shared {
    public class sr<T> {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public void SetSuccess(T data) {
            Data = data;
            Success = true;
        }
        public static sr<T> Get() {
            var s = new sr<T>();
            return s;
        }
        public static sr<T> Get(string message) {
            var s = new sr<T>();
            s.Message = message;
            return s;
        }
        public static sr<T> Get(Exception exception) {
            var s = new sr<T>();
            s.Exception = exception;
            return s;
        }
        public static sr<T> Get(T data) {
            var s = new sr<T>();
            s.Data = data;
            return s;
        }
        public static sr<T> GetSuccess(T data) {
            var s = new sr<T>();
            s.Data = data;
            s.Success = true;
            return s;
        }
    }
}

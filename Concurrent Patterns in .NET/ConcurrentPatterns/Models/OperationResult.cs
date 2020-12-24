using System.Collections.Generic;
using System.Collections;

namespace ConcurrentModels.Models
{
    public enum Result
    {
        Success,
        Failure
    }
    public class OperationResult<T>
    {
        public Result Result { get; set; }
        public T Value { get; set; }
        public List<string> Messages { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
    
}

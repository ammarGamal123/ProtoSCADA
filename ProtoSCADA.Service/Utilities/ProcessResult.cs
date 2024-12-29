namespace ProtoSCADA.Service.Utilities
{
    public class ProcessResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public string Message { get; set; }

        // Success overload with message and data
        public static ProcessResult<T> Success(string message, T data) =>
            new ProcessResult<T> { IsSuccess = true, Data = data, Message = message };

        // Failure overload with message and data
        public static ProcessResult<T> Failure(string errorMessage, T data) =>
            new ProcessResult<T> { IsSuccess = false, ErrorMessage = errorMessage, Data = data };

        // Success overload with data only
        public static ProcessResult<T> Success(T data) =>
            new ProcessResult<T> { IsSuccess = true, Data = data };

        // Failure overload with errorMessage only
        public static ProcessResult<T> Failure(string errorMessage) =>
            new ProcessResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}

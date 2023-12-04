namespace TaskManagement.Domain.ViewModel
{
    public class ResponseViewModel<T>
    {
        public ResponseViewModel() { }

        public ResponseViewModel(bool success, string? message, T? data, List<T>? erros = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Erros = erros;
        }

        public bool Success { get; set; } = true;
        public string? Message { get; set; } = "Operation finally successfuly!";
        public T? Data { get; set; }
        public List<T>? Erros { get; set; }
    }

    public class ResponseViewModel
    {
        public ResponseViewModel() { }
        public ResponseViewModel(bool success, string message, object? data, List<string>? erros = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Erros = erros;
        }

        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Operation finally successfuly!";
        public object? Data { get; set; }
        public List<string>? Erros { get; set; }
    }
}
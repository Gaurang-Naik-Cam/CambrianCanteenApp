namespace CanteenApp.Common.Lib
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public Result()
        {
            IsSuccess = false;
            Message = string.Empty;
        }
    }
}
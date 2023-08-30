namespace lesson1_Simple_Functions___Controller.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        public Response() { }
        public Response(T data)
        {
            IsSuccess = true;
            Error = null;
            Data = data;
        }


    }
}

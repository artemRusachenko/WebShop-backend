namespace lesson1_Simple_Functions___Controller.Exceptions
{
    public class UserException : ArgumentException
    {
        public string Value { get; }
        public string Message { get; }
        public UserException(string message, string value) 
        {
            Value = value;
            Message = message;
        }
    }
}

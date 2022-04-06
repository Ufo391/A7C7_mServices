namespace CommunicationApi.Exceptions
{
    // Exception Klasse

    public class SecurityException : Exception
    {
        public enum ERROR_CODE
        {
            SECURITY_VALIDATION_FAILED
        }

        public ERROR_CODE ErrorCode { get; private set; }

        public SecurityException(ERROR_CODE code) : base()
        {
            ErrorCode = code;
        }

        public SecurityException(ERROR_CODE code, string message) : base(message)
        {
            ErrorCode = code;
        }

        public SecurityException(ERROR_CODE code, string message, Exception inner) : base(message, inner)
        {
            ErrorCode = code;
        }

        public override string ToString()
        {
            return $"[{Enum.GetName(typeof(ERROR_CODE), ErrorCode)}]: {Message}";
        }
    }
}

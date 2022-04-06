namespace CommunicationApi.Exceptions
{
    // Exceptionklasse

    public class ReliabilityException : Exception
    {
        public enum ERROR_CODE
        {
            INCOMING_VALIDATION_FAILED, EXECUTING_FAILED, INVALID_ORDER_PAIR_TO_ORDERBOOK
        }

        public ERROR_CODE ErrorCode { get; private set; }

        public ReliabilityException(ERROR_CODE code) : base()
        {
            ErrorCode = code;
        }

        public ReliabilityException(ERROR_CODE code, string message) : base(message)
        {
            ErrorCode = code;
        }

        public ReliabilityException(ERROR_CODE code, string message, Exception inner) : base(message, inner)
        {
            ErrorCode = code;
        }

        public override string ToString()
        {
            return $"[{Enum.GetName(typeof(ERROR_CODE), ErrorCode)}]: {Message}";
        }
    }
}

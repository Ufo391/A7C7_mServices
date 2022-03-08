using System;
using System.Collections.Generic;
using System.Text;

namespace CommunicationApi.Exceptions
{
    public class AbstractLayerException : Exception
    {
        public enum ERROR_CODES
        {
            LAYER_WAS_ALREADY_SET
        }

        public ERROR_CODES ErrorCode
        {
            get; 
            private set;
        }

        public AbstractLayerException(ERROR_CODES code)
        {
            ErrorCode = code;
        }

        public AbstractLayerException(string? message) : base(message)
        {

        }

        public AbstractLayerException(string? message, Exception? innerException) : base(message, innerException)
        {

        }

        public override string ToString()
        {
            return $"[{Enum.GetName(typeof(ERROR_CODES), ErrorCode)}]: {Message}";
        }
    }
}

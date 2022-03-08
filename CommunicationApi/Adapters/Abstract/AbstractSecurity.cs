using ExpertAdvisors.Model.Orders;
using Microsoft.AspNetCore.Http;
using System;
using static ExpertAdvisors.Model.Orders.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractSecurity
    {
        // Attribute
        protected Action<string> OnTick = delegate { };
        protected AbstractAerial _receiveTransmit;
        public AbstractAerial ReceiveTransmit
        {
            get
            {
                return _receiveTransmit;
            }
            set
            {
                if (_receiveTransmit == null)
                {
                    _receiveTransmit = value;
                    _receiveTransmit.AddEventHandlerOnTick(OnTickHandler);
                }
                else
                {
                    throw new AbstractLayerException(AbstractLayerException.ERROR_CODES.LAYER_WAS_ALREADY_SET);
                }
            }
        }

        // Abstrakt
        abstract protected bool Validate(HttpContext httpContext);
        abstract protected string SecurityStrategy(); // StrategyPattern        

        // Hilfsmethoden
        protected void OnTickHandler(string tick, HttpContext httpContext)
        {
            if(Validate(httpContext) == true)
            {
                OnTick(tick);
            }
            else
            {
                throw new SecurityException(SecurityException.ERROR_CODE.SECURITY_VALIDATION_FAILED, "Unauthorisierter Zugriff");
            }
        }
        public void AddEventHandlerOnTick(Action<string> meth)
        {
            OnTick += meth;
        }
        public AbstractOrder OpenOrder(DIRECTION direction, double volume, double openPrice)
        {
            var token = SecurityStrategy();
            return ReceiveTransmit.OpenOrder(direction, volume, openPrice, token);
        }
        public AbstractOrder CloseOrder(AbstractOrder order)
        {
            var token = SecurityStrategy();
            return ReceiveTransmit.CloseOrder(order, token);
        }
        public AbstractOrder OrderStatus(AbstractOrder order)
        {
            var token = SecurityStrategy();
            return ReceiveTransmit.OrderStatus(order, token);
        }
    }

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

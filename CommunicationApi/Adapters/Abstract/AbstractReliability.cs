using ExpertAdvisors.Model.Orders;
using System;
using System.Text.RegularExpressions;
using static ExpertAdvisors.Model.Orders.AbstractOrder;

namespace CommunicationApi.Adapters.Abstract
{
    public abstract class AbstractReliability
    {
        // Attribute              
        private string TickRegexPattern;
        protected Action<Match> OnTick = delegate { };        
        protected AbstractSecurity _security; 
        public AbstractSecurity Security
        {
            get
            {
                return _security;                
            }
            set
            {
                if(_security == null)
                {
                    _security = value;
                    _security.AddEventHandlerOnTick(OnTickHandler);
                }
                else
                {
                    throw new AbstractLayerException(AbstractLayerException.ERROR_CODES.LAYER_WAS_ALREADY_SET);
                }
            }
        }

        // Abstrakt
        abstract protected bool ReliabilityStrategy(); // StrategyPattern

        // Konstruktor
        protected AbstractReliability(string tickRegexPattern)
        {            
            
            TickRegexPattern = tickRegexPattern;
        }

        // Hilfsmethoden
        protected void OnTickHandler(string tick)
        {
            Match result = new Regex(TickRegexPattern).Match(tick);
            if (result.Success == true)
            {                
                OnTick(result);
            }
            else
            {
                throw new ReliabilityException(ReliabilityException.ERROR_CODE.INCOMING_VALIDATION_FAILED);
            }
        }

        // Methoden
        public void AddEventHandlerOnTick(Action<Match> meth)
        {
            OnTick += meth;
        }

        public void RemoveEventHandlerOnTick(Action<Match> meth)
        {
            OnTick -= meth;
        }

        public bool OpenOrder(bool direction, double volume, double openPrice)
        {
            if (ReliabilityStrategy() == true)
            {
                var order = Security.OpenOrder(direction, volume, openPrice);

                if (order.TradingPair.Equals(pair) == false)
                {
                    throw new ReliabilityException(ReliabilityException.ERROR_CODE.INVALID_ORDER_PAIR_TO_ORDERBOOK);
                }

                return order;
            }
            else
            {
                throw new ReliabilityException(ReliabilityException.ERROR_CODE.EXECUTING_FAILED);
            }
        }

        public AbstractOrder CloseOrder(AbstractOrder order)
        {
            if (ReliabilityStrategy() == true)
            {
                return Security.CloseOrder(order);
            }
            else
            {
                throw new ReliabilityException(ReliabilityException.ERROR_CODE.EXECUTING_FAILED);
            }
        }      
        
        public AbstractOrder OrderStatus(AbstractOrder order)
        {
            if (ReliabilityStrategy() == true)
            {
                return Security.OrderStatus(order);
            }
            else
            {
                throw new ReliabilityException(ReliabilityException.ERROR_CODE.EXECUTING_FAILED);
            }
        }
    }

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

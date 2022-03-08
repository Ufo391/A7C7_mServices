using System;

namespace TestPackages.Bookkeepings
{
    public class AbstractOrder
    {
        public enum ORDER_STATUS
        {
            INITIALIZED, OPEN, CLOSED, PENDING
        }

        public enum DirectionType
        {
            BEARISCH, BULLISH
        }

        // Konstruktor
        public AbstractOrder(string id, string market, ORDER_STATUS status, DirectionType direction, 
            DateTime openDate, double volume, double openPrice, string pair)
        {
            ExternId = id;
            Status = status;
            Direction = direction;
            OpenDate = openDate;
            Volume = volume;
            OpenPrice = openPrice;            
            TradingPair = pair;
            Market = market;
            InternalId = Guid.NewGuid();
        }

        // Attribute
        public string ExternId { get; private set; } // Z.B. ID von MetaTrader
        public Guid InternalId { get; private set; }

        private ORDER_STATUS status;
        public ORDER_STATUS Status 
        {
            get
            {
                return status;
            }
            set
            {
                if(status == ORDER_STATUS.CLOSED)
                {                    
                    throw new NotImplementedException("Order war schon geschlossen und kann nicht noch mal verändert werden!");
                }
                if(value == ORDER_STATUS.CLOSED)
                {
                    CloseDate = new DateTime(DateTime.Now.Ticks);
                }
                status = value;
            }
        }
        public string Market { get; private set; }
        public DirectionType Direction { get; private set; }
        public DateTime OpenDate { get; private set; }
        public DateTime CloseDate { get; private set; }
        public double Volume { get; private set; }
        public double OpenPrice { get; private set; } // Menge die nötig war um die Order zu öffnen
        public double ClosePrice { get; private set; }        
        public double StopLoss { get; set; }
        public double TakeProfit { get; set; }
        public string TradingPair { get; private set; }

        // Funktionen
        public override string ToString()
        {
            return $"[{ExternId}] | OpenDate: {OpenDate} | {Enum.GetName(typeof(ORDER_STATUS), Status)} | {Market} | {Enum.GetName(typeof(DirectionType), Direction)} | OpenPrice: {OpenPrice} | Pair: {TradingPair}";
        }

        public override bool Equals(object obj)
        {
            if(obj is AbstractOrder == false)
            {
                return false;
            }
            var order = (AbstractOrder)obj;
            return order.OpenDate.Equals(OpenDate) && order.ExternId.Equals(ExternId) && order.Market.Equals(Market) && order.Direction.Equals(Direction) && order.TradingPair.Equals(TradingPair) && order.OpenPrice == OpenPrice;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 126345;                
                result *= OpenDate.GetHashCode() + 4321;
                result *= ExternId.GetHashCode() + 4321;
                result *= Market.GetHashCode() + 4321;
                result *= Direction.GetHashCode() + 4321;
                result *= TradingPair.GetHashCode() + 4321;
                result *= OpenPrice.GetHashCode() + 4321;
                return result;
            }
        }

        // Methoden
        public void RefreshOrder(AbstractOrder order)
        {
            OpenDate = order.OpenDate;
            CloseDate = order.CloseDate;
            OpenPrice = order.OpenPrice;            
            ClosePrice = order.ClosePrice;
            StopLoss = order.StopLoss;
            TakeProfit = order.TakeProfit;
            Status = order.Status;
        }
    }
}

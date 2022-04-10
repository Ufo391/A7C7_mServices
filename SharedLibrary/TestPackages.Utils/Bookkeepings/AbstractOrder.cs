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

        // Constructor
        public AbstractOrder(string id, string market, ORDER_STATUS status, DirectionType direction, 
            DateTime openDate, double volume, double openPrice)
        {
            ExternId = id;
            Status = status;
            Direction = direction;
            OpenDate = openDate;
            Volume = volume;
            OpenPrice = openPrice;            
            Market = market;
            InternalId = Guid.NewGuid();
        }

        public AbstractOrder()
        {

        }

        // Attributes
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

        // Functions
        public override string ToString()
        {
            return $"[{ExternId}] | OpenDate: {OpenDate} | {Enum.GetName(typeof(ORDER_STATUS), Status)} | {Market} | {Enum.GetName(typeof(DirectionType), Direction)} | OpenPrice: {OpenPrice}";
        }

        // Methods
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

        public override bool Equals(object? obj)
        {
            return obj is AbstractOrder order &&
                   ExternId == order.ExternId &&
                   InternalId.Equals(order.InternalId) &&
                   status == order.status &&
                   Status == order.Status &&
                   Market == order.Market &&
                   Direction == order.Direction &&
                   OpenDate == order.OpenDate &&
                   CloseDate == order.CloseDate &&
                   Volume == order.Volume &&
                   OpenPrice == order.OpenPrice &&
                   ClosePrice == order.ClosePrice &&
                   StopLoss == order.StopLoss &&
                   TakeProfit == order.TakeProfit;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(ExternId);
            hash.Add(InternalId);
            hash.Add(status);
            hash.Add(Status);
            hash.Add(Market);
            hash.Add(Direction);
            hash.Add(OpenDate);
            hash.Add(CloseDate);
            hash.Add(Volume);
            hash.Add(OpenPrice);
            hash.Add(ClosePrice);
            hash.Add(StopLoss);
            hash.Add(TakeProfit);
            return hash.ToHashCode();
        }
    }
}

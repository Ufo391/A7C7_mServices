using Communication.FastProtocol.Read;
using CoreApi.Model;
using ExpertAdvisors.Abstract;
using ExpertAdvisors.Abstract.Strategy;
using ExpertAdvisors.Model.Broker;
using ExpertAdvisors.Model.Orders;
using ExpertAdvisors.Model.Orders.Calculation;
using System;
using System.Collections.Generic;
using static ExpertAdvisors.Model.Orders.AbstractOrder;
using AbstractOrder = ExpertAdvisors.Model.Orders.AbstractOrder;

namespace ExpertAdvisors
{
    public class Head
    {
        // Attribute
        public Guid Id { get; private set; }

        public BrokerAccount Broker { get; private set; }
        public Pair Pair { get; private set; } // Traded Pair

        private Action<AbstractReader> OnTick = delegate { };
        private AccountProfitConverter AccountProfitConverter;

        // Konstruktor
        public Head(SessionModel sessionModel, BrokerAccount broker, Pair pair)
        {
            Id = Guid.NewGuid();
            Broker = broker;
            Pair = pair;
        }

        // Destruktor
        ~Head()
        {

        }                

        protected bool OpenOrder(bool isBullish, double volume, double openPrice)
        {
            if (Reliability.OpenOrder(isBullish, volume, openPrice) == true) 
            {
                hier weiter
            Orderbook.InsertOrder(order);
                return true;
            }
            
            return false;
        }
        protected void CloseOrder(AbstractOrder order)
        {
            Reliability.CloseOrder(order);
            Orderbook.GetOrderByGuid(order.InternalId).RefreshOrder(order);
        }
        protected void CloseOrder(Guid id)
        {
            CloseOrder(Orderbook.GetOrderByGuid(id));
        }
        protected AbstractOrder OrderStatus(AbstractOrder order)
        {
            // Gibt die aktuellste Version der Order wieder (ohne GUID)
            var latestOrder = Reliability.OrderStatus(order);
            Orderbook.GetOrderById(order.ExternId).RefreshOrder(latestOrder); // Order aktualisieren
            return Orderbook.GetOrderById(order.ExternId);
        }
        protected AbstractOrder OrderStatus(Guid id)
        {
            return OrderStatus(Orderbook.GetOrderByGuid(id));
        }

    }
}

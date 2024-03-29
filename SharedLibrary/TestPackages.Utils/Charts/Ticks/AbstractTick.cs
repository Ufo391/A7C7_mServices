﻿using System.Text.RegularExpressions;

namespace TestPackages.Utils.Charts.Ticks
{
    public abstract class AbstractTick
    {
        // Attributes
        protected Match Data;
        public DateTime TimeStamp { get; private set; }
        public string TickRaw { get; private set; }

        // Constructor
        public AbstractTick(string tick)
        {
            TimeStamp = DateTime.Now;
            TickRaw = tick;
            Data = ValidateTick(tick, GetTickValidationRegex());
        }

        // Abstract        
        public abstract Regex GetTickValidationRegex();
        public abstract Match ValidateTick(string tick, Regex regex);

        public abstract Guid TraderHeadId
        {
            get;
        }

        public abstract string Symbol
        {
            get;
        }

        public abstract DateTime Date
        {
            get;
        }

        public abstract float Bid
        {
            get;
        }

        public abstract float Ask
        {
            get;
        }

        public override bool Equals(object? obj)
        {
            return obj is AbstractTick tick &&
                   EqualityComparer<Match>.Default.Equals(Data, tick.Data) &&
                   TimeStamp == tick.TimeStamp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Data, TimeStamp);
        }
    }
}

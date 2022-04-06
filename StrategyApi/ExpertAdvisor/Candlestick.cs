using Communication.FastProtocol.Read;
using System;

namespace ExpertAdvisors
{
    public class Candlestick
    {
        private static uint IDCounter = 0;

        // Attribute
        public uint ID { get; private set; }
        public ulong Intervall { get; private set; }
        public float OpenAsk { get; private set; }
        public float OpenBid { get; private set; }
        public float CloseAsk { get; private set; }
        public float CloseBid { get; private set; }
        public float HighAsk { get; private set; }
        public float HighBid { get; private set; }
        public float LowAsk { get; private set; }
        public float LowBid { get; private set; }
        public DateTime OpenDate { get; set; }
        public DateTime CloseDate { get; private set; }
        public bool IsReadOnly { get; private set; }
        public Tick LastTick { get; private set; }

        private Action<Candlestick, Tick> OnCandleClosed = delegate { };

        // Kosntruktor
        public Candlestick(ulong intervall)
        {
            Intervall = intervall;
            IsReadOnly = false;
            ID = IDCounter;
            IDCounter++;
        }

        // Hilfsmethoden
        public void AddOnCandleClosedHandler(Action<Candlestick, Tick> meth)
        {
            OnCandleClosed += meth;
        }

        public void OnTickHandler(AbstractReader aReader)
        {
            return; // Erstmal deaktiviert MUSS ÜBERARBEITET WERDEN siehe TODOS
            /*
            var tick = (Tick)aReader;

            if (IsReadOnly == true)
            {
                // Kerze ist eigentlich schon geschlossen & kann eig. nicht nochmal betreten werden
                throw new NotImplementedException("NICHT IMPLEMENTIERT!");
            }

            if (OpenDate == DateTime.MinValue)
            {
                OpenCandle(tick);
            }

            SetLowest(tick);
            SetHeighest(tick);

            if (IsIntervallOver(tick) == true)
            {
                CloseCandle(tick);
            }

            LastTick = tick;*/
        }

        private bool IsIntervallOver(Tick tick)
        {
            return tick.Date.Subtract(OpenDate).TotalMilliseconds >= Intervall;
        }

        private void CloseCandle(Tick ignoredTick)
        {
            CloseAsk = LastTick.Ask;
            CloseBid = LastTick.Bid;
            CloseDate = new DateTime(OpenDate.Ticks);
            CloseDate = CloseDate.AddMilliseconds(Intervall);
            IsReadOnly = true;
            OnCandleClosed(this, ignoredTick);
        }

        private void OpenCandle(Tick tick)
        {
            OpenAsk = tick.Ask;
            OpenBid = tick.Bid;
            OpenDate = new DateTime(tick.Date.Ticks);
            HighAsk = float.MinValue;
            HighBid = float.MinValue;
            LowAsk = float.MaxValue;
            LowBid = float.MaxValue;
        }

        private void SetLowest(Tick tick)
        {
            if (LowAsk > tick.Ask)
            {
                LowAsk = tick.Ask;
            }

            if (LowBid > tick.Bid)
            {
                LowBid = tick.Bid;
            }
        }

        private void SetHeighest(Tick tick)
        {
            if (HighAsk < tick.Ask)
            {
                HighAsk = tick.Ask;
            }

            if (HighBid < tick.Bid)
            {
                HighBid = tick.Bid;
            }
        }

        // Override
        public override string ToString()
        {
            return $"{ID} => Intervall: {Intervall} | OpenDate: {OpenDate} | CloseDate: {CloseDate}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Candlestick == false)
            {
                return false;
            }

            var castedObj = (Candlestick)obj;

            return castedObj.OpenDate.Equals(OpenDate) && castedObj.CloseDate.Equals(CloseDate) && castedObj.ID.Equals(ID) && castedObj.IsReadOnly.Equals(IsReadOnly);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 187124;
                result *= OpenDate.GetHashCode();
                result *= CloseDate.GetHashCode();
                result *= ID.GetHashCode();
                result *= IsReadOnly.GetHashCode();
                return result;
            }
        }
    }
}

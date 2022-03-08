using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TestPackages.Utils.Charts
{
    public abstract class AbstractTick
    {
        // Attributes
        private enum ENUM_OBJ_INDEX
        {
            NULL, // Beim Matchobjekt ist der erste Eintrag immer der gesamte Match -> deshalb Platzhalter
            GUID,
            SYMBOL,
            DAY_MONTH_YEAR,
            HOUR_MIN_SEC,
            BID,
            ASK,
            LAST_PRICE,
            LAST_PRICE_VOLUME,
            LAST_PRICE_TIME,
            FLAG,
            REAL_VOLUME,
            BALANCE,
            CREDIT,
            PROFIT,
            EQUITY,
            MARGIN,
            MARGIN_FREE,
            MARGIN_LEVEL,
            MARGIN_CALL,
            MARGIN_STOP_OUT
        }

        private static Dictionary<ENUM_METATRADER_TICK_FLAG, string> MapTickFlags = new Dictionary<ENUM_METATRADER_TICK_FLAG, string>()
        {
            {ENUM_METATRADER_TICK_FLAG.ASK, "Ask" },
            {ENUM_METATRADER_TICK_FLAG.BID, "Bid" },
            {ENUM_METATRADER_TICK_FLAG.BUY, "Buy" },
            {ENUM_METATRADER_TICK_FLAG.LAST, "Last" },
            {ENUM_METATRADER_TICK_FLAG.SELL, "Sell" },
            {ENUM_METATRADER_TICK_FLAG.VOLUME, "Volume" }
        };

        private enum ENUM_METATRADER_TICK_FLAG
        {
            BID = 1, ASK = 2, LAST = 3, VOLUME = 4, BUY = 5, SELL = 6
        }

        private static readonly string RegexFastProtocolTickDate = @"(\d+)\.(\d+)\.(\d+)\|(\d+):(\d+):(\d+)";

        private Match Data;
        public DateTime TimeStamp { get; private set; }

        // Konstruktor
        public AbstractTick(Match matchData)
        {
            TimeStamp = DateTime.Now;
            Data = matchData;
            EAId = Guid.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.GUID].Value);

            if (MapTickFlags.ContainsKey((ENUM_METATRADER_TICK_FLAG)Enum.ToObject(typeof(ENUM_METATRADER_TICK_FLAG), FlagCode)) == false)
            {
                throw new TickException(TickException.ERROR_CODE.FLAG_CODE_NOT_EXISTS);
            }
        }

        // Methoden        
        public override bool Equals(object obj)
        {
            if (obj is Tick == false)
            {
                return false;
            }

            var instance = (Tick)obj;
            return instance.TimeStamp.Equals(TimeStamp) && instance.GetDataHashCode() == GetDataHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 1265;
                result *= Data.GetHashCode() + 12354;
                result *= TimeStamp.GetHashCode() + 12354;
                return result;
            }
        }

        private Guid _EAId;
        public Guid EAId
        {
            get
            {
                return _EAId;
            }
            private set
            {
                _EAId = value;
            }
        }

        // GET
        public string Symbol
        {
            get
            {
                return Data.Groups[(int)ENUM_OBJ_INDEX.SYMBOL].Value;
            }
        }

        public DateTime Date
        {
            get
            {
                string strDate = $"{Data.Groups[(int)ENUM_OBJ_INDEX.DAY_MONTH_YEAR].Value}|{Data.Groups[(int)ENUM_OBJ_INDEX.HOUR_MIN_SEC].Value}";
                // Datum konvertieren
                Match match = Regex.Match(strDate, RegexFastProtocolTickDate);
                return new DateTime(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[1].Value), int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value));
            }
        }

        public float Bid
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.BID].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Ask
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.ASK].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float LastPrice
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.LAST_PRICE].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float LastPriceVolume
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.LAST_PRICE_VOLUME].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public DateTime LastPriceTime
        {
            get
            {
                return new DateTime(long.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.LAST_PRICE_TIME].Value));
            }
        }

        public int FlagCode
        {
            get
            {
                return int.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.FLAG].Value);
            }
        }

        public string Flag
        {
            get
            {
                return MapTickFlags[(ENUM_METATRADER_TICK_FLAG)FlagCode];
            }
        }

        public float RealVolume
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.REAL_VOLUME].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Balance
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.BALANCE].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Credit
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.CREDIT].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Profit
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.PROFIT].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Equity
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.EQUITY].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float Margin
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.MARGIN].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float MarginFree
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.MARGIN_FREE].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float MarginLevel
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.MARGIN_LEVEL].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float MarginCall
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.MARGIN_CALL].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public float MarginStopOut
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.MARGIN_STOP_OUT].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public int GetDataHashCode()
        {
            return Data.GetHashCode();
        }
    }
}

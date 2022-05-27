using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestPackages.Utils.Charts.Ticks
{
    public class MetaTraderForexTick : AbstractTick
    {
        // Enums
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
        private enum ENUM_METATRADER_TICK_FLAG
        {
            BID = 1, ASK = 2, LAST = 3, VOLUME = 4, BUY = 5, SELL = 6
        }

        // Attributes
        private static readonly string RegexFastProtocolTickDate = @"(\d+)\.(\d+)\.(\d+)\|(\d+):(\d+):(\d+)";
        private static Dictionary<ENUM_METATRADER_TICK_FLAG, string> MapTickFlags = new Dictionary<ENUM_METATRADER_TICK_FLAG, string>()
        {
            {ENUM_METATRADER_TICK_FLAG.ASK, "Ask" },
            {ENUM_METATRADER_TICK_FLAG.BID, "Bid" },
            {ENUM_METATRADER_TICK_FLAG.BUY, "Buy" },
            {ENUM_METATRADER_TICK_FLAG.LAST, "Last" },
            {ENUM_METATRADER_TICK_FLAG.SELL, "Sell" },
            {ENUM_METATRADER_TICK_FLAG.VOLUME, "Volume" }
        };

        // Constructor
        public MetaTraderForexTick(string tick) : base(tick)
        {
        }

        public override Regex GetTickValidationRegex()
        {
            throw new NotImplementedException();
        }

        public override Match ValidateTick(string tick, Regex regex)
        {
            throw new NotImplementedException();
        }

        // GET
        public override Guid TraderHeadId
        {
            get
            {
                return Guid.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.GUID].Value);
            }
        }

        public override string Symbol
        {
            get
            {
                return Data.Groups[(int)ENUM_OBJ_INDEX.SYMBOL].Value;
            }
        }

        public override DateTime Date
        {
            get
            {
                string strDate = $"{Data.Groups[(int)ENUM_OBJ_INDEX.DAY_MONTH_YEAR].Value}|{Data.Groups[(int)ENUM_OBJ_INDEX.HOUR_MIN_SEC].Value}";
                // Datum konvertieren
                Match match = Regex.Match(strDate, RegexFastProtocolTickDate);
                return new DateTime(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[1].Value), int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value));
            }
        }

        public override float Bid
        {
            get
            {
                return float.Parse(Data.Groups[(int)ENUM_OBJ_INDEX.BID].Value, CultureInfo.InvariantCulture.NumberFormat);
            }
        }

        public override float Ask
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
    }
}

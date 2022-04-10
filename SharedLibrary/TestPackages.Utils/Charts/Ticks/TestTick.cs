﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestPackages.Utils.Charts.Ticks
{
    public class TestTick : AbstractTick
    {
        // Attributes
        public Guid DebugHeadId { get; set; }
        public string DebugSymbol { get; set; }
        public DateTime DebugDate { get; set; }
        public float DebugBid { get; set; }
        public float DebugAsk { get; set; }

        // Constructor
        public TestTick(string tick) : base(tick)
        {
        }

        // Methods
        public override Guid HeadId => DebugHeadId;

        public override string Symbol => DebugSymbol;

        public override DateTime Date => DebugDate;

        public override float Bid => DebugBid;

        public override float Ask => DebugAsk;

        public override Regex GetTickValidationRegex()
        {
            return new Regex(".*");
        }

        public override Match ValidateTick(string tick, Regex regex)
        {
            return regex.Match(tick);
        }
    }
}

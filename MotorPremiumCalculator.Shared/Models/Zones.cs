using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPremiumCalculator.Shared.Models
{
    public class Zones
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public string ZoneName { get; set; }
    }
}

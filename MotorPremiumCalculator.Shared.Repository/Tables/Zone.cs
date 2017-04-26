using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MotorPremiumCalculator.Shared.Repository.Tables
{
    public class Zone
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public string ZoneName { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPremiumCalculator.Shared.Repository.Tables
{
    public class FourWheelerUpto6Passenger
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public int ZoneId { get; set; }
        public int CCId { get; set; }
        public int VechicleAgeId { get; set; }
        public int ActPremium { get; set; }
        public double ComprehensivePremium { get; set; }
    }
}

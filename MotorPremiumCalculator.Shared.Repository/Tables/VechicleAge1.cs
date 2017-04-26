using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPremiumCalculator.Shared.Repository.Tables
{
    public class VechicleAge1
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPremiumCalculator.Shared.Repository.Tables
{
    public class PrivateCarCC
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public int CCMin { get; set; }
        public int CcMax { get; set; }
    }
}

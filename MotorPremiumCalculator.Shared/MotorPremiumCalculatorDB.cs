using MotorPremiumCalculator.Shared.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotorPremiumCalculator.Shared
{
    public class MotorPremiumCalculatorDB : SQLiteConnection
    {
        static object syncObj = new object();
        public MotorPremiumCalculatorDB(string path) : base(path)
        {
            //Create tables
            CreateTable<Zones>();
        }
    }
}

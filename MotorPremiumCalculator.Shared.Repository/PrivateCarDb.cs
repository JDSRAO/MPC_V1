using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using MotorPremiumCalculator.Shared.Repository.Tables;

namespace MotorPremiumCalculator.Shared.Repository
{
    public class PrivateCarDb : SQLiteConnection
    {
        static object syncObj = new object();
        public PrivateCarDb(string path) : base(path)
        {

        }

        public PrivateCar GetPrivateCarActData(string zoneName,int cc)
        {
            int zoneId = GetZoneId(zoneName);
            int ccId = GetPrivateCarCcId(cc);
            //PrivateCar privateCarData = this.Get<PrivateCar>(row => row.ZoneId == zoneId && row.CCId == ccId);
            PrivateCar privateCarData = this.Get<PrivateCar>(row => row.CCId == ccId);
            return privateCarData;
        }

        public int GetZoneId(string zoneName)
        {
            return this.Get<Zone>(name => name.ZoneName.Equals(zoneName)).ID;
        }

        public int GetPrivateCarCcId(int cc)
        {
            return this.Get<PrivateCarCC>(name => name.CCMin <= cc && name.CcMax >= cc).ID;
        }
    }
}

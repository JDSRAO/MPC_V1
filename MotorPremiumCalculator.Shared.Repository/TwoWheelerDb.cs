using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using MotorPremiumCalculator.Shared.Repository.Tables;
using System.IO;

namespace MotorPremiumCalculator.Shared.Repository
{
    public class TwoWheelerDb : SQLiteConnection
    {
        static object syncObj = new object();
        public TwoWheelerDb(string path) : base(path)
        {
            //CreateTables();
            //InsertData();
        }

        public TwoWheeler GetTwoWheelerData(string zoneName,string cc, int vechicleAge, bool isPaToDriver, bool isPaToPillionRider )
        {
            lock (syncObj)
            {
                var result = CustomSQLExecution();
                int zoneId = GetZoneId(zoneName);
                int ccId = GetTwoWheelerCcId(cc);
                int vechicleAgeId = GetVechicleAge1Id(vechicleAge);
                var actTwowheeler = this.Get<TwoWheeler>(act => act.ZoneId == zoneId && act.CCId == ccId && act.VechicleAgeId == vechicleAgeId);
                return actTwowheeler;
            }
        }

        private List<TwoWheeler> CustomSQLExecution()
        {
            List<TwoWheeler> data = this.Query<TwoWheeler>("SELECT * FROM TwoWheeler INNER JOIN Zone ON TwoWheeler.ZoneId= Zone._id WHERE Zone.ZoneName = ?","A");
            var cmd = NewCommand();
            cmd.CommandText = "SELECT * FROM TwoWheeler INNER JOIN Zone ON TwoWheeler.ZoneId= Zone._id WHERE Zone.ZoneName = ?";
            cmd.Bind("A");
            List<TwoWheeler> result = cmd.ExecuteQuery<TwoWheeler>();
            return result;
        }

        public int GetZoneId(string zoneName)
        {
            lock (syncObj)
            {
                return this.Get<Zone>(name => name.ZoneName.Equals(zoneName)).ID;
            }
        }
        public int GetTwoWheelerCcId(string selectedCc)
        {
            lock (syncObj)
            {
                int cc = Convert.ToInt32(selectedCc);
                //string cmd = "SELECT * FROM 'TwoWheelerCC' WHERE [CCMin] <= ? AND [CcMax] >= ?";
                //SQLiteCommand command = this.CreateCommand(cmd,cc,cc);
                //var a =  command.ExecuteScalar<TwoWheelerCC>();
                return this.Get<TwoWheelerCC>(name => name.CCMin <= cc && name.CcMax >= cc).ID;
            }
        }
        public int GetVechicleAge1Id(int vechicleAge )
        {
            lock (this)
            {
                return this.Get<VechicleAge1>(age => age.AgeMin <= vechicleAge && age.AgeMax >= vechicleAge).ID;
            }
        }

        #region Data insertion
        private void InsertData()
        {
            InsertZoneData();
            InsertVechicleAge1Data();
            InsertVechicleAge2Data();
            InsertTwoWheelerCcData();
        }

        private void InsertTwoWheelerCcData()
        {
            lock (syncObj)
            {
                this.Insert(new TwoWheelerCC { CCMin = 0, CcMax = 150 });
                this.Insert(new TwoWheelerCC { CCMin = 151, CcMax = 350 });
                this.Insert(new TwoWheelerCC { CCMin = 351, CcMax = 1000 });
            }
        }

        private void InsertZoneData()
        {
            lock (syncObj)
            {
                this.Insert(new Zone { ZoneName = "A" });
                this.Insert(new Zone { ZoneName = "B" });
                this.Insert(new Zone { ZoneName = "C" });
            }
        }

        private void InsertVechicleAge1Data()
        {
            lock (syncObj)
            {
                this.Insert(new VechicleAge1 { AgeMin = 0, AgeMax = 5});
                this.Insert(new VechicleAge1 { AgeMin = 5, AgeMax = 10 });
                this.Insert(new VechicleAge1 { AgeMin = 10, AgeMax = 100 });
            }
        }

        private void InsertVechicleAge2Data()
        {
            lock (syncObj)
            {
                this.Insert(new VechicleAge2 { AgeMin = 0, AgeMax = 5 });
                this.Insert(new VechicleAge2 { AgeMin = 5, AgeMax = 7 });
                this.Insert(new VechicleAge2 { AgeMin = 7, AgeMax = 100 });
            }
        }
        #endregion

        #region Table Creation
        private void CreateTables()
        {
            CreateTable<Zone>();
            CreateTable<VechicleAge1>();
            CreateTable<VechicleAge2>();
            CreateTable<TwoWheelerCC>();
            CreateTable<PrivateCarCC>();
            CreateTable<TwoWheeler>();
        }
        #endregion
    }
}

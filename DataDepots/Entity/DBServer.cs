using DataDepots.Define;
using DataDepots.Util;
using System.Collections.Generic;

namespace DataDepots
{
    public class DBServer
    {
        public string IP { get; set; }

        public Dictionary<string, Database> DataBase = new Dictionary<string, Database>();

        internal DBServer AddDataBase()
        {

            var dbDefines = Depots.iContainer.GetServices<IDataBaseDefine>();
            foreach (var dbDefine in dbDefines)
            {
                this.DataBase.Add(dbDefine.Database.Name, dbDefine.Database);
                dbDefine.Database.AddTable();
            }

            return this;
        }

    }
}

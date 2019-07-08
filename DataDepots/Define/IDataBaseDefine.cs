using DataDepots.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots.Define
{
    public interface IDatabaseDefine
    {
        Database Database { get; set; }

        void Create(string dbName);

        void CheckExists(ExecContext context);

    }
}

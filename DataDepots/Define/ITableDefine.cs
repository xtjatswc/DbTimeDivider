using DataDepots.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataDepots.Define
{
    public interface ITableDefine
    {
        void CheckExists(ExecContext context);

        Table Table { get; set; }

        void Create(string tableName);

    }
}

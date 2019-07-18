using DbTimeDivider.Entity;
using System;

namespace DbTimeDivider.Schema
{
    public interface ITableSchema
    {
        void CheckExists(DivisionContext context);

        Table Table { get; set; }

        void Create(TablePack tablePack);

        string GenerateEntity();

    }
}

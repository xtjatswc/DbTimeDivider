using DbTimeDivider.Entity;

namespace DbTimeDivider.Schema
{
    public interface ITableSchema
    {
        void CheckExists(DivisionContext context);

        Table Table { get; set; }

        void Create(string tableName);

    }
}

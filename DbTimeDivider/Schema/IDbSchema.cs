using DbTimeDivider.Entity;

namespace DbTimeDivider.Schema
{
    public interface IDbSchema
    {
        Database Database { get; set; }

        void Create(string dbName);

        void CheckExists(DivisionContext context);

    }
}

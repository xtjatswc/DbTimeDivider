using DbTimeDivider.Entity;

namespace DbTimeDivider.Schema
{
    public interface IDbSchema
    {
        Database Database { get; set; }

        void Create(QueryItem queryItem);

        void CheckExists(DivisionContext context);

        QueryItem DefaultQueryItem { get; }
    }
}

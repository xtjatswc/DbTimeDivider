using DbTimeDivider.Entity;
using System.Collections.Generic;

namespace DbTimeDivider.Schema
{
    public abstract class AbsTableSchema<BelongToClass> : ITableSchema
    {
        public Table Table { get; set; }
        private List<string> _isExists = new List<string>();

        public AbsTableSchema()
        {
            Table = new Table
            {
                Database = (TimeDivider.GetService<BelongToClass>() as IDbSchema).Database,
                ITableSchema = this
            };
            Define();
        }

        protected abstract void Define();

        public abstract void Create(string tableName);

        public void CheckExists(DivisionContext context)
        {
            foreach (var particleSet in context.Particles)
            {
                string existsKey = $"{particleSet.DatabaseName}=>{particleSet.TableName}";

                if (_isExists.Contains(existsKey))
                    continue;

                Table.Database.DBProvider.CurrentParticleSet = particleSet;
                if (Table.Database.DBProvider.IsTableExists(particleSet.TableName))
                {
                    _isExists.Add(existsKey);
                    continue;
                }

                Create(particleSet.TableName);
                _isExists.Add(existsKey);
            }

        }
    }
}

﻿using DbTimeDivider.Entity;
using DbTimeDivider.IFace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbTimeDivider.Schema
{
    public abstract class AbsDbSchema<BelongToClass, DBProviderClass> : IDbSchema
    {

        public Type BelongTo => typeof(BelongToClass);

        public Database Database { get; set; }

        private List<string> _isExists = new List<string>();

        public AbsDbSchema()
        {
            Database = new Database
            {
                DbHost = (TimeDivider.GetService<BelongToClass>() as AbsDbHostSchema).DbHost,
                DBProvider = TimeDivider.GetService<DBProviderClass>() as AbsDBProvider,
                IDbSchema = this
            };
            Define();
        }

        protected abstract void Define();

        public abstract void Create(string dbName);

        public void CheckExists(DivisionContext context)
        {
            var dbNames = context.Particles.Select(o => o.DatabaseName).Distinct();
            Database.DBProvider.CurrentParticleSet = new ParticleSet() { DatabaseName = "master" };

            foreach (var dbName in dbNames)
            {
                if (_isExists.Contains(dbName))
                    continue;

                if (Database.DBProvider.IsDatabaseExists(dbName))
                {
                    _isExists.Add(dbName);
                    continue;
                }

                Create(dbName);
                _isExists.Add(dbName);

            }
        }
    }
}
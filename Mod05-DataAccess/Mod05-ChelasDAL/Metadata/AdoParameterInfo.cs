﻿namespace Mod05_ChelasDAL.Metadata
{
    using System.Data;

    public class AdoParameterInfo
    {
        public DbType DbType { get; private set; }
        public string Name { get; private set; }
        public object Value { get; private set; }

        public AdoParameterInfo(string name, DbType dbType, object value)
        {
            Name = name;
            DbType = dbType;
            Value = value;
        }
    }
}
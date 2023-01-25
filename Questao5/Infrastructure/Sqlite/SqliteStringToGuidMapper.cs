﻿using System.Data;
using Dapper;

namespace Questao5.Infrastructure.Sqlite;

public class SqliteStringToGuidMapper : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value)
    {
        return new Guid((string)value);
    }
}
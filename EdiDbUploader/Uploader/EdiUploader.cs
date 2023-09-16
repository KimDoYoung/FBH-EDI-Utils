using FBH.EDI.Common.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;

namespace EdiDbUploader
{
    /// <summary>
    /// 각 문서의 Uploader의 부모 클래스
    /// </summary>
    public class EdiUploader
    {
        private string connectionString;
        private NpgsqlConnection connection;

        public string GetConnectionString()
        {
            return connectionString;
        }
        public void SetConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected NpgsqlConnection OpenConnection()
        {
            if (connection == null)
            {
                connection = new NpgsqlConnection(GetConnectionString());
            }
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            return connection;
        }

        protected NpgsqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new NpgsqlConnection(GetConnectionString());
            }
            return connection;
        }
        public NpgsqlTransaction BeginTransaction()
        {
            return OpenConnection().BeginTransaction();
        }
        public object ExecuteScalar(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand(sql);
            command.Connection = OpenConnection();
            return command.ExecuteScalar();
        }
        public virtual string Insert(EdiDocument ediDoc)
        {
            return "";
        }

        public virtual List<String> Insert(List<EdiDocument> ediDocumentList)
        {
            return null;
        }

        protected NpgsqlParameter NewSafeParameter(string parameterName, object data) 
        {
            if (data == null)
            {
                return new NpgsqlParameter() { ParameterName = parameterName, Value = DBNull.Value };
            } else if (data is int)
            {
                return new NpgsqlParameter() { ParameterName = parameterName, NpgsqlDbType = NpgsqlDbType.Numeric, Value = data };
            } else if (data is decimal)
            {
                return new NpgsqlParameter() { ParameterName = parameterName, NpgsqlDbType = NpgsqlDbType.Numeric, Value = data };
            } else if (data is DateTime) {
                return new NpgsqlParameter()  { ParameterName = parameterName, NpgsqlDbType = NpgsqlDbType.Date,  Value = data  };
            }
            else
            {
                return new NpgsqlParameter() { ParameterName = parameterName, NpgsqlDbType = NpgsqlDbType.Varchar, Value = data };
            }
        }
    }
}

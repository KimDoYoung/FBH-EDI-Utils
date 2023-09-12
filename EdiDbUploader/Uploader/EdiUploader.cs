using FBH.EDI.Common.Model;
using Npgsql;
using System;
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

        protected string GetConnectionString()
        {
            return connectionString;
        }
        protected void SetConnectionString(string connectionString)
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
        public NpgsqlTransaction BeginTransaction ()
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
    }
}
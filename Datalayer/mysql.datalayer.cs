using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Leaf.Data
{
    public class DataLayerBase : IDisposable
    {
        public struct Parameters
        {
            public object Value;
            public string Name;
            public DbType Type;


            public Parameters(string name, DbType type, object value)
            {
                Value = value;
                Name = name;
                Type = type;
            }


            public int GetDriverType()
            {

                switch (Type)
                {
                    case DbType.AnsiString:
                    case DbType.String:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.VarChar;
                    case DbType.AnsiStringFixedLength:
                    case DbType.StringFixedLength:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.VarChar;
                    case DbType.Byte:
                    case DbType.Int16:
                    case DbType.SByte:
                    case DbType.UInt16:
                    case DbType.Int32:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Int32;
                    case DbType.Int64:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Int64;
                    case DbType.Single:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Double;
                    case DbType.Decimal:
                    case DbType.Double:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Double;
                    case DbType.Date:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Date;
                    case DbType.DateTime:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Timestamp;
                    case DbType.Time:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Timestamp;
                    case DbType.Binary:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Binary;
                    case DbType.Boolean:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Bool;
                    case DbType.UInt64:
                    case DbType.VarNumeric:
                    case DbType.Currency:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Int64;
                    case DbType.Object:
                    case DbType.Guid:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.Guid;
                    case DbType.Xml:
                        return (int)MySql.Data.MySqlClient.MySqlDbType.VarChar;
                    default:
                        throw new NotSupportedException();
                }
            }
        }


        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private string _connectionString;

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }

        public IDbTransaction BeginTransction()
        {

            Transaction = CurrentConnection.BeginTransaction();

            return Transaction;

        }
        public void CommitTransaction()
        {
            Transaction.Commit();
        }
        public void RollbackTransaction()
        {
            Transaction.Rollback();
        }
        public IDbConnection CurrentConnection
        {
            get
            {
                return _connection;
            }
        }

        public IDbTransaction Transaction { get => _transaction; set => _transaction = value; }

        //constructor
        public DataLayerBase(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new MySqlConnection(connectionString);
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                // Log.Write(Log.LogMessageType.Database, "DataLayerBase", "error");
                throw ex;
            }

        }

        private IDbCommand GetCommand(string sql, params Parameters[] parameters)
        {
            MySqlConnection cmd = new MySqlConnection(_connectionString); // = new MySqlConnection(sql, (MySqlConnection)CurrentConnection);
            cmd.Open();
            // cmd.BindByName = true;
            MySqlCommand command = new MySqlCommand();
            MySqlParameter tmpparam;
            foreach (Parameters param in parameters)
            {
                tmpparam = new MySqlParameter(param.Name, (MySqlDbType)param.GetDriverType());
                tmpparam.Value = param.Value;
                command.Parameters.Add(tmpparam);
            }

            command.Connection = cmd;
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            command.ExecuteNonQuery();

            return command;

        }

        public DataTable GetDataTable(string sql, params Parameters[] parameters)
        {
            // Log.Write(Log.LogMessageType.Database, "GetDataTable", sql + GetLogParams(parameters), _connectionString);
            DataTable table = new DataTable();

            using (MySqlCommand cmd = (MySqlCommand)GetCommand(sql, parameters))
            {
                table.Load(cmd.ExecuteReader());
                return table;
            }
        }

        public object GetScalar(string sql, params Parameters[] parameters)
        {
            // Log.Write(Log.LogMessageType.Database, "GetScalar", sql + GetLogParams(parameters), _connectionString);
            using (MySqlCommand cmd = (MySqlCommand)GetCommand(sql, parameters))
            {
                return cmd.ExecuteScalar();
            }
        }


        public int Execute(string sql, params Parameters[] parameters)
        {

            try
            {
                // Log.Write(Log.LogMessageType.Database, "Execute", sql + GetLogParams(parameters), _connectionString);
                using (MySqlCommand cmd = (MySqlCommand)GetCommand(sql, parameters))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetLogParams(params Parameters[] parameters)
        {
            string tmp = "";
            foreach (Parameters parameter in parameters)
            {
                tmp += "\nParametro: Name= " + parameter.Name + " Value= " + (parameter.Value != null ? parameter.Value.ToString() : "");
            }
            return tmp;
        }
    }
}
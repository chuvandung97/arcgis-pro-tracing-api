using Npgsql;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPGEMS.Utilities;

namespace Schema.Data
{
    public class PgLogDbBase
    {
        private string _connectionstring1;

        internal IConfigService _configService;
        internal ILoggingService _loggingService;

        NpgsqlConnection _connection;

        public PgLogDbBase(IConfigService configService, ILoggingService loggingService)
        {
            _loggingService = loggingService;
            _configService = configService;
            //_connectionstring1 = configService.GetConnectionString("PGRequestLogConnectionString");

            DbInteractionSQL _dbInteractionSQL = new DbInteractionSQL(configService.GetAppSetting("PGConnectionLogPath"), configService.GetAppSetting("DBConfigPath"));
            _connectionstring1 = _dbInteractionSQL.GetConnectionStringfromXML(configService.GetAppSetting("PGRequestLogConnectionString"));
            
        }

        private NpgsqlConnection OpenConnection()
        {
            if (_connection != null && _connection.State != System.Data.ConnectionState.Closed)
                return _connection;

            _connection = new NpgsqlConnection(_connectionstring1);
            try
            {
                _connection.Open();
                return _connection;
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
            }

            throw new Exception("Unable to open connection to database.");
        }

        public async Task<HashSet<Dictionary<string, object>>> ReadDataAsync(NpgsqlCommand Command)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

            await Task.Run(() =>
            {
                bool hasError = false;
                try
                {
                    using (var connection = OpenConnection())
                    {
                        Command.Connection = connection;
                        if (!Command.IsPrepared)
                            Command.Prepare();
                        using (var reader = Command.ExecuteReader())
                        {
                            var columns = reader.GetColumnSchema();
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                foreach (var columnDef in columns)
                                {
                                    if (columnDef.ColumnOrdinal.HasValue)
                                    {
                                        object val = null;
                                        if (!reader.IsDBNull(columnDef.ColumnOrdinal.Value))
                                        {
                                            val = reader.GetValue(columnDef.ColumnOrdinal.Value);
                                            if ((columnDef.PostgresType.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Json || columnDef.PostgresType.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Jsonb) && val != null)
                                            {
                                                val = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(val.ToString());
                                            }
                                            else if (columnDef.DataType.Name == "Array" && (columnDef.DataTypeName == "_jsonb" || columnDef.DataTypeName == "_json"))
                                            {
                                                val = Array.ConvertAll(val as string[], Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>);
                                            }
                                            //columnDef.PostgresType.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.
                                        }
                                        row.Add(columnDef.ColumnName, val);
                                    }
                                }
                                result.Add(row);
                            }
                            reader.Close();
                        }
                        Command.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _loggingService.Error(ex);
                    hasError = true;
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
                //if (hasError)
                //throw new Exception("Unable to read data. Please see logs.");
            });

            return result;
        }
        public HashSet<Dictionary<string, object>> ReadData(NpgsqlCommand Command)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

            bool hasError = false;
            try
            {
                using (var connection = OpenConnection())
                {
                    Command.Connection = connection;
                    if (!Command.IsPrepared)
                        Command.Prepare();
                    using (var reader = Command.ExecuteReader())
                    {
                        var columns = reader.GetColumnSchema();
                        while (reader.Read())
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();
                            foreach (var columnDef in columns)
                            {
                                if (columnDef.ColumnOrdinal.HasValue)
                                {
                                    object val = null;
                                    if (!reader.IsDBNull(columnDef.ColumnOrdinal.Value))
                                    {
                                        val = reader.GetValue(columnDef.ColumnOrdinal.Value);
                                        if ((columnDef.PostgresType.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Json || columnDef.PostgresType.NpgsqlDbType == NpgsqlTypes.NpgsqlDbType.Jsonb) && val != null)
                                        {
                                            val = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(val.ToString());
                                        }
                                        //columnDef.PostgresType.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.
                                    }
                                    row.Add(columnDef.ColumnName, val);
                                }
                            }
                            result.Add(row);
                        }
                        reader.Close();
                    }
                    Command.Dispose();
                }
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
                hasError = true;
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
            //if (hasError)
            //    throw new Exception("Unable to read data. Please see logs.");

            return result;
        }
        public async Task<Dictionary<string, object>> UpdateDataAsync(NpgsqlCommand Command)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            await Task.Run(() =>
            {
                bool hasError = false;
                try
                {
                    using (var connection = OpenConnection())
                    {
                        Command.Connection = connection;
                        if (!Command.IsPrepared)
                            Command.Prepare();
                        Command.ExecuteNonQuery();
                        int rowsAffected = Convert.ToInt16(Command.Parameters["rowcount"].Value);
                        if (rowsAffected > 0)
                            result.Add("Message", "Success");
                        else
                            result.Add("Message", "No records");
                        Command.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _loggingService.Error(ex);
                    hasError = true;
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
                //if (hasError)
                //    throw new Exception("Unable to read data. Please see logs.");
            });

            return result;
        }

        public async Task<int> ScalarDataAsync(NpgsqlCommand Command)
        {
            int result = 0;
            await Task.Run(() =>
            {
                bool hasError = false;
                try
                {
                    using (var connection = OpenConnection())
                    {
                        Command.Connection = connection;
                        if (!Command.IsPrepared)
                            Command.Prepare();
                        result = Convert.ToInt16(Command.ExecuteScalar());
                        Command.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _loggingService.Error(ex);
                    hasError = true;
                    throw ex;
                }
                finally
                {
                    _connection.Close();
                }
                //if (hasError)
                //    throw new Exception("Unable to read data. Please see logs.");
            });

            return result;
        }
    }
}

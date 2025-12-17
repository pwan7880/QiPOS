using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Principal; 
using System.Windows.Forms;
namespace QiPOS
{
    /// <summary>
    /// TODO: makes the connection string more configurable
    /// Connect to SQL Using those connectors not sure why we need them
    /// </summary>
    public sealed class Connect
    {
        public SqlDataReader reader;
        public DataTable aTable;
        public DataSet aDataSet;
        public static SqlConnection connection;
        public SqlDataAdapter adaptor;
        private SqlCommandBuilder cmdBuilder;
        private string QueryStr;
        private string connectString;
        private static string cachedConnectionString = null;
        public string ConnectionStr
        {
            get { return connectString; }
            set { connectString = value; }
        }
        private string LoadConnectionString()
        {
            if (!string.IsNullOrEmpty(cachedConnectionString))
                return cachedConnectionString;

            // Prioritize App.config connection string
            string connStr = ConfigurationManager.ConnectionStrings["QiPOSDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new DatabaseUnavailableException("Connection string 'QiPOSDb' is missing or empty in App.config.");
            }

            cachedConnectionString = connStr;
            Console.WriteLine($"Loaded connection string from App.config: {connStr}");
            return connStr;
        }
        public void Close()
        {
            connection.Close();
        }
        public Connect()
        {
            connectString = LoadConnectionString();
        }


        public void ConnectBD()
        {
            if (Connect.connection != null)
                Connect.connection.Close();
            try
            {
                Connect.connection = new SqlConnection(ConnectionStr);
                Connect.connection.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void NoReturnQuery(string queryStr)
        {
            try
            {
                ConnectBD();
                new SqlCommand(queryStr, Connect.connection).ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + queryStr);
            }
        }

        public void QueryTable(string queryStr)
        {
            try
            {
                QueryStr = queryStr;
                ConnectBD();
                SqlCommand sqlCommand = new SqlCommand(QueryStr, Connect.connection);
                adaptor = new SqlDataAdapter
                {
                    SelectCommand = sqlCommand
                };
                cmdBuilder = new SqlCommandBuilder(adaptor);
                aDataSet = new DataSet();
                adaptor.Fill(aDataSet, "returntable1");
                aTable = aDataSet.Tables["returntable1"];
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + queryStr);
            }
        }
        public void UpdateTable(DataTable tableChanges)
        {
            try
            {
                adaptor.SelectCommand = new SqlCommand(QueryStr, Connect.connection);
                if (tableChanges == null)
                    return;
                adaptor.Update(tableChanges);
                aTable.AcceptChanges();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(adaptor.UpdateCommand + ex.Message);
            }
            catch (Exception e1)
            {
                if (e1.Data.Contains("Dynamic SQL"))
                {
                    Console.WriteLine("issues ");
                }
            }
        }

        public void QueryTable(string queryStr1, string queryStr2)
        {
            try
            {
                QueryStr = queryStr1;
                ConnectBD();
                SqlCommand sqlCommand1 = new SqlCommand(QueryStr, Connect.connection);
                adaptor = new SqlDataAdapter
                {
                    SelectCommand = sqlCommand1
                };
                cmdBuilder = new SqlCommandBuilder(adaptor);
                aDataSet = new DataSet();
                adaptor.Fill(aDataSet, "returntable1");
                aTable = aDataSet.Tables["returntable1"];
                ConnectBD();
                SqlCommand sqlCommand2 = new SqlCommand(queryStr2, Connect.connection);
                adaptor = new SqlDataAdapter
                {
                    SelectCommand = sqlCommand2
                };
                cmdBuilder = new SqlCommandBuilder(adaptor);
                adaptor.Fill(aDataSet, "returntable2");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + queryStr1 + queryStr2);
            }
        }


        public int GetInt32(string queryStr)
        {
            int num1 = 0;
            try
            {
                ConnectBD();
                reader = new SqlCommand(queryStr, Connect.connection).ExecuteReader();
                while (reader.Read())
                    num1 = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message + " " + queryStr);
            }
            return num1;
        }

        public string AddBackslash(string inStr)
        {
            inStr = inStr.Trim();
            inStr = inStr.Replace("\\", "\\\\");
            inStr = inStr.Replace("'", "''");
            return inStr;
        }

        /// <summary>
        /// This backs up the database to a specified folder.
        /// </summary>
        public void BackupDatabase()
        {
            // TODO: the literals need to  be changed to match your environment
            string connectionString = connectString;
            string backupFolder = @"C:\data\backups"; // Change this path if needed
            string dbName = "cornucopia"; // Logical DB name (used in BACKUP DATABASE)

            string timestamp = DateTime.Now.ToString("dd-MM-yyyy");
            string backupFile = Path.Combine(backupFolder, $"QiPos_backup[{timestamp}].bak");

            if (!Directory.Exists(backupFolder))
                Directory.CreateDirectory(backupFolder);

            string backupSql = $@"
            BACKUP DATABASE [{dbName}]
            TO DISK = N'{backupFile}'
            WITH FORMAT, INIT, NAME = 'QiPOS Full Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;
        ";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(backupSql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"Backup completed: {backupFile}");
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogWriter.Instance.Log($"Backup failed: {ex.Message}");                    
                }
            }
        }

        public DataTable QueryTableSP(string spName, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(LoadConnectionString()))
            {
                using (var cmd = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure })
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        Console.WriteLine($"Parameters for {spName}:");
                        foreach (var param in parameters)
                        {
                            Console.WriteLine($"  {param.ParameterName} = {param.Value ?? "NULL"}");
                            cmd.Parameters.AddWithValue(param.ParameterName, param.Value ?? DBNull.Value);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No parameters for {spName}");
                    }
                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    try
                    {
                        string currentUser = WindowsIdentity.GetCurrent()?.Name ?? "Unknown";
                        Console.WriteLine($"Attempting connection as: {currentUser}, Initial Database: {conn.Database}");
                        conn.Open();
                        Console.WriteLine($"Connected to: {conn.DataSource}, Database: {conn.Database}");
                        adapter.Fill(table);
                        Console.WriteLine($"Rows returned from {spName}: {table.Rows.Count}");
                        foreach (DataRow row in table.Rows)
                        {
                            Console.WriteLine("Row data:");
                            foreach (DataColumn col in table.Columns)
                            {
                                var value = row[col];
                                // Handle potential format issues in logging
                                string displayValue = value?.ToString() ?? "NULL";
                                Console.WriteLine($"  {col.ColumnName} = {displayValue} (Type: {value?.GetType().Name ?? "null"})");
                            }
                        }
                        //Console.WriteLine("QueryTableSP executed {SPName}, rows returned: {RowCount}", spName, table.Rows.Count);
                        return table; // Ensure table is returned even if logging fails
                    }
                    catch (SqlException ex)
                    {
                        string errorDetails = $"SQL Error Number: {ex.Number}, Message: {ex.Message}, Procedure: {ex.Procedure}, Line: {ex.LineNumber}, State: {ex.State}";
                        LogError(ex, $"SQL error executing SP: {spName}. {errorDetails}");
                        throw new DatabaseUnavailableException($"1 Failed to execute {spName}: {errorDetails}", ex);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"FormatException in QueryTableSP: {ex.Message}, StackTrace: {ex.StackTrace}");
                        LogError(ex, $"Format error executing SP: {spName}");
                        return new DataTable(); // Return empty table on format error to prevent null
                    }
                    catch (Exception ex)
                    {
                        LogError(ex, $"Unexpected error executing SP: {spName}");
                        throw new DatabaseUnavailableException($"2 Failed to execute {spName}: {ex.Message}", ex);
                    }
                }
            }
        }
        private void LogError(Exception ex, string context)
        {
            ErrorLogWriter.Instance.Log(ex.Message +" Database error in " + context);
            Console.WriteLine($"Error in {context}: {ex.Message}");
        }

        public void ExecuteNonQuerySP(string spName, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(LoadConnectionString()))
            {
                using (var cmd = new SqlCommand(spName, conn) { CommandType = CommandType.StoredProcedure })
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ErrorLogWriter.Instance.Log($"Failed to execute: {spName}");
                        throw new DatabaseUnavailableException($"Failed to execute {spName}", ex);
                    }
                }
            }
        }

         
        

        /// <summary>
        /// Check all required tables exist in the local database.
        /// </summary>
        /// <exception cref="DatabaseUnavailableException"></exception>
        public void ValidateLocalDb()
        {
            try
            {
                using (var conn = new SqlConnection(this.connectString))
                {
                    conn.Open();

                    var requiredTables = new[]
                    { 
                "account_cards",
                "account_list",
                "pos_look_up",
                "pos_sale",
                "pos_sale_cache",
                "pos_sale_detail", 
                "pos_stock",
                "sub_agent",
                "sub_inv_mag",
                "sub_inv_paper_details",
                "sub_inv_sum",
                "sub_template",
                "sub_template_details",
                "user_"
            };

                    foreach (var table in requiredTables)
                    {
                        string checkSql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @name";
                        using (var cmd = new SqlCommand(checkSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", table);
                            int exists = (int)cmd.ExecuteScalar();

                            if (exists == 0)
                                throw new TableMissingException($"Missing required table: {table}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DatabaseUnavailableException("Could not connect to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new DatabaseUnavailableException("Unexpected error during database verification.", ex);
            }
        }

    }
    public class DatabaseUnavailableException : Exception
    {
        public DatabaseUnavailableException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }

    public class TableMissingException : Exception
    {
        public string TableName { get; }

        public TableMissingException(string tableName)
            : base($"Required table is missing: {tableName}")
        {
            TableName = tableName;
        }

        public TableMissingException(string tableName, Exception innerException)
            : base($"Required table is missing: {tableName}", innerException)
        {
            TableName = tableName;
        }
    }
}


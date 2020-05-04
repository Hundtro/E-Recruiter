using Microsoft.Data.Sqlite;

namespace erecruiter
{
    class DatabaseAdapter
    {
        private SqliteConnectionStringBuilder connectionString;
        private SqliteConnection connection;
        private SqliteCommand command;
        private SqliteDataReader dataReader;

        public DatabaseAdapter(string dbsource)
        {
            connectionString = new SqliteConnectionStringBuilder()
            {
                DataSource = dbsource
            };
                
            connection = new SqliteConnection(connectionString.ConnectionString);
        }

        public int ExecuteCommand(string query)
        {
            int rows = 0;
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            rows = command.ExecuteNonQuery();
            connection.Close();

            return rows;
        }

        public void ExecuteSelectCommand(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            dataReader = command.ExecuteReader();
        }

        public void ClearData()
        {
            if(dataReader != null)
                dataReader.Close();
            if(connection.State.Equals("Open"))
                connection.Close();
        }

        public bool MoveToNextRow()
        {
            if(dataReader != null)
                return dataReader.Read();
            else
                return false;
        }

        public string GetColumnValue(int number)
        {
            return dataReader[number].ToString();
        }

        public string GetColumnValue(string name)
        {
            return dataReader[name].ToString();
        }
    }
}

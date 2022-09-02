// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using MySql.Data.MySqlClient;

namespace Icarus.Database
{
    public static class Connection
    {
        private static string connectionString = string.Empty;
        public static void SetConnectionString(string host, int port, string database, string userid, string password)
        {
            connectionString = "server=" + host +
                               ";database=" + database +
                               ";port=" + port +
                               ";userid=" + userid +
                               ";password=" + password + ";";
        }
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
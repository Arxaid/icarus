// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using MySql.Data.MySqlClient;

namespace Icarus.Database
{
    public static class Tables
    {
        public static void SetupTables()
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                MySqlCommand createFireteam = new MySqlCommand(
                    "CREATE TABLE IF NOT EXISTS fireteam(" +
                    "id INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT," +
                    "fireteam_id BIGINT UNSIGNED NOT NULL," +
                    "fireteam_guild BIGINT UNSIGNED NOT NULL," +
                    "fireteam_leader BIGINT UNSIGNED," +
                    "fireteam_active_member BIGINT UNSIGNED," +
                    "fireteam_maybe_member BIGINT UNSIGNED)",
                    connection);

                connection.Open();
                createFireteam.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
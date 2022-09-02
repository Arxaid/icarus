// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using MySql.Data.MySqlClient;

namespace Icarus.Database
{
    public static class Fireteam
    {
        public static bool FireteamCreate(ulong fireteamID, ulong guildID, ulong leaderID)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO fireteam(fireteam_id, fireteam_guild, fireteam_time, fireteam_leader, fireteam_member)" +
                                                         "VALUES (@FireteamID, @FireteamGuild, UTC_TIMESTAMP(), @FireteamLeader, @FireteamMember);", connection);

                    cmd.Parameters.AddWithValue("@FireteamID", fireteamID);
                    cmd.Parameters.AddWithValue("@FireteamGuild", guildID);
                    cmd.Parameters.AddWithValue("@FireteamLeader", leaderID);
                    cmd.Parameters.AddWithValue("@FireteamMember", leaderID);
                    cmd.Prepare();

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
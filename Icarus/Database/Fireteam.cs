// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

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

                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO fireteam(fireteam_id, fireteam_guild, fireteam_leader, fireteam_active_member)" +
                                                         "VALUES (@FireteamID, @FireteamGuild, @FireteamLeader, @FireteamMember);", connection);

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

        public static bool FireteamAddActiveMember(ulong fireteamID, ulong guildID, ulong memberID)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(@"INSERT INTO fireteam(fireteam_id, fireteam_guild, fireteam_active_member)" +
                                                         "VALUES (@FireteamID, @FireteamGuild, @FireteamActiveMember);", connection);

                    cmd.Parameters.AddWithValue("@FireteamID", fireteamID);
                    cmd.Parameters.AddWithValue("@FireteamGuild", guildID);
                    cmd.Parameters.AddWithValue("@FireteamActiveMember", memberID);
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

        public static bool FireteamDeleteActiveMember(ulong fireteamID, ulong memberID)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(@"DELETE FROM fireteam WHERE fireteam_id=@FireteamID " + 
                                                         "AND fireteam_active_member=@FireteamMember", connection);

                    cmd.Parameters.AddWithValue("@FireteamID", fireteamID);
                    cmd.Parameters.AddWithValue("@FireteamMember", memberID);
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

        public static List<ulong> FireteamGetActiveMembers(ulong fireteamID)
        {
            List<ulong> ActiveMembersList = new List<ulong>();

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand(@"SELECT fireteam_active_member FROM fireteam " +
                                                         "WHERE fireteam_id=@FireteamID", connection);

                    cmd.Parameters.AddWithValue("@FireteamID", fireteamID);
                    cmd.Prepare();

                    using (MySqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var member = (ulong)dataReader["fireteam_active_member"];
                            ActiveMembersList.Add(member);
                        }

                        return ActiveMembersList;
                    }
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
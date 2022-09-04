// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using DSharpPlus.Entities;

namespace Icarus.Commands
{
    internal static class ErrorEmbeds
    {
        public static DiscordEmbedBuilder databaseError_FireteamCreation = new DiscordEmbedBuilder
        {
            Title = "Database error.",
            Description = "Fireteam creation.",
            Color = DiscordColor.Red
        };

        public static DiscordEmbedBuilder databaseError_FireteamAddMember = new DiscordEmbedBuilder
        {
            Title = "Database error.",
            Description = "Adding a member to existing fireteam.",
            Color = DiscordColor.Red
        };

        public static DiscordEmbedBuilder databaseError_FireteamDeleteMember = new DiscordEmbedBuilder
        {
            Title = "Database error.",
            Description = "Removal a member from existing fireteam.",
            Color = DiscordColor.Red
        };
    }
}
// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using System.Collections.Generic;
using System.Threading.Tasks;

using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Icarus.Content
{
    public class PendingActivityChoiceProvider : IChoiceProvider
    {
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            return new DiscordApplicationCommandOptionChoice[]
            {
                new DiscordApplicationCommandOptionChoice("King's Fall",            "kf"),
                new DiscordApplicationCommandOptionChoice("Vow of the Disciple",    "vod"),
                new DiscordApplicationCommandOptionChoice("Vault of Glass",         "vog"),
                new DiscordApplicationCommandOptionChoice("Deep Stone Crypt",       "dsc"),
                new DiscordApplicationCommandOptionChoice("Garden of Salvation",    "gos"),
                new DiscordApplicationCommandOptionChoice("The Last Wish",          "lw"),

                new DiscordApplicationCommandOptionChoice("Ordeal Grandmaster",     "gm"),

                new DiscordApplicationCommandOptionChoice("Duality",                "duality"),
                new DiscordApplicationCommandOptionChoice("Grasp of Avarice",       "grasp"),
                new DiscordApplicationCommandOptionChoice("The Prophecy",           "prophecy"),
                new DiscordApplicationCommandOptionChoice("Pit of Heresy",          "pit"),
                new DiscordApplicationCommandOptionChoice("Shattered Throne",       "throne"),

                new DiscordApplicationCommandOptionChoice("Custom",                 "custom")
            };
        }
    }
}
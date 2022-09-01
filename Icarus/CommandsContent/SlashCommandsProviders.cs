// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using System.Collections.Generic;
using System.Threading.Tasks;

using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Icarus.Commands
{
    public class PendingActivityChoiceProvider : IChoiceProvider
    {
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            return new DiscordApplicationCommandOptionChoice[]
            {
                new DiscordApplicationCommandOptionChoice("King's Fall",            "King's Fall"),
                new DiscordApplicationCommandOptionChoice("Vow of the Disciple",    "Vow of the Disciple"),
                new DiscordApplicationCommandOptionChoice("Vault of Glass",         "Vault of Glass"),
                new DiscordApplicationCommandOptionChoice("Deep Stone Crypt",       "Deep Stone Crypt"),
                new DiscordApplicationCommandOptionChoice("Garden of Salvation",    "Garden of Salvation"),
                new DiscordApplicationCommandOptionChoice("The Last Wish",          "The Last Wish"),

                new DiscordApplicationCommandOptionChoice("Ordeal Grandmaster",     "Ordeal Grandmaster"),

                new DiscordApplicationCommandOptionChoice("Duality",                "Duality"),
                new DiscordApplicationCommandOptionChoice("Grasp of Avarice",       "Grasp of Avarice"),
                new DiscordApplicationCommandOptionChoice("The Prophecy",           "The Prophecy"),
                new DiscordApplicationCommandOptionChoice("Pit of Heresy",          "Pit of Heresy"),
                new DiscordApplicationCommandOptionChoice("Shattered Throne",       "Shattered Throne"),

                new DiscordApplicationCommandOptionChoice("Custom",                 "Custom activity")
            };
        }
    }
}
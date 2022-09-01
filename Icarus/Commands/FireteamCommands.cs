// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

using Icarus.Content;

namespace Icarus.Commands
{
    [SlashCommandGroup("fireteam", "Contains main slash commands for interaction with LFG services")]
    public class SlashFireteamCommands : ApplicationCommandModule
    {
        [SlashCommand("create", "Creates a new fireteam.")]
        public async Task Create(InteractionContext ctx,
                                 [ChoiceProvider(typeof(PendingActivityChoiceProvider))]
                                 [Option("activity", "Select an activity from the drop-down list")] string activity,
                                 [Option("comment", "Write here everything you want to clarify")] string comment) 
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Your chosen activity is " + activity + " and you have commented " + comment)).ConfigureAwait(false);
        }
    }
}
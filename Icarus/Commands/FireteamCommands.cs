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

namespace Icarus.Commands
{
    [SlashCommandGroup("fireteam", "Contains main slash commands for interaction with LFG services")]
    public class SlashFireteamCommands : ApplicationCommandModule
    {
        public static DiscordEmbedBuilder fireteamEmbed;

        [SlashCommand("create", "Creates a new fireteam.")]
        public async Task Create(InteractionContext ctx,
                                 [ChoiceProvider(typeof(PendingActivityChoiceProvider))]
                                 [Option("activity", "Select an activity from the drop-down list")] string searchActivity,
                                 [Option("time", "Write here time when the activity starts DD.MM-HH:MM")] string searchTime,
                                 [Option("comment", "Write here everything you want to clarify")] string searchComment) 
        {
            fireteamEmbed = new DiscordEmbedBuilder
            {
                Title = searchActivity + "\n" + ActivityContent.PendingActivityTime(searchTime),
                Description = searchComment,
                Color = DiscordColor.Black
            }.WithThumbnail(ActivityContent.PendingActivityImage(searchActivity));

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(fireteamEmbed)).ConfigureAwait(false);
        }
    }
}
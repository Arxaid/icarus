// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using System.Collections.Generic;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

using Icarus.Database;

namespace Icarus.Commands
{
    [SlashCommandGroup("fireteam", "Contains main slash commands for interaction with LFG services")]
    public class SlashFireteamCommands : ApplicationCommandModule
    {
        [SlashCommand("create", "Creates a new fireteam.")]
        [SlashRequireGuild]
        [SlashRequirePermissions(Permissions.SendMessages)]
        public async Task Create(InteractionContext ctx,
                                 [ChoiceProvider(typeof(PendingActivityChoiceProvider))]
                                 [Option("activity", "Select an activity from the drop-down list")] string searchActivity,
                                 [Option("time", "Write here time when the activity starts DD.MM-HH:MM")] string searchTime,
                                 [Option("comment", "Write here everything you want to clarify")] string searchComment)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                              .WithContent(ctx.User.Username + "'s new fireteam.")).ConfigureAwait(false);

            await ctx.DeleteResponseAsync().ConfigureAwait(false);

            DiscordEmbedBuilder fireteamEmbed = new DiscordEmbedBuilder
            {
                Title = searchActivity + "\n" + ActivityContent.PendingActivityTime(searchTime),
                Description = searchComment + "\n\n**Fireteam leader: **\n" + ctx.User.Mention + " - " + ctx.User.Username,
                Color = DiscordColor.Black
            }
            .WithThumbnail(ActivityContent.PendingActivityImage(searchActivity));

            DiscordMessage fireteamMessage = await ctx.Channel.SendMessageAsync(embed: fireteamEmbed).ConfigureAwait(false);

            await fireteamMessage.ModifyAsync(msg =>
            {
                msg.AddComponents(new DiscordComponent[]
                {
                    new DiscordButtonComponent(ButtonStyle.Primary, "join", "Join", false),
                    new DiscordButtonComponent(ButtonStyle.Primary, "maybe", "Maybe Join", false),
                    new DiscordButtonComponent(ButtonStyle.Danger, "delete", "Delete", false)
                });
                msg.Embed = fireteamEmbed.Build();
            }).ConfigureAwait(false);

            if (!Fireteam.FireteamCreate(fireteamMessage.Id, ctx.Guild.Id, ctx.User.Id))
            {
                await ctx.Channel.SendMessageAsync(embed: ErrorEmbeds.databaseError_FireteamCreation).ConfigureAwait(false);
            }
        }
    }
}
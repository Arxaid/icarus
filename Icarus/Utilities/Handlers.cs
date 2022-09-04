// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

using Icarus.Commands;
using Icarus.Database;

namespace Icarus.Utilities
{
    internal class Handlers
    {
        private DiscordClient Client;
        public Handlers(DiscordClient client) { this.Client = client; }

        #region StatusHandler

        internal Task OnClientReady(DiscordClient discordClient, ReadyEventArgs eventArgs)
        {
            Client.UpdateStatusAsync(new DiscordActivity(Version.CurrentVersion, ActivityType.Watching), UserStatus.DoNotDisturb);
            return Task.CompletedTask;
        }

        #endregion
        #region GuildHandler

        private DiscordChannel LogChannel;

        internal async Task OnFindingTheGuild(DiscordClient discordClient, GuildCreateEventArgs eventArgs)
        {
            LogChannel = eventArgs.Guild.GetChannel(1014838285807394836);

            var messages = LogChannel.GetMessagesAsync();
            if (messages.Result.Any())
            {
                await LogChannel.DeleteMessagesAsync(messages.Result).ConfigureAwait(false);
            }

            DiscordEmbedBuilder startupEmbed = new DiscordEmbedBuilder
            {
                Title = $"Icarus service has been successfully launched",
                Description = $"Launch time: { DateTime.Now }\n" + $"Current version: {Version.CurrentVersion}",
                Color = DiscordColor.Black
            };

            await LogChannel.SendMessageAsync(embed: startupEmbed).ConfigureAwait(false);
        }

        #endregion
        #region ButtonPressHandler

        public async Task OnButtonPressed(DiscordClient discordClient, ComponentInteractionCreateEventArgs eventArgs)
        {
            await eventArgs.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

            DiscordEmbed fireteamEmbed = eventArgs.Message.Embeds.FirstOrDefault();

            DiscordEmbedBuilder fireteamModifiedEmbed = new DiscordEmbedBuilder()
            {
                Title = fireteamEmbed.Title,
                Description = fireteamEmbed.Description,
                Color = DiscordColor.Black
            }
            .WithThumbnail(fireteamEmbed.Thumbnail.Url.ToString());

            if (eventArgs.Id == "join")
            {
                List<ulong> ActiveMembersList = Fireteam.FireteamGetActiveMembers(eventArgs.Message.Id);

                if (ActiveMembersList.Contains(eventArgs.User.Id))
                {
                    if(!Fireteam.FireteamDeleteActiveMember(eventArgs.Message.Id, eventArgs.User.Id))
                    {
                        await eventArgs.Channel.SendMessageAsync(embed: ErrorEmbeds.databaseError_FireteamDeleteMember).ConfigureAwait(false);
                    }
                }
                else
                {
                    if(!Fireteam.FireteamAddActiveMember(eventArgs.Message.Id, eventArgs.Guild.Id, eventArgs.User.Id))
                    {
                        await eventArgs.Channel.SendMessageAsync(embed: ErrorEmbeds.databaseError_FireteamAddMember).ConfigureAwait(false);
                    }
                }

                string activeMembersString = string.Empty;
                foreach (ulong memberID in ActiveMembersList)
                {
                    DiscordMember discordMember = await eventArgs.Guild.GetMemberAsync(memberID).ConfigureAwait(false);
                    activeMembersString += discordMember.Mention + " - " + discordMember.Nickname + "\n";
                }
                if (!activeMembersString.Any())
                {
                    activeMembersString += " ";
                }

                fireteamModifiedEmbed.ClearFields();
                fireteamModifiedEmbed.AddField("Fireteam: ", activeMembersString);

                if (ActiveMembersList.Count >= 6)
                {
                    await eventArgs.Message.ModifyAsync(msg =>
                    {
                        msg.WithEmbed(embed: fireteamModifiedEmbed);

                        msg.AddComponents(new DiscordComponent[]
                        {
                            new DiscordButtonComponent(ButtonStyle.Primary, "join", "Join", true),
                            new DiscordButtonComponent(ButtonStyle.Primary, "maybe", "Maybe Join", false),
                            new DiscordButtonComponent(ButtonStyle.Danger, "delete", "Delete", false)
                        });
                        msg.Embed = fireteamModifiedEmbed.Build();
                    }).ConfigureAwait(false);
                }
                else
                {
                    await eventArgs.Message.ModifyAsync(msg =>
                    {
                        msg.WithEmbed(embed: fireteamModifiedEmbed);

                        msg.AddComponents(new DiscordComponent[]
                        {
                            new DiscordButtonComponent(ButtonStyle.Primary, "join", "Join", false),
                            new DiscordButtonComponent(ButtonStyle.Primary, "maybe", "Maybe Join", false),
                            new DiscordButtonComponent(ButtonStyle.Danger, "delete", "Delete", false)
                        });
                        msg.Embed = fireteamModifiedEmbed.Build();
                    }).ConfigureAwait(false);
                }
            }

            if (eventArgs.Id == "maybe")
            {
                //fireteamModifiedEmbed.AddField("test", "this is maybe join button");
            }
        }

        #endregion
    }
}
// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using System;
using System.Linq;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Icarus.Utilities
{
    internal class Handlers
    {
        private DiscordClient Client;
        private DiscordChannel LogChannel;

        public Handlers(DiscordClient client) { this.Client = client; }

        #region StatusHandler

        internal Task OnClientReady(DiscordClient discordClient, ReadyEventArgs eventArgs)
        {
            Client.UpdateStatusAsync(new DiscordActivity("v" + Version.CurrentVersion, ActivityType.Watching), UserStatus.DoNotDisturb);
            return Task.CompletedTask;
        }

        #endregion
        #region LogHandler

        internal async Task OnFindingTheGuild(DiscordClient discordClient, GuildCreateEventArgs eventArgs)
        {
            LogChannel = eventArgs.Guild.GetChannel(1014543689764315197);

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
    }
}
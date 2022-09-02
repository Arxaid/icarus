// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

using Icarus.Commands;
using Icarus.Utilities;
using Icarus.Database;

namespace Icarus.Core
{
    internal class IcarusCore
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension PrefixCommands { get; private set; }
        public SlashCommandsExtension SlashCommands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public Handlers Handlers { get; private set; }

        public async Task RunAsync()
        {
            #region Config

            var json = string.Empty;
            using (var fileStream = File.OpenRead("config.json"))
            using (var streamReader = new StreamReader(fileStream, new UTF8Encoding(false)))
                json = await streamReader.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var clientConfig = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.All
            };

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = true,
                DmHelp = true,
                EnableMentionPrefix = true
            };

            var interactivityConfig = new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromDays(10)
            };

            Connection.SetConnectionString(configJson.Host, 3306, configJson.Name, configJson.User, configJson.Password);
            Tables.SetupTables();

            #endregion

            this.Client = new DiscordClient(clientConfig);

            PrefixCommands = this.Client.UseCommandsNext(commandsConfig);
            PrefixCommands.SetHelpFormatter<CustomHelpFormatter>();
            PrefixCommands.RegisterCommands<PrefixBaseCommands>();

            SlashCommands = this.Client.UseSlashCommands();
            SlashCommands.RegisterCommands<SlashBaseCommands>();
            SlashCommands.RegisterCommands<SlashFireteamCommands>();

            this.Client.UseInteractivity(interactivityConfig);
            this.Handlers = new Handlers(this.Client);
            this.Client.Ready += this.Handlers.OnClientReady;
            this.Client.GuildAvailable += this.Handlers.OnFindingTheGuild;
            this.Client.ComponentInteractionCreated += this.Handlers.OnButtonPressed;

            await this.Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
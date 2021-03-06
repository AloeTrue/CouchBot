﻿using Discord;
using Discord.Commands;
using MTD.DiscordBot.Domain;
using MTD.DiscordBot.Json;
using MTD.DiscordBot.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MTD.DiscordBot.Modules
{
    // Create a module with the 'sample' prefix
    [Group("goodbyes")]
    public class Goodbyes : ModuleBase
    {
        [Command("on"), Summary("Turns the goodbyes on")]
        public async Task On()
        {
            var guild = ((IGuildUser)Context.Message.Author).Guild;

            var user = ((IGuildUser)Context.Message.Author);

            if (!user.GuildPermissions.ManageGuild)
            {
                return;
            }

            var file = Constants.ConfigRootDirectory + Constants.GuildDirectory + guild.Id + ".json";
            var server = new DiscordServer();

            if (File.Exists(file))
                server = JsonConvert.DeserializeObject<DiscordServer>(File.ReadAllText(file));

            server.Goodbyes = true;
            if (string.IsNullOrEmpty(server.GoodbyeMessage))
            {
                server.GoodbyeMessage = "Good bye, %USER%, thanks for hanging out!";
            }

            File.WriteAllText(file, JsonConvert.SerializeObject(server));
            await Context.Channel.SendMessageAsync("Goodbyes have been turned on.");
        }

        [Command("off"), Summary("Turns the goodbyes off")]
        public async Task Off()
        {
            var guild = ((IGuildUser)Context.Message.Author).Guild;

            var user = ((IGuildUser)Context.Message.Author);

            if (!user.GuildPermissions.ManageGuild)
            {
                return;
            }

            var file = Constants.ConfigRootDirectory + Constants.GuildDirectory + guild.Id + ".json";
            var server = new DiscordServer();

            if (File.Exists(file))
                server = JsonConvert.DeserializeObject<DiscordServer>(File.ReadAllText(file));

            server.Goodbyes = false;
            File.WriteAllText(file, JsonConvert.SerializeObject(server));
            await Context.Channel.SendMessageAsync("Goodbyes have been turned off.");
        }

        [Command("set"), Summary("Sets the goodbye message")]
        public async Task Set(string message)
        {
            var guild = ((IGuildUser)Context.Message.Author).Guild;

            var user = ((IGuildUser)Context.Message.Author);

            if (!user.GuildPermissions.ManageGuild)
            {
                return;
            }

            var file = Constants.ConfigRootDirectory + Constants.GuildDirectory + guild.Id + ".json";
            var server = new DiscordServer();

            if (File.Exists(file))
                server = JsonConvert.DeserializeObject<DiscordServer>(File.ReadAllText(file));

            server.GoodbyeMessage = message;
            File.WriteAllText(file, JsonConvert.SerializeObject(server));
            await Context.Channel.SendMessageAsync("Goodbye Message has been set.");
        }
    }
}

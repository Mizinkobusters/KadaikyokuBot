using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace KadaikyokuBot
{
    public class HelpCmd : ModuleBase
    {
        [Command("help")]
        public async Task Reply()
        {
            await ReplyAsync("help test.");
        }
    }
}

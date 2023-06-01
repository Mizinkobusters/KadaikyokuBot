using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace KadaikyokuBot
{
    class Program
    {
        // Discord Bot TOKEN
        private static string token;
        // Discord Bot起動に必要な変数
        private readonly DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider serviceProvider;

        // コマンド実行時に使うPrefix
        private const char PREFIX = '!';

        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo diParent = di.Parent.Parent;
            Console.WriteLine($"{diParent.FullName}");
            var builder = new ConfigurationBuilder()
                .AddJsonFile(path: $"{diParent.FullName}\\appsettings.json");
            var configuration = builder.Build();
            token = (string)configuration["DiscordToken"];

            GakkyokuLoader loader = new GakkyokuLoader();
            loader.loadKadaikyoku();
            GakkyokuFormatter formatter = new GakkyokuFormatter();
            if (Gakkyoku.gakkyokuList.Count != 0)
            {
                formatter.gakkyokuListToFumenList(Gakkyoku.gakkyokuList);
            }
            
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
                //LogLevel = LogSeverity.Info
            };

            client = new DiscordSocketClient(config);
            client.Log += LogAsync;
            client.Ready += onReady;
        }

        public async Task MainAsync()
        {
            commands = new CommandService();
            serviceProvider = new ServiceCollection().BuildServiceProvider();
            client.MessageReceived += MessageRecieved;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task onReady()
        {
            Console.WriteLine($"{client.CurrentUser} is Running.");
            return Task.CompletedTask;
        }

        private async Task MessageRecieved(SocketMessage message)
        {
            if (message == null)
            {
                return;
            }
            if (message.Author.IsBot)
            {
                return;
            }

            ComponentBuilder button = new ComponentBuilder()
                .WithButton("課題曲を生成！", "kadaikyoku", ButtonStyle.Primary);

            // 課題曲という単語に反応するようにする
            if (message.Content.Contains("課題曲"))
            {
                await message.Channel.SendMessageAsync("課題曲の募集ですか？");
                await message.Channel.SendMessageAsync("↓のボタンを押すか、 `!kadaikyoku` を実行すると課題曲を生成できますよ！", 
                    components: button.Build());
            }

            //コマンド実行
            var msg = message as SocketUserMessage;
            int argPos = 0;
            // コマンド実行時のPrefixを設定
            if (!msg.HasCharPrefix(PREFIX, ref argPos))
            {
                return;
            }
            var context = new CommandContext(client, msg);
            var result = await commands.ExecuteAsync(context, argPos, serviceProvider);
            // 失敗したらエラーを送信
            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
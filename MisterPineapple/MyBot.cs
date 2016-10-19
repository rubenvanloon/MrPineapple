using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace MisterPineapple
{
    internal class MyBot
    {
        string[] MemesFolder;
        Random rand;

        private readonly DiscordClient _bot;
        private readonly CommandService _commands;

        private enum Command
        {
            Uid = 1,
            Points = 2
        }

        public MyBot()
        {

            rand = new Random();
            MemesFolder = new string[]
                {
                    "mem/meme1.jpg",
                    "mem/meme2.jpg",
                    "mem/meme3.jpg",
                    "mem/meme4.jpg",
                    "mem/meme5.jpg",
                    "mem/meme6.jpg",
                    "mem/meme7.jpg",
                    "mem/meme8.jpg",
                    "mem/meme9.jpg",
                    "mem/meme10.jpg",
                    "mem/meme11.jpg",
                    "mem/meme12.jpg",
                };

            _bot = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            _bot.UsingCommands(x =>
            {
               x.PrefixChar = '!';
               x.AllowMentionPrefix = true;
            });

            _commands = _bot.GetService<CommandService>();

            _bot.UserJoined += Bot_UserJoined;
            _bot.UserLeft += Bot_UserLeft;
            var serverList = _bot.Servers.ToList();

            foreach (var server in serverList)
            {

            }

            RegisterMemeCommand();
            RegisterDeleteCommand();

            _bot.ExecuteAndWait(async () =>
            {
                await _bot.Connect("MjM3NjEwOTEwNjA3MjEyNTQ2.CualvQ.WvPmFzBXDy1TP8WeiRJpxNzSejM", TokenType.Bot);
            });
        }

        public void Bot_UserJoined(object sender, UserEventArgs e)
        {

        }

        public void Bot_UserLeft(object sender, UserEventArgs e)
        {

        }


        private void RegisterMemeCommand()
        {
            _commands.CreateCommand("memes")
                .Do(async (e) =>
                {
                    int randomMeme = rand.Next(MemesFolder.Length);
                    string MemeToPost = MemesFolder[randomMeme];
                    await e.Channel.SendFile(MemeToPost);
                });
        }

        private void RegisterDeleteCommand()
        {
            _commands.CreateCommand("delete")
                .Do(async (e) =>
                {
                    Message[] MessagesToDelete;
                    MessagesToDelete = await e.Channel.DownloadMessages(10);

                    await e.Channel.DeleteMessages(MessagesToDelete);
                });
        }


        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
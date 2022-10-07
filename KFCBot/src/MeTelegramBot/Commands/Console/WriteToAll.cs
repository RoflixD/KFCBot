using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Telegram.Bot;

namespace KFCBot.src.MeTelegramBot
{
    public class WriteToAll : BaseConsoleCommand
    {
        private static string FilePath = "Users.txt";

        public override async void Execute()
        {
            List<string> usersId = FileWorker.ReadFrom(FilePath);
            Console.WriteLine("Ready to write! Just type your message!");
            var msg = Console.ReadLine();
            foreach(var id in usersId)
            {
                long sendToId;
                if (long.TryParse(id, out sendToId))
                {
                    await MeTelegramBot.BotClient.SendTextMessageAsync(sendToId, msg);
                }
                else
                {
                    Console.WriteLine("Error!");
                    continue;
                }
            }
        }

        public override bool IsCommand(string str)
        {
            return str.ToLower() == "writetoall";
        }
    }
}

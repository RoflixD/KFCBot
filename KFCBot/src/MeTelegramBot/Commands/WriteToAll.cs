using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;

namespace KFCBot.src.MeTelegramBot
{
    public class WriteToAll : BaseCommand
    {
        public override async void Execute()
        {            
            if(MeTelegramBot.AllChats.Count <= 0)
            {
                Console.WriteLine("Have no active members!");
                return;
            }
            Console.WriteLine("Ready to write!");
            var msg = Console.ReadLine();
            foreach(var chat in MeTelegramBot.AllChats)
            {
                if(MeTelegramBot.BotClient == null)
                {
                    Console.WriteLine("Have no client, fucker!");
                    return;
                }
                await MeTelegramBot.BotClient.SendTextMessageAsync(chat, msg);
            }
        }

        public override bool IsCommand(string str)
        {
            return str.ToLower() == "writetoall";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Telegram.Bot;

namespace KFCBot.src.MeTelegramBot
{
    public class WriteToAll : BaseConsoleCommand
    {
        public override async void Execute()
        {
            Console.WriteLine("Ready to write! Just type your message!");
            var msg = Console.ReadLine();
            if (!File.Exists("Users.txt"))
            {
                Console.WriteLine("Have no users file. There's no users it the chat or something wrong with writing func!");
                return;
            }
            using (StreamReader sr = new StreamReader("Users.txt"))
            {
                string s = "";
                while((s = sr.ReadLine()) != null)
                {
                    try
                    {
                        long chatId = long.Parse(s);
                        await MeTelegramBot.BotClient.SendTextMessageAsync(chatId, msg);
                        Console.WriteLine("Done!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Somthing went wrong while I was trying send message to all memebers in the chat!");
                        Console.WriteLine(ex.ToString());
                        return;
                    }
                }
            }            
        }

        public override bool IsCommand(string str)
        {
            return str.ToLower() == "writetoall";
        }
    }
}

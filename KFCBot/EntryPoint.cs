using KFCBot.src.MeTelegramBot;
using System;

namespace KFCBot
{
    class EntryPoint
    {
        public static void Main(string[] args) 
        {
            MeTelegramBot bot = new MeTelegramBot();
            bot.Start();            
        }
    }
}

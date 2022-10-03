using KFCBot.src.MeTelegramBot;
/*
 * TODO:
 * #1 Abstract ReadWrite operations
 * #1 Add admints flag to users list file
 * #1 mb add abstraction for users
 */

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

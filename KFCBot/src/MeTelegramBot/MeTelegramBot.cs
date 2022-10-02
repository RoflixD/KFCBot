using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Collections.Generic;

namespace KFCBot.src.MeTelegramBot
{
    class MeTelegramBot
    {
        public static ITelegramBotClient BotClient { get; set; }
        public static List<Chat> AllChats = new List<Chat>();

        public void Start()
        {
            Console.WriteLine("Enter the password:");
            string keyStr = Console.ReadLine();
            if (keyStr == null && keyStr.Length < 0)
            {
                Console.WriteLine("Password can't be empty!");
            }

            try
            {
                int key = int.Parse(keyStr);
                string token = BotProps.Token;
                BotClient = new TelegramBotClient(MeEncrypter.CryptXOR(ref token, key));
                var cts = new CancellationTokenSource();
                var cancellationToken = cts.Token;
                var receiverOptions = new ReceiverOptions
                {
                    AllowedUpdates = { },
                };
                BotClient.StartReceiving(
                    HandleUpdateAsync,
                    HandleErrorAsync,
                    receiverOptions,
                    cancellationToken
                );
                Console.WriteLine($"{BotClient.GetMeAsync().Result.FirstName} has been initialized");
                Console.WriteLine("Ready to rock!))!)");
                ProcessCommand(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (!AllChats.Contains(update.Message.Chat)) 
            {
                AllChats.Add(update.Message.Chat);
            }
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {            
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            return null;
        }

        private void ProcessCommand(string command)
        {
            if(command == "exit")
            {
                return;
            }

            foreach(var cmd in BotProps.Commands)
            {
                if (cmd.IsCommand(command))
                {
                    Console.WriteLine("Yes, I can do it!");
                    cmd.Execute();
                }
            }            
            ProcessCommand(Console.ReadLine());
        } 
    }
}

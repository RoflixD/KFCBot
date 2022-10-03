#region SystemIncludes
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion
#region TelegramIncludes
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System.IO;
#endregion

namespace KFCBot.src.MeTelegramBot
{
    class MeTelegramBot
    {
        public static string UsersListPath = "";

        public static ITelegramBotClient BotClient { get; set; }
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

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message == null)
            {
                return;
            }
           
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if (update.Message.NewChatMembers != null)
                {
                    NewUser(update);
                }                
                foreach (var botCommand in BotProps.BotCommands)
                {
                    if (botCommand.IsCommand(update.Message.Text.ToLower()))
                    {
                        botCommand.Execute(update);
                        return;
                    }
                }                               
                await botClient.SendTextMessageAsync(update.Message.Chat, "Ниче не понял");
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
                    cmd.Execute();
                    ProcessCommand(Console.ReadLine());
                }
            }
            Console.WriteLine("Can't find this command!");
            ProcessCommand(Console.ReadLine());
        }

        //#1
        private void NewUser(Update update)
        {
            Console.WriteLine($"New user has been conected!\n" +
                        $"Id: {update.Id}; First Name: {update.Message.Chat.FirstName}; LastName: {update.Message.Chat.LastName} \n");

            if (!System.IO.File.Exists("Users.txt"))
            {
                Console.WriteLine("Have no users file! Creating it and write new user id in it!");
                using (StreamWriter sw = System.IO.File.CreateText("Users.txt"))
                {
                    sw.WriteLine(update.Message.Chat.Id.ToString());
                }
            }
            else
            {
                Console.WriteLine("File already exist! Just adding new user id in it!");
                using (StreamWriter sw = System.IO.File.AppendText("Users.txt"))
                {
                    sw.WriteLine(update.Message.Chat.Id.ToString());
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KFCBot.src.MeTelegramBot
{
    public class SimpleResponse : BaseBotCommand
    {
        public override async void Execute(Update update)
        {
            await MeTelegramBot.BotClient.SendTextMessageAsync(update.Message.Chat, "Где вопрос там и ответ!");
        }

        public override bool IsCommand(string cmd)
        {
            return cmd.ToLower() == "ответь";
        }

    }
}

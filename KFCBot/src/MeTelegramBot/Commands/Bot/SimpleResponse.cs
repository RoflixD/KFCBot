#region SystemIncludes
using System;
#endregion
#region TelegramIncludes
using Telegram.Bot;
using Telegram.Bot.Types;
#endregion
#region KFCBotIncludes
using KFCBot.src.MeTelegramBot.Commands;
#endregion

namespace KFCBot.src.MeTelegramBot
{
    public class SimpleResponse : BaseBotCommand
    {
        RoleTypes AccessLevel = RoleTypes.User | RoleTypes.SuperUser | RoleTypes.Admin;

        public override async void Execute(Update update)
        {
            if (!IsCommandAccessible(AccessLevel))
            {
                Console.WriteLine("You have no permission!");
                return;
            }
            await MeTelegramBot.BotClient.SendTextMessageAsync(update.Message.Chat, "Где вопрос там и ответ!");
        }

        public override bool IsCommand(string cmd)
        {
            return cmd.ToLower() == "ответь";
        }

        public override bool IsCommandAccessible(RoleTypes role)
        {
            return AccessLevel.HasFlag(role);
        }
    }
}

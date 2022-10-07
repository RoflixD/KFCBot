using KFCBot.src.MeTelegramBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace KFCBot.src.MeTelegramBot
{
    public abstract class BaseBotCommand
    {
        public virtual async void Execute(Update update) { }
        public abstract bool IsCommand(string cmd);
        public abstract bool IsCommandAccessible(RoleTypes role);
    }
}

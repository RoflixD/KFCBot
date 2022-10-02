using System;
using System.Collections.Generic;
using System.Text;

namespace KFCBot.src.MeTelegramBot
{
    public abstract class BaseCommand
    {
        public virtual async void Execute() { }

        public abstract bool IsCommand(string cmd);
    }
}

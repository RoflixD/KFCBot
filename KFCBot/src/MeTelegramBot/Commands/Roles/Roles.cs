using System;

namespace KFCBot.src.MeTelegramBot.Commands
{
    [Flags]
    public enum RoleTypes
    {
        Admin = 0,
        SuperUser = 1,
        User = 2
    }
}

using CommandSystem;
using System;

namespace GhostHunter_AutoEvent.Commands
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    class EventCommands : ICommand
    {
        public string Command { get; } = "GhostHuntersEvent Start";

        public string[] Aliases { get; } = {"ghosthuntersevent"};  

        public string Description { get; } = "Starts the Ghost Hunters event";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is CommandSender)
            {
                if (RoundSummary.RoundInProgress() == false)
                {
                    response = "Starting Ghost Hunters Event Next Round...";
                    Handlers.GhostHuntersEventStart.IsGhostHuntersEventGoingOn();
                    return true;
                }
                else
                {
                    response = "Can't iniciate Ghost Hunters event while round is in progress.";
                    return false;
                }
            }
            else
            {
                response = "You do not have permission to use this command";
                return false;
            }
        }
    }
}

using Exiled.API.Interfaces;
using System.ComponentModel;

namespace GhostHunter_AutoEvent
{
    public sealed class Config : IConfig
    {
        [Description("Wheter the plugin is Enabled or not.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug messages should be shown in the console.")]
        public bool Debug { get; set; } = false;

        [Description("Time that late joined players can still spawn.")]
        public static float LateJoinTime = 30;

        [Description("Amount of Particles Disruptor to be given to ntf players.")]
        public static int ParticleDisruptorQuantity = 2;
    }
}

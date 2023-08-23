using System;
using Exiled.API.Features;
using Exiled.API.Enums;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using GhostHunter_AutoEvent.Handlers;
using System.IO.Ports;
using Exiled.Events.EventArgs.Server;

namespace GhostHunter_AutoEvent
{
    public class GhostHunterAutoEvent : Plugin<Config> 
    {
        private static readonly Lazy<GhostHunterAutoEvent> Newinstance = new Lazy<GhostHunterAutoEvent>(valueFactory: () => new GhostHunterAutoEvent());
        public static GhostHunterAutoEvent Instance => Newinstance.Value;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private GhostHuntersEventStart player;
        private GhostHuntersEventStart server;
        private GhostHunterAutoEvent() 
        {
        }
        public override void OnEnabled()
        { 
                RegisterEvents();
        }
        public override void OnDisabled()
        {
            UnRegisterEvents();
        }
        public void RegisterEvents()
        {   
            server = new GhostHuntersEventStart();
            player = new GhostHuntersEventStart();
            
            Server.RoundStarted += server.OnEventStart;
            Server.RoundEnded += GhostHuntersEventStart.GhostHuntersEventEnded;
            Player.Joined += player.OnLatePlayerJoin;
            Server.RestartingRound += GhostHuntersEventStart.HasGhostHuntersEventEnded;
            Server.RespawningTeam += player.RespawneventNotAllowed;   

        }

        public void UnRegisterEvents()
        {
            Player.Joined -= player.OnLatePlayerJoin;
            Server.RoundStarted -= server.OnEventStart;
            Server.RoundEnded -= GhostHuntersEventStart.GhostHuntersEventEnded;
            Server.RestartingRound -= GhostHuntersEventStart.HasGhostHuntersEventEnded;
            Server.RespawningTeam -= player.RespawneventNotAllowed;

            player = null;
            server = null;
        }
    }
}


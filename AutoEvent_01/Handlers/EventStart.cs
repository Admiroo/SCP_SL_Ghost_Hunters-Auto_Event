using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Collections.Generic;
using Player = Exiled.API.Features.Player;
using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Server;
using System;

namespace GhostHunter_AutoEvent.Handlers
{
    public class GhostHuntersEventStart
    {
        public float LateJoinTimeGHEventPermited = Config.LateJoinTime;
        public static bool IsGHEventGoingOn = false;
        public static void IsGhostHuntersEventGoingOn()
        {
            IsGHEventGoingOn = true;
        }
        public static void GhostHuntersEventEnded(RoundEndedEventArgs ev)
        {
            
           
                IsGHEventGoingOn = false;
            
            
        }
        public static void HasGhostHuntersEventEnded()
        {


            IsGHEventGoingOn = false;


        }

        public void OnEventStart()
        {
            if (IsGHEventGoingOn == true)
            {
                System.Random rand = new System.Random();
                List<Player> PlyList = new List<Player>();
               

                Door.LockAll(5000, ZoneType.LightContainment);
                Door.Get(DoorType.ElevatorNuke).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.ElevatorScp049).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.GateB).IsOpen = true;
                Door.Get(DoorType.CheckpointEzHczA).BreakDoor(Interactables.Interobjects.DoorUtils.DoorDamageType.ServerCommand);
                Door.Get(DoorType.CheckpointEzHczB).BreakDoor(Interactables.Interobjects.DoorUtils.DoorDamageType.ServerCommand);
                Door.Get(DoorType.GateA).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.GateB).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.EscapePrimary).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.EscapeSecondary).Lock(5000, DoorLockType.NoPower);
                Door.Get(DoorType.SurfaceGate).Lock(5000, DoorLockType.NoPower);


                Map.Broadcast(10, "O evento Ghost Hunters começou...");

                foreach (Player ply in Player.List)
                {
                    PlyList.Add(ply);
                }
                double Z = PlyList.Count / 3;
                if (PlyList.Count <= 20)
                {
                    Math.Floor(Z);
                }
                else
                {
                    Math.Ceiling(Z);
                }

                for (int i = 0; i < Z ; i++)
                {
                    int RandPly = rand.Next(PlyList.Count);
                    Player Selected106 = PlyList[RandPly];
                    Selected106.Role.Set(RoleTypeId.Scp106);
                    Selected106.Position = RoleExtensions.GetRandomSpawnLocation(RoleTypeId.Scp106).Position;
                    PlyList.RemoveAt(RandPly);
                }
                foreach (Player ply in PlyList)
                {
                    ply.Role.Set(RoleTypeId.NtfSergeant);
                    ply.ClearInventory();
                    for(int number = 0; number < Config.ParticleDisruptorQuantity; number++)
                    {
                        ply.AddItem(ItemType.ParticleDisruptor);
                    }
                    ply.AddItem(ItemType.Radio);
                    ply.AddItem(ItemType.Flashlight);
                    ply.AddItem(ItemType.ArmorHeavy);
                    int GrenadeChance = rand.Next(1,3);
                    if (GrenadeChance == 1)
                    {
                        ply.AddItem(ItemType.GrenadeHE);

                    }
                    ply.Position = RoleExtensions.GetRandomSpawnLocation(RoleTypeId.NtfSergeant).Position;
                }
            }
        }
      
        public void OnLatePlayerJoin(JoinedEventArgs ev)
        {
             if (IsGHEventGoingOn == true && RoundSummary.RoundInProgress() && RoundSummary.roundTime < LateJoinTimeGHEventPermited)
                {
                    ev.Player.Role.Set(RoleTypeId.NtfSergeant);
                    ev.Player.AddItem(ItemType.ParticleDisruptor);
                    ev.Player.AddItem(ItemType.ParticleDisruptor);
                    ev.Player.AddItem(ItemType.ParticleDisruptor);
                    ev.Player.Position = RoleExtensions.GetRandomSpawnLocation(RoleTypeId.NtfSergeant).Position;
            }     
        }
        public void RespawneventNotAllowed(RespawningTeamEventArgs ev)
        {
            if (IsGHEventGoingOn == true)
            {
                ev.IsAllowed = false;
            }
        }
    }   
}   

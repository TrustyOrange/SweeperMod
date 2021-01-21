using System.Collections.Generic;
using HarmonyLib;
using Hazel;
using UnhollowerBaseLib;
using PlayerControl = FFGALNAPKCD;
using GameDataPlayerInfo = EGLJNOMOGNP.DCJMABDDJCF;
using AmongUsClient = FMLLKEACGIO;

namespace SweeperMod
{
	[HarmonyPatch]
	public static class PlayerControlPatch
	{
		public static PlayerControl Sweeper;
		
		[HarmonyPatch(typeof(PlayerControl), "HandleRpc")]
		public static void Postfix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
		{
			if (callId == 42)
			{
				byte readByte = reader.ReadByte();
				foreach (PlayerControl player in PlayerControl.AllPlayerControls)
				{
					if (player.PlayerId == readByte)
					{
						PlayerControlPatch.Sweeper = player;
					}
				}
			}
		}

		public static bool IsSweeper(PlayerControl player)
		{
			return player.PlayerId == PlayerControlPatch.Sweeper.PlayerId;
		}

		public static List<PlayerControl> getCrewMates(Il2CppReferenceArray<GameDataPlayerInfo> infection)
		{
			List<PlayerControl> list = new List<PlayerControl>();
			foreach (PlayerControl player in PlayerControl.AllPlayerControls)
			{
				bool isImpostor = false;
				foreach (GameDataPlayerInfo player1 in infection)
				{
					if (player.PlayerId == player1.LAOEJKHLKAI.PlayerId)
					{
						isImpostor = true;
						break;
					}
				}
				if (!isImpostor)
				{
					list.Add(player);
				}
			}
			return list;
		}


		[HarmonyPatch(typeof(PlayerControl), "RpcSetInfected")]
		public static void Postfix([HarmonyArgument(0)] Il2CppReferenceArray<GameDataPlayerInfo> infected)
		{
			MessageWriter messageWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, 42, SendOption.None, -1);
			List<PlayerControl> crewMates = PlayerControlPatch.getCrewMates(infected);
			System.Random random = new System.Random();
			PlayerControlPatch.Sweeper = crewMates[random.Next(0, crewMates.Count)];
			byte playerId = PlayerControlPatch.Sweeper.PlayerId;
			messageWriter.Write(playerId);
			AmongUsClient.Instance.FinishRpcImmediately(messageWriter);
		}
	}
}

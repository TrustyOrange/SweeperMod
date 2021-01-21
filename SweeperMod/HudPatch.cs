using HarmonyLib;
using UnityEngine;
using MeetingHud = OOCJALPKPEP;
using PlayerVoteArea = HDJGDMFCHDN;
using HudManager = PIEFJFEOGOL;
using PlayerControl = FFGALNAPKCD;

namespace SweeperMod
{
	[HarmonyPatch]
	public static class HudPatch
	{
		public static void updateMeetingHUD(MeetingHud __instance)
		{
			foreach (PlayerVoteArea player in __instance.HBDFFAHBIGI)
			{
				if (player.NameText.Text == PlayerControlPatch.Sweeper.name && PlayerControlPatch.IsSweeper(PlayerControl.LocalPlayer))
				{
					player.NameText.Color = new Color(0f, 0.33f, 0.79f, 1f);
				}
			}
		}

		[HarmonyPatch(typeof(HudManager), "Update")]
		public static void Postfix(HudManager __instance)
		{
			if (MeetingHud.Instance != null)
			{
				HudPatch.updateMeetingHUD(MeetingHud.Instance);
			}
			if (PlayerControl.AllPlayerControls.Count > 1 && PlayerControlPatch.Sweeper != null && PlayerControlPatch.IsSweeper(PlayerControl.LocalPlayer))
			{
				PlayerControl.LocalPlayer.nameText.Color = new Color(0f, 0.33f, 0.79f, 1f);
			}
		}

		private static System.Random random = new System.Random();
	}
}

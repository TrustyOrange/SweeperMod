using HarmonyLib;
using UnityEngine;
using IntroCutscene_CoBegin_d__10 = PENEIDJGGAF.CKACLKCOJFO;
using PlayerControl = FFGALNAPKCD;

namespace SweeperMod
{
	[HarmonyPatch]
	public static class IntroCutScenePatch
	{
		[HarmonyPatch(typeof(IntroCutscene_CoBegin_d__10), "MoveNext")]
		public static void Postfix(IntroCutscene_CoBegin_d__10 __instance)
		{
			if (PlayerControlPatch.IsSweeper(PlayerControl.LocalPlayer))
			{
				__instance.field_Public_PENEIDJGGAF_0.Title.Text = "Sweeper";
				__instance.field_Public_PENEIDJGGAF_0.Title.Color = new Color(0f, 0.33f, 0.79f, 1f);
				__instance.field_Public_PENEIDJGGAF_0.ImpostorText.Text = "Use vents to move around";
				__instance.field_Public_PENEIDJGGAF_0.BackgroundBar.material.color = new Color(0f, 0.33f, 0.79f, 1f);
			}
		}
	}
}

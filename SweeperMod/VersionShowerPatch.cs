using HarmonyLib;
using VersionShower = BOCOFLHKCOJ;

namespace SweeperMod
{
	[HarmonyPatch(typeof(VersionShower), "Start")]
	public static class VersionShowerPatch
	{
		public static void Postfix(VersionShower __instance)
		{
			AELDHKGBIFD text = __instance.text;
			text.Text += " + [0054C9FF]Sweeper[] Mod by Tomozbot";
		}
	}
}

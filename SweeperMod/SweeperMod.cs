using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;

namespace SweeperMod
{
	[BepInPlugin("gg.tomozbot.sweepermod", "Sweeper Mod", "1.0.0")]
	public class SweeperMod : BasePlugin
	{
		public override void Load()
		{
			this._harmony = new Harmony("gg.tomozbot.sweepermod");
			this._harmony.PatchAll();
		}

		private Harmony _harmony;
	}
}

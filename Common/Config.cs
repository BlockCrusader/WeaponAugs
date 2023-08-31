using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WeaponAugs.Common
{
	
	public class ConfigClient : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;
		[DefaultValue(true)]
		public bool AugmentDust;
	}
	

	public class ConfigServer : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;
		
		[Range(1, 5)]
		[DefaultValue(1)]
		public int AugmentLimit;

		[DefaultValue(true)]
		public bool PatchTooltips;

		[DefaultValue(true)]
		public bool CustomDeathMessages;

		[DefaultValue(false)]
		public bool MoreShards;

		[DefaultValue(true)]
		public bool ProgressShards;

		[DefaultValue(false)]
		public bool MasterShards;
	}
}

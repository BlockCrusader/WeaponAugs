using Terraria;
using Terraria.ID;
using Terraria.Localization;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Ultimate
{
	public class RunicJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Runic;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Runic (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.RunicUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 0;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Purple;
		}
	}
}
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Rare
{
	public class RelentlessJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Relentless;
		public override AugTier Tier => AugTier.Rare;


		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Relentless (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.RelentlessRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 0;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class RelentlessJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Relentless;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Relentless (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.RelentlessEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<RelentlessJewelRar>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class RelentlessJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Relentless;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Relentless (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.RelentlessUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<RelentlessJewelRar>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
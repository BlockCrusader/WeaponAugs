using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class SturdyJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Sturdy;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Sturdy (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.SturdyUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 0;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class SturdyJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Sturdy;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Sturdy (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.SturdyRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<SturdyJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class SturdyJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Sturdy;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Sturdy (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.SturdyEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<SturdyJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class SturdyJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Sturdy;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Sturdy (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.SturdyUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<SturdyJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
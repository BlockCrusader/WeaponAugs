using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class UpliftingJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Uplifting;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Uplifting (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.UpliftingUnc}";
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

	public class UpliftingJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Uplifting;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Uplifting (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.UpliftingRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UpliftingJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class UpliftingJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Uplifting;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Uplifting (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.UpliftingEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UpliftingJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class UpliftingJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Uplifting;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Uplifting (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.UpliftingUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UpliftingJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
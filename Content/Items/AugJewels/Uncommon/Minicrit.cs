using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class MinicritJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Minicrit;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Minicrit (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.MinicritUnc}";
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

	public class MinicritJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Minicrit;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Minicrit (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.MinicritRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<MinicritJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class MinicritJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Minicrit;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Minicrit (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.MinicritEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<MinicritJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class MinicritJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Minicrit;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Minicrit (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.MinicritUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<MinicritJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
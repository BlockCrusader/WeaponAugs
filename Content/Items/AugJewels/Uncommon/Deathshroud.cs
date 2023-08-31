using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class DeathshroudJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Deathshroud;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Deathshroud (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.DeathshroudUnc}";
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

	public class DeathshroudJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Deathshroud;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Deathshroud (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.DeathshroudRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<DeathshroudJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class DeathshroudJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Deathshroud;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Deathshroud (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.DeathshroudEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<DeathshroudJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class DeathshroudJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Deathshroud;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Deathshroud (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.DeathshroudUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<DeathshroudJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
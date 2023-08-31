using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class MegastrikeJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Megastrike;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Megastrike (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.MegastrikeUnc}";
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

	public class MegastrikeJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Megastrike;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Megastrike (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.MegastrikeRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<MegastrikeJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class MegastrikeJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Megastrike;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Megastrike (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.MegastrikeEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<MegastrikeJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class MegastrikeJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Megastrike;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Megastrike (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.MegastrikeUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<MegastrikeJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
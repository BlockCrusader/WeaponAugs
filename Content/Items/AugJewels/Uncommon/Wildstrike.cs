using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class WildstrikeJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Wildstrike;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Wildstrike (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.WildstrikeUnc}";
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

	public class WildstrikeJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Wildstrike;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Wildstrike (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.WildstrikeRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<WildstrikeJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class WildstrikeJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Wildstrike;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Wildstrike (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.WildstrikeEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<WildstrikeJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class WildstrikeJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Wildstrike;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Wildstrike (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.WildstrikeUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<WildstrikeJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
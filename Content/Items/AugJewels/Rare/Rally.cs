using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Rare
{
	public class RallyJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Rally;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Rally") + Language.GetTextValue("Mods.WeaponAugs.Items.Rare");
		private string AugTooltipValue => $"{AugPowerArchive.RallyRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
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

	public class RallyJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Rally;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Rally") + Language.GetTextValue("Mods.WeaponAugs.Items.Epic");
		private string AugTooltipValue => $"{AugPowerArchive.RallyEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<RallyJewelRar>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class RallyJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Rally;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Rally") + Language.GetTextValue("Mods.WeaponAugs.Items.Ultimate");
		private string AugTooltipValue => $"{AugPowerArchive.RallyUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<RallyJewelRar>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
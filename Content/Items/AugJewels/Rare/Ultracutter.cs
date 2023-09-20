using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Rare
{
	public class UltracutterJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Ultracutter;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ultracutter") + Language.GetTextValue("Mods.WeaponAugs.Items.Rare");
		private string AugTooltipValue => $"{AugPowerArchive.UltracutterRar}";
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

	public class UltracutterJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Ultracutter;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ultracutter") + Language.GetTextValue("Mods.WeaponAugs.Items.Epic");
		private string AugTooltipValue => $"{AugPowerArchive.UltracutterEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UltracutterJewelRar>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class UltracutterJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Ultracutter;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ultracutter") + Language.GetTextValue("Mods.WeaponAugs.Items.Ultimate");
		private string AugTooltipValue => $"{AugPowerArchive.UltracutterUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UltracutterJewelRar>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
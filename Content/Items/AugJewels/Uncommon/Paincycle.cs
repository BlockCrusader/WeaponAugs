using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class PaincycleJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Paincycle;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Paincycle") + Language.GetTextValue("Mods.WeaponAugs.Items.Uncommon");
		private string AugTooltipValue => $"{AugPowerArchive.PaincycleUnc}";
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

	public class PaincycleJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Paincycle;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Paincycle") + Language.GetTextValue("Mods.WeaponAugs.Items.Rare");
		private string AugTooltipValue => $"{AugPowerArchive.PaincycleRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PaincycleJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class PaincycleJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Paincycle;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Paincycle") + Language.GetTextValue("Mods.WeaponAugs.Items.Epic");
		private string AugTooltipValue => $"{AugPowerArchive.PaincycleEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PaincycleJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class PaincycleJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Paincycle;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Paincycle") + Language.GetTextValue("Mods.WeaponAugs.Items.Ultimate");
		private string AugTooltipValue => $"{AugPowerArchive.PaincycleUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PaincycleJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Uncommon
{
	public class BarrageJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Barrage;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Barrage") + Language.GetTextValue("Mods.WeaponAugs.Items.Uncommon");
		private string AugTooltipValue => $"{AugPowerArchive.BarrageUnc}";
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

	public class BarrageJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Barrage;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Barrage") + Language.GetTextValue("Mods.WeaponAugs.Items.Rare");
		private string AugTooltipValue => $"{AugPowerArchive.BarrageRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<BarrageJewelUnc>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class BarrageJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Barrage;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Barrage") + Language.GetTextValue("Mods.WeaponAugs.Items.Epic");
		private string AugTooltipValue => $"{AugPowerArchive.BarrageEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<BarrageJewelUnc>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class BarrageJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Barrage;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Barrage") + Language.GetTextValue("Mods.WeaponAugs.Items.Ultimate");
		private string AugTooltipValue => $"{AugPowerArchive.BarrageUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<BarrageJewelUnc>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
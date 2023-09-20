using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class ForceJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Force;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas"; 
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Force") + Language.GetTextValue("Mods.WeaponAugs.Items.Basic");
		private string AugTooltipValue => $"{AugPowerArchive.ForceBas}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelBas;
			ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.value = 0;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.White;
		}
	}

	public class ForceJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Force;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Force") + Language.GetTextValue("Mods.WeaponAugs.Items.Uncommon");
		private string AugTooltipValue => $"{AugPowerArchive.ForceUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ForceJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class ForceJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Force;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Force") + Language.GetTextValue("Mods.WeaponAugs.Items.Rare");
		private string AugTooltipValue => $"{AugPowerArchive.ForceRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ForceJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class ForceJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Force;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Force") + Language.GetTextValue("Mods.WeaponAugs.Items.Epic");
		private string AugTooltipValue => $"{AugPowerArchive.ForceEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ForceJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class ForceJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Force;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Force") + Language.GetTextValue("Mods.WeaponAugs.Items.Ultimate");
		private string AugTooltipValue => $"{AugPowerArchive.ForceUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ForceJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
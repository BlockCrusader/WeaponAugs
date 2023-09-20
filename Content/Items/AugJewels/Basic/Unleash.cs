using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class UnleashJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Unleash;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas"; 
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Unleash") + Language.GetTextValue("Mods.WeaponAugs.Items.Basic");
		private string AugTooltipValue => $"{AugPowerArchive.UnleashBas}";
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

	public class UnleashJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Unleash;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Unleash") + Language.GetTextValue("Mods.WeaponAugs.Items.Uncommon");
		private string AugTooltipValue => $"{AugPowerArchive.UnleashUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UnleashJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class UnleashJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Unleash;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Unleash") + Language.GetTextValue("Mods.WeaponAugs.Items.Rare");
		private string AugTooltipValue => $"{AugPowerArchive.UnleashRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UnleashJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class UnleashJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Unleash;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Unleash") + Language.GetTextValue("Mods.WeaponAugs.Items.Epic");
		private string AugTooltipValue => $"{AugPowerArchive.UnleashEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UnleashJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class UnleashJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Unleash;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Unleash") + Language.GetTextValue("Mods.WeaponAugs.Items.Ultimate");
		private string AugTooltipValue => $"{AugPowerArchive.UnleashUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<UnleashJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
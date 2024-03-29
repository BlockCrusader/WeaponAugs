using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class IgniteJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Ignite;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ignite") + Language.GetTextValue("Mods.WeaponAugs.Items.Basic");
		// private string AugTooltipValue => $"{AugPowerArchive.IgniteBas}";
		private string AugTooltipValue => Language.GetTextValue("BuffName.OnFire");
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

	public class IgniteJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Ignite;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ignite") + Language.GetTextValue("Mods.WeaponAugs.Items.Uncommon");
		// private string AugTooltipValue => $"{AugPowerArchive.IgniteUnc}";
		private string AugTooltipValue => Language.GetTextValue("BuffName.Frostburn");
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<IgniteJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class IgniteJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Ignite;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ignite") + Language.GetTextValue("Mods.WeaponAugs.Items.Rare");
		// private string AugTooltipValue => $"{AugPowerArchive.IgniteRar}";
		private string AugTooltipValue => Language.GetTextValue("BuffName.Shadowflame");
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<IgniteJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class IgniteJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Ignite;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ignite") + Language.GetTextValue("Mods.WeaponAugs.Items.Epic");
		// private string AugTooltipValue => $"{AugPowerArchive.IgniteEpi}";
		private string AugTooltipValue => Language.GetTextValue("BuffName.CursedInferno");
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<IgniteJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class IgniteJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Ignite;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => Language.GetTextValue("Mods.WeaponAugs.Items.Ignite") + Language.GetTextValue("Mods.WeaponAugs.Items.Ultimate");
		// private string AugTooltipValue => $"{AugPowerArchive.IgniteUlt}";
		private string AugTooltipValue => Language.GetTextValue("Mods.WeaponAugs.Buffs.RunicBlaze.DisplayName");
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<IgniteJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
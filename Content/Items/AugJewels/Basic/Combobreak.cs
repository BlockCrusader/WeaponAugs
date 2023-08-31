using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class CombobreakJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Combobreak;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas"; 
		private string AugTitle => "Combobreak (Basic)";
		private string AugTooltipValue => $"{AugPowerArchive.CombobreakBas}";
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

	public class CombobreakJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Combobreak;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Combobreak (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.CombobreakUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<CombobreakJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class CombobreakJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Combobreak;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Combobreak (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.CombobreakRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<CombobreakJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class CombobreakJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Combobreak;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Combobreak (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.CombobreakEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<CombobreakJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class CombobreakJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Combobreak;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Combobreak (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.CombobreakUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<CombobreakJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
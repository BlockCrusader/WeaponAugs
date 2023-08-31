using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class PrecisionJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Precision;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas";
		private string AugTitle => "Precision (Basic)";
		private string AugTooltipValue => $"{AugPowerArchive.PrecisionBas}";
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

	public class PrecisionJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Precision;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Precision (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.PrecisionUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PrecisionJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class PrecisionJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Precision;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Precision (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.PrecisionRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PrecisionJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class PrecisionJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Precision;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Precision (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.PrecisionEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PrecisionJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class PrecisionJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Precision;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Precision (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.PrecisionUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PrecisionJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
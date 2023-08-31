using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class DireJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Dire;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas"; 
		private string AugTitle => "Dire (Basic)";
		private string AugTooltipValue => $"{AugPowerArchive.DireBas}";
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

	public class DireJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Dire;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Dire (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.DireUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<DireJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class DireJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Dire;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Dire (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.DireRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<DireJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class DireJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Dire;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Dire (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.DireEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<DireJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class DireJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Dire;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Dire (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.DireUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<DireJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
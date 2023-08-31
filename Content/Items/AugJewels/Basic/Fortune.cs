using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class FortuneJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Fortune;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas";
		private string AugTitle => "Fortune (Basic)";
		private string AugTooltipValue => $"{AugPowerArchive.FortuneBas}";
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

	public class FortuneJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Fortune;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Fortune (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.FortuneUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<FortuneJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class FortuneJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Fortune;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Fortune (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.FortuneRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<FortuneJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class FortuneJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Fortune;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Fortune (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.FortuneEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<FortuneJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class FortuneJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Fortune;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Fortune (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.FortuneUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<FortuneJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
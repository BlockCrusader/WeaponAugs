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
		private string AugTitle => "Ignite (Basic)";
		private string AugTooltipValue => $"{AugPowerArchive.IgniteBas}";
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
		private string AugTitle => "Ignite (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.IgniteUnc}";
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
		private string AugTitle => "Ignite (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.IgniteRar}";
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
		private string AugTitle => "Ignite (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.IgniteEpi}";
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
		private string AugTitle => "Ignite (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.IgniteUlt}";
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
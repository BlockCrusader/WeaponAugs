using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels.Basic
{
	public class ArmorbaneJewelBas : AugmentJewel
	{
		public override AugType Augment => AugType.Armorbane;
		public override AugTier Tier => AugTier.Basic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelBas"; 
		private string AugTitle => "Armorbane (Basic)";
		private string AugTooltipValue => $"{AugPowerArchive.ArmorbaneBas}";
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

	public class ArmorbaneJewelUnc : AugmentJewel
	{
		public override AugType Augment => AugType.Armorbane;
		public override AugTier Tier => AugTier.Uncommon;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUnc"; 
		private string AugTitle => "Armorbane (Uncommon)";
		private string AugTooltipValue => $"{AugPowerArchive.ArmorbaneUnc}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUnc;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ArmorbaneJewelBas>());
			Item.rare = ItemRarityID.Orange;
		}
	}

	public class ArmorbaneJewelRar : AugmentJewel
	{
		public override AugType Augment => AugType.Armorbane;
		public override AugTier Tier => AugTier.Rare;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelRar";
		private string AugTitle => "Armorbane (Rare)";
		private string AugTooltipValue => $"{AugPowerArchive.ArmorbaneRar}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelRar;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ArmorbaneJewelBas>());
			Item.rare = ItemRarityID.Lime;
		}
	}

	public class ArmorbaneJewelEpi : AugmentJewel
	{
		public override AugType Augment => AugType.Armorbane;
		public override AugTier Tier => AugTier.Epic;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelEpi";
		private string AugTitle => "Armorbane (Epic)";
		private string AugTooltipValue => $"{AugPowerArchive.ArmorbaneEpi}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelEpi;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ArmorbaneJewelBas>());
			Item.rare = ItemRarityID.Cyan;
		}
	}

	public class ArmorbaneJewelUlt : AugmentJewel
	{
		public override AugType Augment => AugType.Armorbane;
		public override AugTier Tier => AugTier.Ultimate;

		
		public override string Texture => "WeaponAugs/Content/Items/AugJewels/AugJewelUlt";
		private string AugTitle => "Armorbane (Ultimate)";
		private string AugTooltipValue => $"{AugPowerArchive.ArmorbaneUlt}";
		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AugTitle, AugTooltipValue);

		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.JewelUlt;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<ArmorbaneJewelBas>());
			Item.rare = ItemRarityID.Purple;
		}
	}
}
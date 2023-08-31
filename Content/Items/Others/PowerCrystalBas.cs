using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;
using WeaponAugs.Content.Items.AugJewels.Basic;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerCrystalBas : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.CrystalBas;
			ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = 0;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.White;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void ModifyItemLoot(ItemLoot itemLoot)
		{
			int[] lootPool = new int[] {
				ModContent.ItemType<ArcanaJewelBas>(),
				ModContent.ItemType<ArmorbaneJewelBas>(),
				ModContent.ItemType<BallisticJewelBas>(),
				ModContent.ItemType<BattlelustJewelBas>(),
				ModContent.ItemType<CombobreakJewelBas>(),
				ModContent.ItemType<ConservationJewelBas>(),
				ModContent.ItemType<DireJewelBas>(),
				ModContent.ItemType<FinisherJewelBas>(),
				ModContent.ItemType<ForceJewelBas>(),
				ModContent.ItemType<FortuneJewelBas>(),
				ModContent.ItemType<FrenzyJewelBas>(),
				ModContent.ItemType<HeartsurgeJewelBas>(),
				ModContent.ItemType<IgniteJewelBas>(),
				ModContent.ItemType<MightJewelBas>(),
				ModContent.ItemType<OverflowJewelBas>(),
				ModContent.ItemType<PrecisionJewelBas>(),
				ModContent.ItemType<RushJewelBas>(),
				ModContent.ItemType<TaintJewelBas>(),
				ModContent.ItemType<TitanreachJewelBas>(),
				ModContent.ItemType<UnleashJewelBas>()
			};

			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, lootPool));
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient<PowerShardBas>(4)
				.AddTile(TileID.Furnaces)
				.Register();
		}
    }
}
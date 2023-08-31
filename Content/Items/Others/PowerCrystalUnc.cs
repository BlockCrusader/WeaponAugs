using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;
using WeaponAugs.Content.Items.AugJewels.Basic;
using WeaponAugs.Content.Items.AugJewels.Uncommon;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerCrystalUnc : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.CrystalUnc;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerCrystalBas>());
			Item.rare = ItemRarityID.Orange;
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
				// New
				ModContent.ItemType<BarrageJewelUnc>(),
				ModContent.ItemType<DeathechoJewelUnc>(),
				ModContent.ItemType<DeathshroudJewelUnc>(),
				ModContent.ItemType<DiversionJewelUnc>(),
				ModContent.ItemType<LightweightJewelUnc>(),
				ModContent.ItemType<LuckJewelUnc>(),
				ModContent.ItemType<MegastrikeJewelUnc>(),
				ModContent.ItemType<MinicritJewelUnc>(),
				ModContent.ItemType<PaincycleJewelUnc>(),
				ModContent.ItemType<RendJewelUnc>(),
				ModContent.ItemType<RevitalizeJewelUnc>(),
				ModContent.ItemType<SiphonJewelUnc>(),
				ModContent.ItemType<SturdyJewelUnc>(),
				ModContent.ItemType<UpliftingJewelUnc>(),
				ModContent.ItemType<WardJewelUnc>(),
				ModContent.ItemType<WildstrikeJewelUnc>(),
				// Upgrades (Basic)
				ModContent.ItemType<ArcanaJewelUnc>(),
				ModContent.ItemType<ArmorbaneJewelUnc>(),
				ModContent.ItemType<BallisticJewelUnc>(),
				ModContent.ItemType<BattlelustJewelUnc>(),
				ModContent.ItemType<CombobreakJewelUnc>(),
				ModContent.ItemType<ConservationJewelUnc>(),
				ModContent.ItemType<DireJewelUnc>(),
				ModContent.ItemType<FinisherJewelUnc>(),
				ModContent.ItemType<ForceJewelUnc>(),
				ModContent.ItemType<FortuneJewelUnc>(),
				ModContent.ItemType<FrenzyJewelUnc>(),
				ModContent.ItemType<HeartsurgeJewelUnc>(),
				ModContent.ItemType<IgniteJewelUnc>(),
				ModContent.ItemType<MightJewelUnc>(),
				ModContent.ItemType<OverflowJewelUnc>(),
				ModContent.ItemType<PrecisionJewelUnc>(),
				ModContent.ItemType<RushJewelUnc>(),
				ModContent.ItemType<TaintJewelUnc>(),
				ModContent.ItemType<TitanreachJewelUnc>(),
				ModContent.ItemType<UnleashJewelUnc>()
			};

			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, lootPool));
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient<PowerShardUnc>(4)
				.AddTile(TileID.Furnaces)
				.Register();
		}
    }
}
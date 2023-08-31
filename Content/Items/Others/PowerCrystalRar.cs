using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;
using WeaponAugs.Content.Items.AugJewels.Basic;
using WeaponAugs.Content.Items.AugJewels.Rare;
using WeaponAugs.Content.Items.AugJewels.Uncommon;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerCrystalRar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.CrystalRar;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerCrystalBas>());
			Item.rare = ItemRarityID.Lime;
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
				ModContent.ItemType<RelentlessJewelRar>(),
				ModContent.ItemType<BreakerJewelRar>(),
				ModContent.ItemType<DeterminationJewelRar>(),
				ModContent.ItemType<HypercritJewelRar>(),
				ModContent.ItemType<KingslayerJewelRar>(),
				ModContent.ItemType<OverdriveJewelRar>(),
				ModContent.ItemType<RallyJewelRar>(),
				ModContent.ItemType<ReapingJewelRar>(),
				ModContent.ItemType<SuperluckJewelRar>(),
				ModContent.ItemType<UltracutterJewelRar>(),
				ModContent.ItemType<UnstableJewelRar>(),
				ModContent.ItemType<VigorJewelRar>(),
				// Updgrades (Uncommon)
				ModContent.ItemType<BarrageJewelRar>(),
				ModContent.ItemType<DeathechoJewelRar>(),
				ModContent.ItemType<DeathshroudJewelRar>(),
				ModContent.ItemType<DiversionJewelRar>(),
				ModContent.ItemType<LightweightJewelRar>(),
				ModContent.ItemType<LuckJewelRar>(),
				ModContent.ItemType<MegastrikeJewelRar>(),
				ModContent.ItemType<MinicritJewelRar>(),
				ModContent.ItemType<PaincycleJewelRar>(),
				ModContent.ItemType<RendJewelRar>(),
				ModContent.ItemType<RevitalizeJewelRar>(),
				ModContent.ItemType<SiphonJewelRar>(),
				ModContent.ItemType<SturdyJewelRar>(),
				ModContent.ItemType<UpliftingJewelRar>(),
				ModContent.ItemType<WardJewelRar>(),
				ModContent.ItemType<WildstrikeJewelRar>(),
				// Upgrades (Basic)
				ModContent.ItemType<ArcanaJewelRar>(),
				ModContent.ItemType<ArmorbaneJewelRar>(),
				ModContent.ItemType<BallisticJewelRar>(),
				ModContent.ItemType<BattlelustJewelRar>(),
				ModContent.ItemType<CombobreakJewelRar>(),
				ModContent.ItemType<ConservationJewelRar>(),
				ModContent.ItemType<DireJewelRar>(),
				ModContent.ItemType<FinisherJewelRar>(),
				ModContent.ItemType<ForceJewelRar>(),
				ModContent.ItemType<FortuneJewelRar>(),
				ModContent.ItemType<FrenzyJewelRar>(),
				ModContent.ItemType<HeartsurgeJewelRar>(),
				ModContent.ItemType<IgniteJewelRar>(),
				ModContent.ItemType<MightJewelRar>(),
				ModContent.ItemType<OverflowJewelRar>(),
				ModContent.ItemType<PrecisionJewelRar>(),
				ModContent.ItemType<RushJewelRar>(),
				ModContent.ItemType<TaintJewelRar>(),
				ModContent.ItemType<TitanreachJewelRar>(),
				ModContent.ItemType<UnleashJewelRar>()
			};

			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, lootPool));
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient<PowerShardRar>(4)
				.AddTile(TileID.Hellforge)
				.Register();
		}
    }
}
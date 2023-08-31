using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;
using WeaponAugs.Content.Items.AugJewels.Basic;
using WeaponAugs.Content.Items.AugJewels.Epic;
using WeaponAugs.Content.Items.AugJewels.Rare;
using WeaponAugs.Content.Items.AugJewels.Ultimate;
using WeaponAugs.Content.Items.AugJewels.Uncommon;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerCrystalUlt : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.CrystalUlt;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerCrystalBas>());
			Item.rare = ItemRarityID.Purple;
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
				ModContent.ItemType<ExecutionJewelUlt>(),
				ModContent.ItemType<PowertheftJewelUlt>(),
				ModContent.ItemType<ResurgenceJewelUlt>(),
				ModContent.ItemType<RunicJewelUlt>(),
				// Upgrades (Epic)
				ModContent.ItemType<BlastJewelUlt>(),
				ModContent.ItemType<CommittedJewelUlt>(),
				ModContent.ItemType<LifeleechJewelUlt>(),
				ModContent.ItemType<MomentumJewelUlt>(),
				ModContent.ItemType<RadianceJewelUlt>(),
				ModContent.ItemType<RelentlessJewelUlt>(),
				ModContent.ItemType<SuperchargeJewelUlt>(),
				ModContent.ItemType<VoidicJewelUlt>(),
				// Upgrades (Rare)
				ModContent.ItemType<CollateralJewelUlt>(),
				ModContent.ItemType<BreakerJewelUlt>(),
				ModContent.ItemType<DeterminationJewelUlt>(),
				ModContent.ItemType<HypercritJewelUlt>(),
				ModContent.ItemType<KingslayerJewelUlt>(),
				ModContent.ItemType<OverdriveJewelUlt>(),
				ModContent.ItemType<RallyJewelUlt>(),
				ModContent.ItemType<ReapingJewelUlt>(),
				ModContent.ItemType<SuperluckJewelUlt>(),
				ModContent.ItemType<UltracutterJewelUlt>(),
				ModContent.ItemType<UnstableJewelUlt>(),
				ModContent.ItemType<VigorJewelUlt>(),
				// Updgrades (Uncommon)
				ModContent.ItemType<BarrageJewelUlt>(),
				ModContent.ItemType<DeathechoJewelUlt>(),
				ModContent.ItemType<DeathshroudJewelUlt>(),
				ModContent.ItemType<DiversionJewelUlt>(),
				ModContent.ItemType<LightweightJewelUlt>(),
				ModContent.ItemType<LuckJewelUlt>(),
				ModContent.ItemType<MegastrikeJewelUlt>(),
				ModContent.ItemType<MinicritJewelUlt>(),
				ModContent.ItemType<PaincycleJewelUlt>(),
				ModContent.ItemType<RendJewelUlt>(),
				ModContent.ItemType<RevitalizeJewelUlt>(),
				ModContent.ItemType<SiphonJewelUlt>(),
				ModContent.ItemType<SturdyJewelUlt>(),
				ModContent.ItemType<UpliftingJewelUlt>(),
				ModContent.ItemType<WardJewelUlt>(),
				ModContent.ItemType<WildstrikeJewelUlt>(),
				// Upgrades (Basic)
				ModContent.ItemType<ArcanaJewelUlt>(),
				ModContent.ItemType<ArmorbaneJewelUlt>(),
				ModContent.ItemType<BallisticJewelUlt>(),
				ModContent.ItemType<BattlelustJewelUlt>(),
				ModContent.ItemType<CombobreakJewelUlt>(),
				ModContent.ItemType<ConservationJewelUlt>(),
				ModContent.ItemType<DireJewelUlt>(),
				ModContent.ItemType<FinisherJewelUlt>(),
				ModContent.ItemType<ForceJewelUlt>(),
				ModContent.ItemType<FortuneJewelUlt>(),
				ModContent.ItemType<FrenzyJewelUlt>(),
				ModContent.ItemType<HeartsurgeJewelUlt>(),
				ModContent.ItemType<IgniteJewelUlt>(),
				ModContent.ItemType<MightJewelUlt>(),
				ModContent.ItemType<OverflowJewelUlt>(),
				ModContent.ItemType<PrecisionJewelUlt>(),
				ModContent.ItemType<RushJewelUlt>(),
				ModContent.ItemType<TaintJewelUlt>(),
				ModContent.ItemType<TitanreachJewelUlt>(),
				ModContent.ItemType<UnleashJewelUlt>()
			};

			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, lootPool));
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient<PowerShardUlt>(4)
				.AddTile(TileID.AdamantiteForge)
				.Register();
		}
    }
}
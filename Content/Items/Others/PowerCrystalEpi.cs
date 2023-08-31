using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;
using WeaponAugs.Content.Items.AugJewels.Basic;
using WeaponAugs.Content.Items.AugJewels.Epic;
using WeaponAugs.Content.Items.AugJewels.Rare;
using WeaponAugs.Content.Items.AugJewels.Uncommon;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerCrystalEpi : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.CrystalEpi;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerCrystalBas>());
			Item.rare = ItemRarityID.Cyan;
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
				ModContent.ItemType<CollateralJewelEpi>(),
				ModContent.ItemType<CommittedJewelEpi>(),
				ModContent.ItemType<LifeleechJewelEpi>(),
				ModContent.ItemType<MomentumJewelEpi>(),
				ModContent.ItemType<RadianceJewelEpi>(),
				ModContent.ItemType<SuperchargeJewelEpi>(),
				ModContent.ItemType<VoidicJewelEpi>(),
				ModContent.ItemType<BlastJewelEpi>(),
				// Upgrades (Rare)
				ModContent.ItemType<RelentlessJewelEpi>(),
				ModContent.ItemType<BreakerJewelEpi>(),
				ModContent.ItemType<DeterminationJewelEpi>(),
				ModContent.ItemType<HypercritJewelEpi>(),
				ModContent.ItemType<KingslayerJewelEpi>(),
				ModContent.ItemType<OverdriveJewelEpi>(),
				ModContent.ItemType<RallyJewelEpi>(),
				ModContent.ItemType<ReapingJewelEpi>(),
				ModContent.ItemType<SuperluckJewelEpi>(),
				ModContent.ItemType<UltracutterJewelEpi>(),
				ModContent.ItemType<UnstableJewelEpi>(),
				ModContent.ItemType<VigorJewelEpi>(),
				// Updgrades (Uncommon)
				ModContent.ItemType<BarrageJewelEpi>(),
				ModContent.ItemType<DeathechoJewelEpi>(),
				ModContent.ItemType<DeathshroudJewelEpi>(),
				ModContent.ItemType<DiversionJewelEpi>(),
				ModContent.ItemType<LightweightJewelEpi>(),
				ModContent.ItemType<LuckJewelEpi>(),
				ModContent.ItemType<MegastrikeJewelEpi>(),
				ModContent.ItemType<MinicritJewelEpi>(),
				ModContent.ItemType<PaincycleJewelEpi>(),
				ModContent.ItemType<RendJewelEpi>(),
				ModContent.ItemType<RevitalizeJewelEpi>(),
				ModContent.ItemType<SiphonJewelEpi>(),
				ModContent.ItemType<SturdyJewelEpi>(),
				ModContent.ItemType<UpliftingJewelEpi>(),
				ModContent.ItemType<WardJewelEpi>(),
				ModContent.ItemType<WildstrikeJewelEpi>(),
				// Upgrades (Basic)
				ModContent.ItemType<ArcanaJewelEpi>(),
				ModContent.ItemType<ArmorbaneJewelEpi>(),
				ModContent.ItemType<BallisticJewelEpi>(),
				ModContent.ItemType<BattlelustJewelEpi>(),
				ModContent.ItemType<CombobreakJewelEpi>(),
				ModContent.ItemType<ConservationJewelEpi>(),
				ModContent.ItemType<DireJewelEpi>(),
				ModContent.ItemType<FinisherJewelEpi>(),
				ModContent.ItemType<ForceJewelEpi>(),
				ModContent.ItemType<FortuneJewelEpi>(),
				ModContent.ItemType<FrenzyJewelEpi>(),
				ModContent.ItemType<HeartsurgeJewelEpi>(),
				ModContent.ItemType<IgniteJewelEpi>(),
				ModContent.ItemType<MightJewelEpi>(),
				ModContent.ItemType<OverflowJewelEpi>(),
				ModContent.ItemType<PrecisionJewelEpi>(),
				ModContent.ItemType<RushJewelEpi>(),
				ModContent.ItemType<TaintJewelEpi>(),
				ModContent.ItemType<TitanreachJewelEpi>(),
				ModContent.ItemType<UnleashJewelEpi>()
			};

			itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, lootPool));
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient<PowerShardEpi>(4)
				.AddTile(TileID.AdamantiteForge)
				.Register();
		}
    }
}
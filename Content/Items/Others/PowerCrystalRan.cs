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
	public class PowerCrystalRan : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.CrystalEnigmatic;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerCrystalBas>());
			Item.rare = ItemRarityID.Pink;
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
			LeadingConditionRule isHardmode = new LeadingConditionRule(new Conditions.IsHardmode());

			int[] lootPoolBas = new int[] {
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

			int[] lootPoolUnc = new int[] {
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

			int[] lootPoolRar = new int[] {
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

			int[] lootPoolEpi = new int[] {
				// New
				ModContent.ItemType<CollateralJewelEpi>(),
				ModContent.ItemType<CommittedJewelEpi>(),
				ModContent.ItemType<LifeleechJewelEpi>(),
				ModContent.ItemType<MomentumJewelEpi>(),
				ModContent.ItemType<RadianceJewelEpi>(),
				ModContent.ItemType<BlastJewelEpi>(),
				ModContent.ItemType<SuperchargeJewelEpi>(),
				ModContent.ItemType<VoidicJewelEpi>(),
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

			int[] lootPoolUlt = new int[] {
				// New
				ModContent.ItemType<ExecutionJewelUlt>(),
				ModContent.ItemType<PowertheftJewelUlt>(),
				ModContent.ItemType<ResurgenceJewelUlt>(),
				ModContent.ItemType<RunicJewelUlt>(),
				// Upgrades (Epic)
				ModContent.ItemType<CollateralJewelUlt>(),
				ModContent.ItemType<CommittedJewelUlt>(),
				ModContent.ItemType<LifeleechJewelUlt>(),
				ModContent.ItemType<MomentumJewelUlt>(),
				ModContent.ItemType<RadianceJewelUlt>(),
				ModContent.ItemType<BlastJewelUlt>(),
				ModContent.ItemType<SuperchargeJewelUlt>(),
				ModContent.ItemType<VoidicJewelUlt>(),
				// Upgrades (Rare)
				ModContent.ItemType<RelentlessJewelUlt>(),
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

			int[] lootPoolAllPH = new int[] {
				// Basic
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
				ModContent.ItemType<UnleashJewelBas>(),
				// Uncommon
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
				ModContent.ItemType<UnleashJewelUnc>(),
				// Rare
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

			int[] lootPoolAllH = new int[] {
				// Basic
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
				ModContent.ItemType<UnleashJewelBas>(),
				// Uncommon
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
				ModContent.ItemType<UnleashJewelUnc>(),
				// Rare
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
				ModContent.ItemType<UnleashJewelRar>(),
				// Epic
				ModContent.ItemType<CollateralJewelEpi>(),
				ModContent.ItemType<CommittedJewelEpi>(),
				ModContent.ItemType<LifeleechJewelEpi>(),
				ModContent.ItemType<MomentumJewelEpi>(),
				ModContent.ItemType<RadianceJewelEpi>(),
				ModContent.ItemType<RelentlessJewelEpi>(),
				ModContent.ItemType<SuperchargeJewelEpi>(),
				ModContent.ItemType<VoidicJewelEpi>(),
				ModContent.ItemType<BlastJewelEpi>(),
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
				ModContent.ItemType<UnleashJewelEpi>(),
				// Ultimate
				ModContent.ItemType<ExecutionJewelUlt>(),
				ModContent.ItemType<PowertheftJewelUlt>(),
				ModContent.ItemType<ResurgenceJewelUlt>(),
				ModContent.ItemType<RunicJewelUlt>(),
				ModContent.ItemType<CollateralJewelUlt>(),
				ModContent.ItemType<CommittedJewelUlt>(),
				ModContent.ItemType<LifeleechJewelUlt>(),
				ModContent.ItemType<MomentumJewelUlt>(),
				ModContent.ItemType<RadianceJewelUlt>(),
				ModContent.ItemType<RelentlessJewelUlt>(),
				ModContent.ItemType<SuperchargeJewelUlt>(),
				ModContent.ItemType<VoidicJewelUlt>(),
				ModContent.ItemType<BlastJewelUlt>(),
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


			IItemDropRule[] lootPoolPreHardmode = new IItemDropRule[] {
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolBas),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolUnc),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolRar),
				ItemDropRule.OneFromOptions(1, lootPoolAllPH) // If the above rolls fail, default to a loot pool containing all possibilities at once
			};

			IItemDropRule[] lootPoolHardmode = new IItemDropRule[] {
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolBas),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolUnc),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolRar),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolEpi),
				ItemDropRule.OneFromOptionsNotScalingWithLuck(2, lootPoolUlt),
				ItemDropRule.OneFromOptions(1, lootPoolAllH) // If the above rolls fail, default to a loot pool containing all possibilities at once
			};

			isHardmode.OnSuccess(new SequentialRulesNotScalingWithLuckRule(1, lootPoolHardmode));
			isHardmode.OnSuccess(new SequentialRulesNotScalingWithLuckRule(4, lootPoolHardmode)); // 1/4 chance for extra drop
			isHardmode.OnFailedConditions(new SequentialRulesNotScalingWithLuckRule(1, lootPoolPreHardmode));
			isHardmode.OnFailedConditions(new SequentialRulesNotScalingWithLuckRule(4, lootPoolPreHardmode)); // 1/4 chance for extra drop
			itemLoot.Add(isHardmode);
		}

        public override void AddRecipes()
        {
			CreateRecipe(1)
				.AddIngredient<PowerShardBas>(12)
				.AddTile(TileID.Furnaces)
				.DisableDecraft()
				.Register();

			CreateRecipe(1)
				.AddIngredient<PowerShardUnc>(6)
				.AddTile(TileID.Furnaces)
				.DisableDecraft()
				.Register();

			CreateRecipe(1)
				.AddIngredient<PowerShardRar>(3)
				.AddTile(TileID.Hellforge)
				.DisableDecraft()
				.Register();

			CreateRecipe(1)
				.AddIngredient<PowerShardEpi>(2)
				.AddTile(TileID.AdamantiteForge)
				.DisableDecraft()
				.Register();

			CreateRecipe(1)
				.AddIngredient<PowerShardUlt>(1)
				.AddTile(TileID.AdamantiteForge)
				.DisableDecraft()
				.Register();
		}
    }
}
using Terraria.ModLoader;

namespace WeaponAugs.Common
{
    // This class defines the variables which determine the potency of each Augment on a per tier basis
    // Defining these numbers here for use elsewhere enables easier management and ensures consistency
    // Each Augment is labeled, and the function of the respective varaibles is listed
    public class AugPowerArchive : ModSystem
    {
        //// Base ////

        // Might
        // Defines: %dmg boost
        public const float MightBas = 3;
        public const float MightUnc = 7;
        public const float MightRar = 12;
        public const float MightEpi = 18;
        public const float MightUlt = 25;

        // Force
        // Defines: %kb boost
        public const float ForceBas = 5;
        public const float ForceUnc = 12;
        public const float ForceRar = 21;
        public const float ForceEpi = 32;
        public const float ForceUlt = 45;

        // Precision
        // Defines: %crit chance boost
        public const float PrecisionBas = 3;
        public const float PrecisionUnc = 7;
        public const float PrecisionRar = 12;
        public const float PrecisionEpi = 18;
        public const float PrecisionUlt = 25;

        // Rush
        // Defines: %atk spd boost
        public const float RushBas = 3;
        public const float RushUnc = 7;
        public const float RushRar = 12;
        public const float RushEpi = 18;
        public const float RushUlt = 25;

        // Armorbane
        // Defines: %defense ignored
        public const float ArmorbaneBas = 5;
        public const float ArmorbaneUnc = 12;
        public const float ArmorbaneRar = 21;
        public const float ArmorbaneEpi = 32;
        public const float ArmorbaneUlt = 45;

        // Titanreach
        // Defines: %size/reach boost
        public const float TitanreachBas = 7;
        public const float TitanreachUnc = 12;
        public const float TitanreachRar = 19;
        public const float TitanreachEpi = 28;
        public const float TitanreachUlt = 35;

        // Dire
        // Defines: %crit dmg boost
        public const float DireBas = 20;
        public const float DireUnc = 24;
        public const float DireRar = 32;
        public const float DireEpi = 44;
        public const float DireUlt = 60;

        // Heartsurge
        // Defines: effect duration
        public const float HeartsurgeBas = 2f;
        public const float HeartsurgeUnc = 3.25f;
        public const float HeartsurgeRar = 5f;
        public const float HeartsurgeEpi = 7.25f;
        public const float HeartsurgeUlt = 10f;

        // Arcana
        // Defines: %reduced mana
        public const float ArcanaBas = 3;
        public const float ArcanaUnc = 7;
        public const float ArcanaRar = 12;
        public const float ArcanaEpi = 18;
        public const float ArcanaUlt = 25;

        // Frenzy
        // Defines: %atk spd boost
        public const float FrenzyBas = 7;
        public const float FrenzyUnc = 12;
        public const float FrenzyRar = 19;
        public const float FrenzyEpi = 28;
        public const float FrenzyUlt = 35;

        // Battlelust
        // Defines: %dmg boost
        public const float BattlelustBas = 7;
        public const float BattlelustUnc = 12;
        public const float BattlelustRar = 19;
        public const float BattlelustEpi = 28;
        public const float BattlelustUlt = 35;

        // Conservation
        // Defines: %ammo conservation
        public const float ConservationBas = 8;
        public const float ConservationUnc = 12;
        public const float ConservationRar = 17;
        public const float ConservationEpi = 23;
        public const float ConservationUlt = 30;

        // Overflow
        // Defines: dmg of proj (% of excess dmg)
        public const float OverflowBas = 70;
        public const float OverflowUnc = 100;
        public const float OverflowRar = 140;
        public const float OverflowEpi = 190;
        public const float OverflowUlt = 250;

        // Fortune
        // Defines: %debuff chance
        public const float FortuneBas = 5;
        public const float FortuneUnc = 12;
        public const float FortuneRar = 21;
        public const float FortuneEpi = 32;
        public const float FortuneUlt = 45;

        // Ignite
        // Defines: debuff type
        public const string IgniteBas = "On Fire!";
        public const string IgniteUnc = "Frostburn";
        public const string IgniteRar = "Shadowflame";
        public const string IgniteEpi = "Cursed Inferno";
        public const string IgniteUlt = "Runic Flame";

        // Taint
        // Defines: debuff duration (sec)
        public const float TaintBas = 10;
        public const float TaintUnc = 17;
        public const float TaintRar = 26;
        public const float TaintEpi = 37;
        public const float TaintUlt = 50;

        // Ballistic
        // Defines: %velocity boost
        public const float BallisticBas = 3;
        public const float BallisticUnc = 7;
        public const float BallisticRar = 12;
        public const float BallisticEpi = 18;
        public const float BallisticUlt = 25;

        // Finisher
        // Defines: max %dmg boost
        public const float FinisherBas = 40;
        public const float FinisherUnc = 65;
        public const float FinisherRar = 100;
        public const float FinisherEpi = 145;
        public const float FinisherUlt = 200;

        // Unleash
        // Defines: max %dmg boost
        public const float UnleashBas = 40;
        public const float UnleashUnc = 65;
        public const float UnleashRar = 100;
        public const float UnleashEpi = 145;
        public const float UnleashUlt = 200;

        // Combobreak
        // Defines: %dmg boost
        public const float CombobreakBas = 10;
        public const float CombobreakUnc = 30;
        public const float CombobreakRar = 60;
        public const float CombobreakEpi = 100;
        public const float CombobreakUlt = 150;

        //// Uncommon ////

        // Deathecho
        // Defines: dmg of effect (% of base)
        public const float DeathechoUnc = 80;
        public const float DeathechoRar = 110;
        public const float DeathechoEpi = 150;
        public const float DeathechoUlt = 200;

        // Lightweight
        // Defines: speed boost %
        public const float LightweightUnc = 10;
        public const float LightweightRar = 14;
        public const float LightweightEpi = 19;
        public const float LightweightUlt = 25;

        // Revitalize
        // Defines: life regen per sec
        public const float RevitalizeUnc = 0.5f;
        public const float RevitalizeRar = 1f;
        public const float RevitalizeEpi = 1.5f;
        public const float RevitalizeUlt = 2f;

        // Megastrike
        // Defines: %dmg boost
        public const float MegastrikeUnc = 35;
        public const float MegastrikeRar = 42;
        public const float MegastrikeEpi = 52;
        public const float MegastrikeUlt = 65;

        // Sturdy
        // Defines: defense boost
        public const float SturdyUnc = 2;
        public const float SturdyRar = 4;
        public const float SturdyEpi = 7;
        public const float SturdyUlt = 11;

        // Barrage
        // Defines: dmg of proj (% of base)
        public const float BarrageUnc = 30;
        public const float BarrageRar = 45;
        public const float BarrageEpi = 70;
        public const float BarrageUlt = 105;

        // Diversion
        // Defines: debuff duration
        public const float DiversionUnc = 1.5f;
        public const float DiversionRar = 2.25f;
        public const float DiversionEpi = 3.25f;
        public const float DiversionUlt = 4.5f;

        // Wildstrike
        // Defines: %atk spd boost
        public const float WildstrikeUnc = 35;
        public const float WildstrikeRar = 42;
        public const float WildstrikeEpi = 52;
        public const float WildstrikeUlt = 65;

        // Uplifting
        // Defines: flight time/jump speed boost (%)
        public const float UpliftingUnc = 10;
        public const float UpliftingRar = 14;
        public const float UpliftingEpi = 19;
        public const float UpliftingUlt = 25;

        // Luck
        // Defines: luck boost
        public const float LuckUnc = 0.1f;
        public const float LuckRar = 0.16f;
        public const float LuckEpi = 0.26f;
        public const float LuckUlt = 0.4f;

        // Rend
        // Defines: defense reduction
        public const float RendUnc = 10;
        public const float RendRar = 13;
        public const float RendEpi = 18;
        public const float RendUlt = 25;

        // Deathshroud
        // Defines: effect duration (sec)
        public const float DeathshroudUnc = 2f;
        public const float DeathshroudRar = 3.5f;
        public const float DeathshroudEpi = 5.5f;
        public const float DeathshroudUlt = 8f;

        // Ward
        // Defines: %DR
        public const float WardUnc = 5;
        public const float WardRar = 9;
        public const float WardEpi = 14;
        public const float WardUlt = 20;

        // Minicrit
        // Defines: minicrit %dmg boost
        public const float MinicritUnc = 20;
        public const float MinicritRar = 35;
        public const float MinicritEpi = 55;
        public const float MinicritUlt = 80;

        // Siphon
        // Defines: mana drained
        public const float SiphonUnc = 5;
        public const float SiphonRar = 9;
        public const float SiphonEpi = 14;
        public const float SiphonUlt = 20;

        // Paincycle
        // Defines: %dmg boost
        public const float PaincycleUnc = 40;
        public const float PaincycleRar = 55;
        public const float PaincycleEpi = 75;
        public const float PaincycleUlt = 100;

        //// Rare ////

        // Rally
        // Defines: tag dmg
        public const float RallyRar = 4;
        public const float RallyEpi = 7;
        public const float RallyUlt = 12;

        // Superluck
        // Defines: luck boost
        public const float SuperluckRar = 0.12f;
        public const float SuperluckEpi = 0.18f;
        public const float SuperluckUlt = 0.25f;

        // Kingslayer
        // Defines: %dmg boost
        public const float KingslayerRar = 15;
        public const float KingslayerEpi = 23;
        public const float KingslayerUlt = 35;

        // Hypercrit
        // Defines: %crit dmg boost
        public const float HypercritRar = 400;
        public const float HypercritEpi = 540;
        public const float HypercritUlt = 700;

        // Unstable
        // Defines: %crit chance boost
        public const float UnstableRar = 15;
        public const float UnstableEpi = 21;
        public const float UnstableUlt = 30;

        // Vigor
        // Defines: max %dmg boost
        public const float VigorRar = 15;
        public const float VigorEpi = 21;
        public const float VigorUlt = 30;

        // Determination
        // Defines: max %dmg boost
        public const float DeterminationRar = 15;
        public const float DeterminationEpi = 23;
        public const float DeterminationUlt = 35;

        // Breaker
        // Defines: max %dmg boost
        public const float BreakerRar = 60;
        public const float BreakerEpi = 75;
        public const float BreakerUlt = 100;

        // Relentless
        // Defines: max %dmg boost
        public const float RelentlessRar = 60;
        public const float RelentlessEpi = 75;
        public const float RelentlessUlt = 100;

        // Reaping
        // Defines: drop chance denom. 
        public const float ReapingRar = 8;
        public const float ReapingEpi = 6;
        public const float ReapingUlt = 3;

        // Overdrive
        // Defines: %dmg boost
        public const float OverdriveRar = 25;
        public const float OverdriveEpi = 33;
        public const float OverdriveUlt = 45;

        // Ultracutter
        // Defines: %dmg boost
        public const float UltracutterRar = 20;
        public const float UltracutterEpi = 30;
        public const float UltracutterUlt = 45;

        //// Epic ////

        // Blast
        // Defines: blast damage (% of base)
        public const float BlastEpi = 100;
        public const float BlastUlt = 150;

        // Voidic
        // Defines: %defense ignored
        public const float VoidicEpi = 65;
        public const float VoidicUlt = 95;

        // Committed
        // Defines: max %dmg boost
        public const float CommittedEpi = 25;
        public const float CommittedUlt = 40;

        // Lifeleech
        // Defines: %lifesteal
        public const float LifeleechEpi = 10;
        public const float LifeleechUlt = 15;

        // Radiance
        // Defines: %lifesteal (per rune) (x3)
        public const float RadianceEpi = 3f;
        public const float RadianceUlt = 5f;

        // Momentum
        // Defines: max %dmg boost
        public const float MomentumEpi = 20;
        public const float MomentumUlt = 35;

        // Collateral
        // Defines: dmg of proj (% of hit)
        public const float CollateralEpi = 115;
        public const float CollateralUlt = 130;

        // Supercharge
        // Defines: %dmg boost
        public const float SuperchargeEpi = 200;
        public const float SuperchargeUlt = 300;

        //// Ultimate ////

        // Execution
        // Defines: Proc. chance %
        public const float ExecutionUlt = 5;

        // Resurgence
        // Defines: Lifesteal potency (% of dmg)
        public const float ResurgenceUlt = 2.5f;

        // Runic
        // Defines: Buff duration (sec)
        public const float RunicUlt = 10;

        // Powertheft
        // Defines: Effect duration (sec)
        public const float PowertheftUlt = 5;
    }

    // This class defines values of ResearchUnlockCount variables used by items
    // Mnay items of similar type (Ex: all Basic Augment Jewels) use the same value
    // Defining these numbers here for use elsewhere enables easier management and ensures consistency
    public class JourneyUnlocksArchive : ModSystem
    {
        // Shard of Power (and Void)
        public const int ShardBas = 100;
        public const int ShardUnc = 80;
        public const int ShardRar = 60;
        public const int ShardEpi = 40;
        public const int ShardUlt = 20;
        public const int ShardVoid = 5;

        // Crystals of Power (and Enigmatic)
        public const int CrystalBas = 25;
        public const int CrystalUnc = 20;
        public const int CrystalRar = 15;
        public const int CrystalEpi = 10;
        public const int CrystalUlt = 5;
        public const int CrystalEnigmatic = 15;

        // Augment Jewels
        public const int JewelBas = 10;
        public const int JewelUnc = 8;
        public const int JewelRar = 6;
        public const int JewelEpi = 4;
        public const int JewelUlt = 2;
    }
}
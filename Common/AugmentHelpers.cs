using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Content.Buffs;
using WeaponAugs.Content.Buffs.Debuffs;
using WeaponAugs.Content.Items.AugJewels.Ultimate;
using WeaponAugs.Content.Projectiles;

namespace WeaponAugs.Common
{
    // This class handles the effects of augments on the ModifyHitNPC and OnHitNPC hooks for both projectiles and true melee
    public class AugmentHelpers : ModSystem
    {
        /// <summary>
        /// Extension of ModifyHitNPC that applies the effects of Augments
        /// </summary>
        /// <param name="player">The player inflicitng the hit.</param>
        /// <param name="target">The NPC damaged.</param>
        /// <param name="modifiers">HitModifiers passed in by the ModifyHitNPC hook.</param>
        /// <param name="baseDmg">Approximate damage of the attack after player stat boosts, but before hit modifiers, defense, etc. Influences proc rate and/or potency of certain augments.</param>
        /// <param name="atkSpd">Approximate total use time of the item that inflicted the attack. Influences proc rate and/or potency of certain augments.</param>
        /// <param name="augments">List of augments types on the item that inflicted the attack.</param>
        /// <param name="augTiers">List of tiers of the augments on the item that inflicted the attack.</param>
        /// <param name="trueMelee">Set to true for hits inflicted by item hitboxes as opposed to projectiles. Procs the Ultracutter augment.</param>
        public static void CombinedAugModifyHit(Player player, NPC target, ref NPC.HitModifiers modifiers, 
            int baseDmg, int atkSpd, List<AugType> augments, List <AugTier> augTiers, bool trueMelee = false)
        {
            GlobalAugPlayer augPlayer = player.GetModPlayer<GlobalAugPlayer>();
            GlobalAugNPC augNPC = target.GetGlobalNPC<GlobalAugNPC>();

            bool triggerCollateral = false;
            float breakerPow = 0;
            float relentlessPow = 0;
            float committedPow = 0;
            float comboPow = 0;
            float unleashPow = 0;
            float paincyclePow = 0;
            float finisherPow = 0;
            float executionChance = 0;
            for (int i = 0; i < augments.Count; i++)
            {
                switch (augments[i])
                {
                    case AugType.Armorbane:
                        switch (augTiers[i])
                        {
                            case AugTier.Basic:
                                modifiers.ScalingArmorPenetration += AugPowerArchive.ArmorbaneBas * 0.01f;
                                break;
                            case AugTier.Uncommon:
                                modifiers.ScalingArmorPenetration += AugPowerArchive.ArmorbaneUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                modifiers.ScalingArmorPenetration += AugPowerArchive.ArmorbaneRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                modifiers.ScalingArmorPenetration += AugPowerArchive.ArmorbaneEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                modifiers.ScalingArmorPenetration += AugPowerArchive.ArmorbaneUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Collateral:
                        triggerCollateral = true;
                        break;
                    case AugType.Voidic:
                        switch (augTiers[i])
                        {
                            case AugTier.Epic:
                                modifiers.ScalingArmorPenetration += AugPowerArchive.VoidicEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                modifiers.ScalingArmorPenetration += AugPowerArchive.VoidicUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        modifiers.DamageVariationScale *= 0f;
                        break;
                    case AugType.Unstable:
                        modifiers.DamageVariationScale *= 10f/3f;
                        break;
                    case AugType.Dire:
                        switch (augTiers[i])
                        {
                            case AugTier.Basic:
                                modifiers.CritDamage += AugPowerArchive.DireBas * 0.01f;
                                break;
                            case AugTier.Uncommon:
                                modifiers.CritDamage += AugPowerArchive.DireUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                modifiers.CritDamage += AugPowerArchive.DireRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                modifiers.CritDamage += AugPowerArchive.DireEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                modifiers.CritDamage += AugPowerArchive.DireUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Minicrit:
                        if (Main.rand.NextBool(3))
                        {
                            switch (augTiers[i])
                            {
                                case AugTier.Uncommon:
                                    modifiers.NonCritDamage += AugPowerArchive.MinicritUnc * 0.01f;
                                    break;
                                case AugTier.Rare:
                                    modifiers.NonCritDamage += AugPowerArchive.MinicritRar * 0.01f;
                                    break;
                                case AugTier.Epic:
                                    modifiers.NonCritDamage += AugPowerArchive.MinicritEpi * 0.01f;
                                    break;
                                case AugTier.Ultimate:
                                    modifiers.NonCritDamage += AugPowerArchive.MinicritUlt * 0.01f;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case AugType.Hypercrit:
                        if (Main.rand.NextBool(10))
                        {
                            switch (augTiers[i])
                            {
                                case AugTier.Rare:
                                    modifiers.CritDamage += AugPowerArchive.HypercritRar * 0.01f;
                                    break;
                                case AugTier.Epic:
                                    modifiers.CritDamage += AugPowerArchive.HypercritEpi * 0.01f;
                                    break;
                                case AugTier.Ultimate:
                                    modifiers.CritDamage += AugPowerArchive.HypercritUlt * 0.01f;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case AugType.Ultracutter:
                        if (trueMelee)
                        {
                            switch (augTiers[i])
                            {
                                case AugTier.Rare:
                                    modifiers.SourceDamage += AugPowerArchive.UltracutterRar * 0.01f;
                                    break;
                                case AugTier.Epic:
                                    modifiers.SourceDamage += AugPowerArchive.UltracutterEpi * 0.01f;
                                    break;
                                case AugTier.Ultimate:
                                    modifiers.SourceDamage += AugPowerArchive.UltracutterUlt * 0.01f;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case AugType.Kingslayer:
                        if (CountsAsBoss(target))
                        {
                            switch (augTiers[i])
                            {
                                case AugTier.Rare:
                                    modifiers.SourceDamage += AugPowerArchive.KingslayerRar * 0.01f;
                                    break;
                                case AugTier.Epic:
                                    modifiers.SourceDamage += AugPowerArchive.KingslayerEpi * 0.01f;
                                    break;
                                case AugTier.Ultimate:
                                    modifiers.SourceDamage += AugPowerArchive.KingslayerUlt * 0.01f;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case AugType.Breaker:
                        switch (augTiers[i])
                        {
                            case AugTier.Rare:
                                breakerPow += AugPowerArchive.BreakerRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                breakerPow += AugPowerArchive.BreakerEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                breakerPow += AugPowerArchive.BreakerUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Relentless:
                        switch (augTiers[i])
                        {
                            case AugTier.Rare:
                                relentlessPow += AugPowerArchive.RelentlessRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                relentlessPow += AugPowerArchive.RelentlessEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                relentlessPow += AugPowerArchive.RelentlessUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Committed:
                        switch (augTiers[i])
                        {
                            case AugTier.Epic:
                                committedPow += AugPowerArchive.CommittedEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                committedPow += AugPowerArchive.CommittedUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Combobreak:
                        switch (augTiers[i])
                        {
                            case AugTier.Basic:
                                comboPow += AugPowerArchive.CombobreakBas * 0.01f;
                                break;
                            case AugTier.Uncommon:
                                comboPow += AugPowerArchive.CombobreakUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                comboPow += AugPowerArchive.CombobreakRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                comboPow += AugPowerArchive.CombobreakEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                comboPow += AugPowerArchive.CombobreakUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Unleash:
                        switch (augTiers[i])
                        {
                            case AugTier.Basic:
                                unleashPow += AugPowerArchive.UnleashBas * 0.01f;
                                break;
                            case AugTier.Uncommon:
                                unleashPow += AugPowerArchive.UnleashUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                unleashPow += AugPowerArchive.UnleashRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                unleashPow += AugPowerArchive.UnleashEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                unleashPow += AugPowerArchive.UnleashUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Paincycle:
                        switch (augTiers[i])
                        {
                            case AugTier.Uncommon:
                                paincyclePow += AugPowerArchive.PaincycleUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                paincyclePow += AugPowerArchive.PaincycleRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                paincyclePow += AugPowerArchive.PaincycleEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                paincyclePow += AugPowerArchive.PaincycleUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Finisher:
                        switch (augTiers[i])
                        {
                            case AugTier.Basic:
                                finisherPow += AugPowerArchive.FinisherBas * 0.01f;
                                break;
                            case AugTier.Uncommon:
                                finisherPow += AugPowerArchive.FinisherUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                finisherPow += AugPowerArchive.FinisherRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                finisherPow += AugPowerArchive.FinisherEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                finisherPow += AugPowerArchive.FinisherUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Execution:
                        executionChance += AugPowerArchive.ExecutionUlt * 0.01f;
                        break;
                    default:
                        break;
                }
            }
            if (breakerPow > 0) // Breaker dmg application
            {
                float hpScalar = (float)target.life / (float)target.lifeMax;
                if (hpScalar > 0f)
                {
                    if (hpScalar >= 0.9f)
                    {
                        modifiers.SourceDamage += breakerPow;
                    }
                    else
                    {
                        float remapSclar = Utils.Remap(hpScalar, 0f, 0.9f, 0f, 1f);
                        modifiers.SourceDamage += breakerPow * remapSclar;
                    }
                }
            }
            if (relentlessPow > 0) // Relentless dmg application
            {
                float hpScalar = (float)target.life / (float)target.lifeMax;
                if (hpScalar <= 0.1f)
                {
                    modifiers.SourceDamage += relentlessPow;
                }
                else
                {
                    float remapSclar = Utils.Remap(hpScalar, 0.1f, 1f, 0f, 1f);
                    modifiers.SourceDamage += relentlessPow * (1f - remapSclar);
                }
            }
            if(committedPow > 0) // Committed dmg application
            {
                float maxCommit = committedPow * 1300;
                float commitScalar = (float)augNPC.committedStacks / maxCommit;
                commitScalar = Math.Clamp(commitScalar, 0f, 1f);
                if(commitScalar > 0)
                {
                    modifiers.SourceDamage += committedPow * commitScalar;
                }
            }
            if(unleashPow > 0 && augPlayer.unleashStacks > 0 && atkSpd > 0) // Unleash damage application
            {
                float maxStacks = atkSpd * (7f/3f);
                float unleashScalar = (float)augPlayer.unleashStacks / maxStacks;
                unleashScalar = Math.Clamp(unleashScalar, 0f, 1f);
                if (unleashScalar > 0)
                {
                    modifiers.SourceDamage += unleashPow * unleashScalar;
                }
                augPlayer.unleashStacks = 0;
            }
            if (comboPow > 0 && augNPC.combobreakStacks >= 4) // Combobreak damage application
            {
                augNPC.combobreakStacks = -1;
                modifiers.SourceDamage += comboPow;
            }
            if (triggerCollateral)
            {
                modifiers.SourceDamage *= 0.5f;
                modifiers.ScalingArmorPenetration += 0.5f;
            }
            if (paincyclePow > 0) // Paincycle damage application
            {
                // Has advanced logic to limit paincycle proc. based on attack count and attack speed:
                // Can proc up to once every 30 ticks, but also no more than once every 3 attacks
                // Life drain is based on the proc rate to average out to -2 hp/sec, but also limited to not drain more than 8 HP
                augPlayer.paincycleCounter++;
                if(augPlayer.paincycleCounter >= 3 && augPlayer.paincycleTimer <= 0 && atkSpd > 0) 
                {
                    modifiers.SourceDamage += paincyclePow;

                    augPlayer.paincycleCounter = 0; 
                    augPlayer.paincycleTimer = 30;

                    int painRate = atkSpd * 3;
                    painRate = Math.Clamp(painRate, 30, 240);
                    int paincycleDmg = painRate / 30;
                    player.Hurt(PlayerDeathReason.ByCustomReason(PaincycleDeathMessage(player)), paincycleDmg, 0, dodgeable: false, scalingArmorPenetration: 1f, knockback: 0);
                }
            }
            // This should retrieve base damage AFTER calculations and modifications, allowing the below effects to scale accordingly
            int trueBaseDmg = modifiers.GetDamage(baseDmg, false); 
            if (finisherPow > 0) // Finisher damage application
            {
                if (trueBaseDmg * (1f + finisherPow) >= target.life)
                {
                    modifiers.SourceDamage += finisherPow;
                }
            }
            if (executionChance > 0 && augPlayer.executionTimer <= 0 && !target.immortal) // Execution proc.
            {
                if(Main.rand.NextFloat() <= executionChance)
                {
                    float immunityThreshold = 100f * baseDmg;
                    if (CountsAsBoss(target))
                    {
                        if(target.life / target.lifeMax <= 0.05f)
                        {
                            modifiers.SetInstantKill();
                            CombatText.NewText(target.Hitbox, Colors.RarityDarkPurple, "Execution!", true);
                        }
                        else if(target.life / target.lifeMax <= 0.1f)
                        {
                            modifiers.FlatBonusDamage += target.lifeMax * 0.05f;
                            modifiers.SetMaxDamage((int)(target.lifeMax * 0.05f));
                        } 
                    }
                    else if (target.lifeMax > immunityThreshold && target.life / target.lifeMax > 0.1f)
                    {
                        modifiers.FlatBonusDamage += target.lifeMax * 0.1f;
                        modifiers.SetMaxDamage((int)(target.lifeMax * 0.1f));
                    }
                    else
                    {
                        modifiers.SetInstantKill();
                        CombatText.NewText(target.Hitbox, Colors.RarityDarkPurple, "Execution!", true);
                    }
                }
                augPlayer.executionTimer = 60;
            }
        }

        /// <summary>
        /// Extension of OnHitNPC that applies the effects of Augments
        /// </summary>
        /// <param name="sourceShoot">The IEntitySource the game should use when spawning on-hit projectiles from augment effects.</param>
        /// <param name="player">The player inflicitng the hit.</param>
        /// <param name="target">The NPC damaged.</param>
        /// <param name="hit">HitInfo passed in by the OnHitNPC hook.</param>
        /// <param name="damageDone">Approximate final damage of the attack. Influences proc rate and/or potency of certain augments.</param>
        /// <param name="atkSpd">Approximate total use time of the item that inflicted the attack. Influences proc rate and/or potency of certain augments.</param>
        /// <param name="augments">List of augments types on the item that inflicted the attack.</param>
        /// <param name="augTiers">List of tiers of the augments on the item that inflicted the attack.</param>
        /// <param name="minion">Set to true for hits inflicted by minions and sentries. Influences proc rate of certain augments.</param>
        public static void CombinedAugOnHit(IEntitySource sourceShoot, Player player, NPC target, 
            NPC.HitInfo hit, int damageDone, int atkSpd, List<AugType> augments, List<AugTier> augTiers, bool minion = false)
        {
            GlobalAugPlayer augPlayer = player.GetModPlayer<GlobalAugPlayer>();
            GlobalAugNPC augNPC = target.GetGlobalNPC<GlobalAugNPC>();

            float buffChance = 0.8f;
            float debuffChance = atkSpd / 150f; // Also used by Runic
            if (minion || atkSpd <= 0)
            {
                buffChance = 0.45f;
                debuffChance = 0.1f;
            }

            float resurgencePow = 0f;
            bool tryFrenzy = false;
            bool tryBattlelust = false;
            bool tryWard = false;
            bool triggerRunic = false;
            bool addCharge = false;
            bool addMomentum = false;
            bool commitEnemy = false;
            bool comboEnemy = false;
            bool siphonTriggered = false;

            // Target Debuffs
            for (int i = 0; i < augments.Count; i++)
            {
                switch (augments[i])
                {
                    case AugType.Ignite:
                        if (Main.rand.NextFloat() <= debuffChance)
                        {
                            int duration = Main.rand.Next(60, 600);
                            switch (augTiers[i])
                            {
                                case AugTier.Basic:
                                    target.AddBuff(BuffID.OnFire, duration);
                                    break;
                                case AugTier.Uncommon:
                                    target.AddBuff(BuffID.Frostburn, duration);
                                    break;
                                case AugTier.Rare:
                                    target.AddBuff(BuffID.ShadowFlame, duration);
                                    break;
                                case AugTier.Epic:
                                    target.AddBuff(BuffID.CursedInferno, duration);
                                    break;
                                case AugTier.Ultimate:
                                    target.AddBuff(ModContent.BuffType<RunicBlaze>(), duration);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case AugType.Taint:
                        if (Main.rand.NextFloat() <= debuffChance && Main.rand.NextBool(3))
                        {
                            int duration = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Basic:
                                    duration = (int)AugPowerArchive.TaintBas * 60;
                                    break;
                                case AugTier.Uncommon:
                                    duration = (int)AugPowerArchive.TaintUnc * 60;
                                    break;
                                case AugTier.Rare:
                                    duration = (int)AugPowerArchive.TaintRar * 60;
                                    break;
                                case AugTier.Epic:
                                    duration = (int)AugPowerArchive.TaintEpi * 60;
                                    break;
                                case AugTier.Ultimate:
                                    duration = (int)AugPowerArchive.TaintUlt * 60;
                                    break;
                                default:
                                    break;
                            }
                            if(duration > 0)
                            {
                                target.AddBuff(ModContent.BuffType<RunicTaint>(), duration);
                            }
                        }
                        break;
                    case AugType.Fortune:
                        float midiasChance = 0f;
                        switch (augTiers[i])
                        {
                            case AugTier.Basic:
                                midiasChance = AugPowerArchive.FortuneBas * 0.01f;
                                break;
                            case AugTier.Uncommon:
                                midiasChance = AugPowerArchive.FortuneUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                midiasChance = AugPowerArchive.FortuneRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                midiasChance = AugPowerArchive.FortuneEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                midiasChance = AugPowerArchive.FortuneUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        if(Main.rand.NextFloat() < midiasChance)
                        {
                            target.AddBuff(BuffID.Midas, Main.rand.Next(30, 300));
                        }
                        break;
                    case AugType.Diversion:
                        if (Main.rand.NextFloat() <= debuffChance)
                        {
                            int duration = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Uncommon:
                                    duration = (int)AugPowerArchive.DiversionUnc * 60;
                                    break;
                                case AugTier.Rare:
                                    duration = (int)AugPowerArchive.DiversionRar * 60;
                                    break;
                                case AugTier.Epic:
                                    duration = (int)AugPowerArchive.DiversionEpi * 60;
                                    break;
                                case AugTier.Ultimate:
                                    duration = (int)AugPowerArchive.DiversionUlt * 60;
                                    break;
                                default:
                                    break;
                            }
                            if (duration > 0)
                            {
                                target.AddBuff(BuffID.Confused, duration);
                            }
                        }
                        break;
                    case AugType.Rend:
                        if (Main.rand.NextFloat() <= debuffChance)
                        {
                            int duration = Main.rand.Next(60, 300);
                            switch (augTiers[i])
                            {
                                case AugTier.Uncommon:
                                    target.AddBuff(ModContent.BuffType<RendUnc>(), duration);
                                    break;
                                case AugTier.Rare:
                                    target.AddBuff(ModContent.BuffType<RendRar>(), duration);
                                    break;
                                case AugTier.Epic:
                                    target.AddBuff(ModContent.BuffType<RendEpi>(), duration);
                                    break;
                                case AugTier.Ultimate:
                                    target.AddBuff(ModContent.BuffType<RendUlt>(), duration);
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case AugType.Powertheft:
                        if (Main.rand.NextFloat() <= debuffChance)
                        {
                            int duration = (int)AugPowerArchive.PowertheftUlt * 60;
                            target.AddBuff(ModContent.BuffType<Powertheft>(), duration);
                            player.AddBuff(ModContent.BuffType<PowertheftBuff>(), duration);
                        }
                        break;
                    case AugType.Rally:
                        switch (augTiers[i])
                        {
                            case AugTier.Rare:
                                target.AddBuff(ModContent.BuffType<RallyRar>(), Main.rand.Next(50, 200));
                                player.MinionAttackTargetNPC = target.whoAmI;
                                break;
                            case AugTier.Epic:
                                target.AddBuff(ModContent.BuffType<RallyEpi>(), Main.rand.Next(50, 200));
                                player.MinionAttackTargetNPC = target.whoAmI;
                                break;
                            case AugTier.Ultimate:
                                target.AddBuff(ModContent.BuffType<RallyUlt>(), Main.rand.Next(50, 200));
                                player.MinionAttackTargetNPC = target.whoAmI;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Frenzy:
                        tryFrenzy = true;
                        break;
                    case AugType.Battlelust:
                        tryBattlelust = true;
                        break;
                    case AugType.Ward:
                        tryWard = true;
                        break;
                    case AugType.Heartsurge:
                        if (Main.rand.NextFloat() <= buffChance && !player.HasBuff(ModContent.BuffType<Heartsurge>())
                            && target.life <= 0)
                        {
                            int duration = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Basic:
                                    duration = (int)AugPowerArchive.HeartsurgeBas * 60;
                                    break;
                                case AugTier.Uncommon:
                                    duration = (int)AugPowerArchive.HeartsurgeUnc * 60;
                                    break;
                                case AugTier.Rare:
                                    duration = (int)AugPowerArchive.HeartsurgeRar * 60;
                                    break;
                                case AugTier.Epic:
                                    duration = (int)AugPowerArchive.HeartsurgeEpi * 60;
                                    break;
                                case AugTier.Ultimate:
                                    duration = (int)AugPowerArchive.HeartsurgeUlt * 60;
                                    break;
                                default:
                                    break;
                            }
                            if (duration > 0)
                            {
                                player.AddBuff(ModContent.BuffType<Heartsurge>(), duration);
                            }
                        }
                        break;
                    case AugType.Deathshroud:
                        if (Main.rand.NextFloat() <= buffChance && augPlayer.deathshroudTimer <= 0 && !player.HasBuff(ModContent.BuffType<Deathshroud>())
                            && target.life <= 0)
                        {
                            int duration = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Uncommon:
                                    duration = (int)AugPowerArchive.DeathshroudUnc * 60;
                                    break;
                                case AugTier.Rare:
                                    duration = (int)AugPowerArchive.DeathshroudRar * 60;
                                    break;
                                case AugTier.Epic:
                                    duration = (int)AugPowerArchive.DeathshroudEpi * 60;
                                    break;
                                case AugTier.Ultimate:
                                    duration = (int)AugPowerArchive.DeathshroudUlt * 60;
                                    break;
                                default:
                                    break;
                            }
                            if (duration > 0)
                            {
                                player.AddBuff(ModContent.BuffType<Deathshroud>(), duration);
                            }
                        }
                        break;
                    case AugType.Momentum:
                        addMomentum = true;
                        break;
                    case AugType.Supercharge:
                        addCharge = true;
                        break;
                    case AugType.Committed:
                        commitEnemy = true;
                        break;
                    case AugType.Combobreak:
                        comboEnemy = true;
                        break;
                    case AugType.Reaping:
                        if (target.life <= 0)
                        {
                            int denom = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Rare:
                                    denom = (int)AugPowerArchive.ReapingRar;
                                    break;
                                case AugTier.Epic:
                                    denom = (int)AugPowerArchive.ReapingEpi;
                                    break;
                                case AugTier.Ultimate:
                                    denom = (int)AugPowerArchive.ReapingUlt;
                                    break;
                                default:
                                    break;
                            }
                            if (denom > 0)
                            {
                                if (Main.rand.NextBool(denom) && player == Main.LocalPlayer)
                                {
                                    int itemType = Main.rand.NextBool() ? ItemID.Heart : ItemID.Star;
                                    Item.NewItem(new EntitySource_OnHit(player, target, "reapingAugment"), target.Hitbox, itemType, noGrabDelay: true);
                                }
                            }
                        }
                        break;
                    case AugType.Runic:
                        if (Main.rand.NextFloat() <= debuffChance && augPlayer.runicTimer <= 0) // Has a beneficial effect, though it uses the 'debuffChance' variable
                        {
                            triggerRunic = true;
                        }
                        break;
                    case AugType.Siphon:
                        if(Main.rand.NextBool() && augPlayer.siphonTimer <= 0)
                        {
                            siphonTriggered = true;
                            int mana;
                            switch (augTiers[i])
                            {
                                case AugTier.Uncommon:
                                    mana = (int)AugPowerArchive.SiphonUnc;
                                    break;
                                case AugTier.Rare:
                                    mana = (int)AugPowerArchive.SiphonRar;
                                    break;
                                case AugTier.Epic:
                                    mana = (int)AugPowerArchive.SiphonEpi;
                                    break;
                                case AugTier.Ultimate:
                                    mana = (int)AugPowerArchive.SiphonUlt;
                                    break;
                                default:
                                    mana = 0;
                                    break;
                            }
                            if(mana > 0 && player == Main.LocalPlayer)
                            {
                                SpawnResourceProj(target, player, sourceShoot, mana, 0);
                            }
                        }
                        break;
                    case AugType.Lifeleech:
                        if (Main.rand.NextBool(10))
                        {
                            int heal = damageDone;
                            switch (augTiers[i])
                            {
                                case AugTier.Epic:
                                    heal = (int)(heal * AugPowerArchive.LifeleechEpi * 0.01f);
                                    break;
                                case AugTier.Ultimate:
                                    heal = (int)(heal * AugPowerArchive.LifeleechUlt * 0.01f);
                                    break;
                                default:
                                    heal = 0;
                                    break;
                            }
                            if (heal > 0 && player == Main.LocalPlayer)
                            {
                                SpawnResourceProj(target, player, sourceShoot, heal, 1);
                            }
                        }
                        break;
                    case AugType.Resurgence:
                        if (player.HasBuff(ModContent.BuffType<Resurgence>()))
                        {
                            resurgencePow += AugPowerArchive.ResurgenceUlt * 0.01f;
                        }
                        break;
                    case AugType.Radiance:
                        if (target.life <= 0)
                        {
                            int heal = damageDone;
                            switch (augTiers[i])
                            {
                                case AugTier.Epic:
                                    heal = (int)(heal * AugPowerArchive.RadianceEpi * 0.01f);
                                    break;
                                case AugTier.Ultimate:
                                    heal = (int)(heal * AugPowerArchive.RadianceUlt * 0.01f);
                                    break;
                                default:
                                    heal = 0;
                                    break;
                            }
                            if (heal > 0 && player == Main.LocalPlayer)
                            {
                                for(int j = 0; j < 3; j++)
                                {
                                    SpawnResourceProj(target, player, sourceShoot, heal, 2);
                                }
                            }
                        }
                        break;
                    case AugType.Overflow:
                        if(target.life<= 0 && augNPC.oldHP > 0)
                        {
                            int overflowDmg = damageDone - augNPC.oldHP;
                            int ai0 = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Basic:
                                    overflowDmg = (int)(overflowDmg * AugPowerArchive.OverflowBas * 0.01f);
                                    ai0 = 0;
                                    break;
                                case AugTier.Uncommon:
                                    overflowDmg = (int)(overflowDmg * AugPowerArchive.OverflowUnc * 0.01f);
                                    ai0 = 1;
                                    break;
                                case AugTier.Rare:
                                    overflowDmg = (int)(overflowDmg * AugPowerArchive.OverflowRar * 0.01f);
                                    ai0 = 2;
                                    break;
                                case AugTier.Epic:
                                    overflowDmg = (int)(overflowDmg * AugPowerArchive.OverflowEpi * 0.01f);
                                    ai0 = 3;
                                    break;
                                case AugTier.Ultimate:
                                    overflowDmg = (int)(overflowDmg * AugPowerArchive.OverflowUlt * 0.01f);
                                    ai0 = 4;
                                    break;
                                default:
                                    overflowDmg = 0;
                                    break;
                            }
                            if(overflowDmg > 0 && player == Main.LocalPlayer)
                            {
                                Vector2 shootVel = Main.rand.NextVector2CircularEdge(12.5f, 12.5f);
                                Vector2 projPos = target.Center;
                                int proj = Projectile.NewProjectile(sourceShoot, projPos, shootVel, ModContent.ProjectileType<OverflowProj>(), 
                                    overflowDmg, hit.Knockback, player.whoAmI, ai0);
                                Main.projectile[proj].Center = projPos;
                            }
                        }
                        break;
                    case AugType.Deathecho:
                        if(target.life <= 0)
                        {
                            int deathechoDmg = damageDone;
                            int ai0 = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Uncommon:
                                    deathechoDmg = (int)(deathechoDmg * (AugPowerArchive.DeathechoUnc * 0.01f));
                                    ai0 = 0;
                                    break;
                                case AugTier.Rare:
                                    deathechoDmg = (int)(deathechoDmg * (AugPowerArchive.DeathechoRar * 0.01f));
                                    ai0 = 1;
                                    break;
                                case AugTier.Epic:
                                    deathechoDmg = (int)(deathechoDmg * (AugPowerArchive.DeathechoEpi * 0.01f));
                                    ai0 = 2;
                                    break;
                                case AugTier.Ultimate:
                                    deathechoDmg = (int)(deathechoDmg * (AugPowerArchive.DeathechoUlt * 0.01f));
                                    ai0 = 3;
                                    break;
                                default:
                                    deathechoDmg = 0;
                                    break;
                            }
                            if (deathechoDmg > 0 && player == Main.LocalPlayer)
                            {
                                Vector2 projPos = target.Center;
                                int proj = Projectile.NewProjectile(sourceShoot, projPos, Vector2.Zero, ModContent.ProjectileType<BlastProj>(),
                                    deathechoDmg, hit.Knockback * 0.75f, player.whoAmI, ai0, 0f);
                                Main.projectile[proj].Center = projPos;
                            }
                        }
                        break;
                    case AugType.Blast:
                        if (Main.rand.NextBool(10))
                        {
                            int blastDmg = damageDone;
                            int ai0 = 0;
                            switch (augTiers[i])
                            {
                                case AugTier.Epic:
                                    blastDmg = (int)(blastDmg * (AugPowerArchive.BlastEpi * 0.01f));
                                    ai0 = 2;
                                    break;
                                case AugTier.Ultimate:
                                    blastDmg = (int)(blastDmg * (AugPowerArchive.BlastUlt * 0.01f));
                                    ai0 = 3;
                                    break;
                                default:
                                    blastDmg = 0;
                                    break;
                            }
                            if (blastDmg > 0 && player == Main.LocalPlayer)
                            {
                                Vector2 projPos = target.Center;
                                int proj = Projectile.NewProjectile(sourceShoot, projPos, Vector2.Zero, ModContent.ProjectileType<BlastProj>(),
                                    blastDmg, 6f, player.whoAmI, ai0, 1f);
                                Main.projectile[proj].Center = projPos;
                            }
                        }
                        break;
                    case AugType.Collateral:
                        int slashDmg = damageDone;
                        int ai = 0;
                        switch (augTiers[i])
                        {
                            case AugTier.Epic:
                                slashDmg = (int)(slashDmg * (AugPowerArchive.CollateralEpi * 0.01f));
                                ai = 0;
                                break;
                            case AugTier.Ultimate:
                                slashDmg = (int)(slashDmg * (AugPowerArchive.CollateralUlt * 0.01f));
                                ai = 1;
                                break;
                            default:
                                slashDmg = 0;
                                break;
                        }
                        if (slashDmg > 0 && player == Main.LocalPlayer)
                        {
                            // Adapted from Super Star Shooter's slash projectiles
                            Vector2 v = Main.rand.NextVector2CircularEdge(200f, 200f);
                            if (v.Y < 0f)
                            {
                                v.Y *= -1f;
                            }
                            v.Y += 100f;
                            Vector2 vector = v.SafeNormalize(v) * 6f;
                            Projectile.NewProjectile(sourceShoot, target.Center - vector * 15f, vector, ModContent.ProjectileType<CollateralSlash>(),
                                slashDmg, hit.Knockback / 2f, player.whoAmI, ai);
                        }
                        break;
                    default:
                        break;
                }
            }
            if(target.life <= 0)
            {
                if (tryFrenzy && Main.rand.NextFloat() <= buffChance && !player.HasBuff(ModContent.BuffType<Frenzy>()))
                {
                    player.AddBuff(ModContent.BuffType<Frenzy>(), Main.rand.Next(200, 600));
                }
                if (tryBattlelust && Main.rand.NextFloat() <= buffChance && !player.HasBuff(ModContent.BuffType<Battlelust>()))
                {
                    player.AddBuff(ModContent.BuffType<Battlelust>(), Main.rand.Next(200, 600));
                }
                if (tryWard && Main.rand.NextFloat() <= buffChance && !player.HasBuff(ModContent.BuffType<Ward>()))
                {
                    player.AddBuff(ModContent.BuffType<Ward>(), Main.rand.Next(200, 600));
                }
            }
            if (addMomentum)
            {
                augPlayer.momentumStacks += atkSpd;
                augPlayer.momentumCooldown = (int)(atkSpd * 5f);
            }
            if (addCharge && augPlayer.superchargeTimer <= 0 && !player.HasBuff(ModContent.BuffType<Supercharge>()))
            {
                augPlayer.superchargeStacks++;
                augPlayer.superchargeTimer = 60;
                augPlayer.superchargeCooldown = 300;
            }
            if (commitEnemy)
            {
                augNPC.committedStacks += atkSpd;
                augNPC.committedCooldown = (int)(atkSpd * 5f);
            }
            if (comboEnemy)
            {
                augNPC.combobreakStacks++;
                augNPC.combobreakCooldown = (int)(atkSpd * 5f);
            }
            if (triggerRunic && player == Main.LocalPlayer)
            {
                augPlayer.runicTimer = 60;
                Item.NewItem(new EntitySource_OnHit(player, target, "runicAugment"), target.Hitbox, ModContent.ItemType<RunicRune>(), noGrabDelay: true);
            }
            if(resurgencePow > 0 && player == Main.LocalPlayer)
            {
                int heal = (int)(damageDone * resurgencePow);
                SpawnResourceProj(target, player, sourceShoot, heal, 1);
            }
            if (siphonTriggered)
            {
                augPlayer.siphonTimer = 60;
            }
        }

        /// <summary>
        /// Used by the Siphon, Lifeleech, and Radiance augments to spawn their repsective projectiles.
        /// </summary>
        /// <param name="target">The enemy that was hit by the attack that triggered the projectile spawn. Projectiles will originate from this NPC.</param>
        /// <param name="player">The player whose attack triggered the projectile spawn. Owner-resotring effects will target this player. This player's lifeSteal will also be checked against by healing effects.</param>
        /// <param name="source">The IEntitySource the game should use when spawning the projectile.</param>
        /// <param name="restoreAmount">How much hp/mana the projectile spawned will grant.</param>
        /// <param name="type">What augment's effects are triggering; 0 = siphon, 1 = lifeleech, 2 = radiance.</param>
        private static void SpawnResourceProj(NPC target, Player player, IEntitySource source, int restoreAmount, int type)
        {
            if(player != Main.LocalPlayer || (player.lifeSteal <= 0 && type != 0))
            {
                return;
            }
            int targetIndex = player.whoAmI;

            switch (type)
            {
                case 0:
                    float manaTracker = 0f;
                    for (int i = 0; i < 255; i++)
                    {
                        Player checkPlayer = Main.player[i];
                        if (checkPlayer.active && !checkPlayer.dead && ((!player.hostile && !checkPlayer.hostile)
                            || player.team == checkPlayer.team) && Vector2.DistanceSquared(target.Center, checkPlayer.Center) < (650f * 650f)
                            && (float)(checkPlayer.statManaMax2 - checkPlayer.statMana) > manaTracker)
                        {
                            manaTracker = checkPlayer.statManaMax2 - checkPlayer.statMana;
                            targetIndex = i;
                        }
                    }
                    break;
                case 1:
                    player.lifeSteal -= restoreAmount;
                    break;
                case 2:
                    player.lifeSteal -= restoreAmount;
                    float hpTracker = 0f;
                    for (int i = 0; i < 255; i++)
                    {
                        Player checkPlayer = Main.player[i];
                        if (checkPlayer.active && !checkPlayer.dead && ((!player.hostile && !checkPlayer.hostile) 
                            || player.team == checkPlayer.team) && Vector2.DistanceSquared(target.Center, checkPlayer.Center) < (650f * 650f) 
                            && (float)(checkPlayer.statLifeMax2 - checkPlayer.statLife) > hpTracker)
                        {
                            hpTracker = checkPlayer.statLifeMax2 - checkPlayer.statLife;
                            targetIndex = i;
                        }
                    }
                    break;
                default:
                    return;
            }
            Vector2 shootVel = Main.rand.NextVector2CircularEdge(10f, 10f);
            Vector2 projPos = target.Center;
            int proj = Projectile.NewProjectile(source, projPos, shootVel, ModContent.ProjectileType<ResourceProj>(),
                                0, 0f, player.whoAmI, targetIndex, restoreAmount, type);
            Main.projectile[proj].Center = projPos;
        }

        /// <summary>
        /// Returns if the given NPC counts as a boss for the purposes of triggering the Kingslayer augment.
        /// </summary>
        private static bool CountsAsBoss(NPC target)
        {
            bool variableCheck = target.boss; 
            bool headCheck = target.GetBossHeadTextureIndex() > 0;
            bool manualCheck = target.type == NPCID.EaterofWorldsBody || target.type == NPCID.EaterofWorldsHead || target.type == NPCID.EaterofWorldsTail
                            || target.type == NPCID.PrimeCannon || target.type == NPCID.PrimeSaw || target.type == NPCID.PrimeVice || target.type == NPCID.PrimeLaser
                            || target.type == NPCID.SkeletronHand
                            || target.type == NPCID.WallofFleshEye
                            || target.type == NPCID.GolemFistLeft || target.type == NPCID.GolemFistRight || target.type == NPCID.GolemHead
                            || target.type == NPCID.Creeper
                            || target.type == NPCID.PlanterasTentacle;
            return (variableCheck || headCheck || manualCheck);
        }

        /// <summary>
        /// Returns a death message for use in deaths to Paincycle's damaging effect.
        /// </summary>
        private static string PaincycleDeathMessage(Player victim)
        {
            if (ModContent.GetInstance<ConfigServer>().CustomDeathMessages)
            {
                List<string> messageList = new List<string>();
                messageList.Add($"{victim.name} should really stop playing Minecraft Dungeons.");
                messageList.Add($"{victim.name} has arguably been using the worst Augment.");
                if (victim.Male)
                {
                    messageList.Add($"Paincycle seems to have hurt {victim.name} more than it hurt his enemies.");
                    messageList.Add($"{victim.name}'s lifeforce was drained by his weapon.");
                }
                else
                {
                    messageList.Add($"Paincycle seems to have hurt {victim.name} more than it hurt her enemies.");
                    messageList.Add($"{victim.name}'s lifeforce was drained by her weapon.");
                }
                messageList.Add($"{victim.name} withered away.");
                messageList.Add($"{victim.name} couldn't handle the pain.");
                messageList.Add($"{victim.name} succumbed to a vicious cycle.");
                messageList.Add($"{victim.name} may want to invest in lifesteal.");
                messageList.Add($"{victim.name} gave in to the dark.");
                messageList.Add($"P A I N C Y C L E");
                return messageList[Main.rand.Next(messageList.Count)];
            }
            return $"{victim.name} was slain...";
        }
    }
}
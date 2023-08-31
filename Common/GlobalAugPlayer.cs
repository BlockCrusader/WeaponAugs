using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Content.Buffs;
using WeaponAugs.Content.Buffs.Debuffs;

namespace WeaponAugs.Common
{
    public class GlobalAugPlayer : ModPlayer
    {
        // Timers; controls how often time-dependent effects trigger/stack
        public int paincycleTimer;
        public int superchargeTimer;
        public int executionTimer = -1;
        public int runicTimer;
        public int siphonTimer;
        public int deathshroudTimer;

        // Cooldown Timers; controls effects' stacks degrading
        public int momentumCooldown = 60;
        public int superchargeCooldown = 300;

        // Stack-trackers; tracks stacks/potency of dynamic effects
        public int unleashStacks;
        public float overheatStacks;
        public int momentumStacks;
        public int superchargeStacks;

        // Hit-counters; controls how often hit-dependent effects trigger/stack
        public int paincycleCounter;

        float augLuck;
        float augSuperluck;
        float augLuckFlag;
        float augSuperluckFlag;
        bool hasOverdrive;
        bool hasUnleash;

        public override void ResetEffects()
        {
            augLuckFlag = augLuck;
            augSuperluckFlag = augSuperluck;
            augLuck = 0;
            augSuperluck = 0;
            hasOverdrive = false;
            hasUnleash = false;
        }

        public override void PreUpdate()
        {
            // Handle timers
            if (momentumStacks > 0)
            {
                momentumCooldown--;
                if (momentumCooldown <= 0)
                {
                    momentumStacks = 0;
                }
            }
            else
            {
                momentumCooldown = 60;
            }

            if (superchargeStacks > 0)
            {
                superchargeCooldown--;
                if (superchargeCooldown <= 0)
                {
                    superchargeStacks --;
                    superchargeCooldown = 60;
                }
            }
            else
            {
                superchargeCooldown = 300;
            }
            if(superchargeStacks < 0)
            {
                superchargeStacks = 0;
            }

            if (paincycleTimer > 0)
            {
                paincycleTimer--;
            }
            if (superchargeTimer > 0)
            {
                superchargeTimer--;
            }
            if (executionTimer > 0)
            {
                executionTimer--;
            }
            if (runicTimer > 0)
            {
                runicTimer--;
            }
            if (siphonTimer > 0)
            {
                siphonTimer--;
            }
            if (deathshroudTimer > 0)
            {
                deathshroudTimer--;
            }
        }

        public override void PostUpdateEquips()
        {
            if(!Player.HeldItem.TryGetGlobalItem(out GlobalAugWep augItem))
            {
                return;
            }
            if(superchargeStacks >= 110)
            {
                superchargeStacks = 0;
                Player.AddBuff(ModContent.BuffType<Supercharge>(), 600);
                SoundEngine.PlaySound(SoundID.Item68, Player.Center);
                for (int i = 0; i < 25; i++)
                {
                    Vector2 speed = Main.rand.NextVector2Circular(7.5f, 7.5f);
                    Dust d = Dust.NewDustPerfect(Player.Center, DustID.RainbowMk2, speed, newColor: Colors.RarityYellow, Scale: 1f);
                    d.noGravity = true;
                }
            }
            HeldAugEffects(augItem.Augments, augItem.AugTiers, augItem.CheckActiveAugs(Player.HeldItem));
            if(unleashStacks > 0)
            {
                Player.AddBuff(ModContent.BuffType<Unleash>(), 2);
            }
            if(momentumStacks > 0)
            {
                Player.AddBuff(ModContent.BuffType<Momentum>(), 2);
            }
            if(superchargeStacks > 0)
            {
                Player.AddBuff(ModContent.BuffType<Charge>(), 2);
            }
        }

        public override void PostUpdate()
        {
            bool notAttacking = Player.itemAnimation <= 0;
            if (hasUnleash && notAttacking)
            {
                unleashStacks++;
            }
            if (!hasOverdrive || notAttacking)
            {
                overheatStacks -= 1.5f;
            }
            if(overheatStacks <= 0)
            {
                overheatStacks = 0;
            }
        }

        /// <summary>
        /// Applies the effects of any augments on a held item
        /// </summary>
        /// <param name="augments">List of augments types on the item.</param>
        /// <param name="augTiers">List of tiers of the augments on the item.</param>
        /// <param name="augCount">Number of augments to be checked.</param>
        private void HeldAugEffects(List<AugType> augments, List<AugTier> augTiers, int augCount)
        {
            float moveSpdMult = 1f;
            float aerialMult = 1f;
            float wardDR = 1f;
            bool usingHeartsurge = false;
            hasUnleash = false;
            hasOverdrive = false;
            for (int i = 0; i < augCount; i++)
            {
                switch (augments[i])
                {
                    case AugType.Revitalize:
                        switch (augTiers[i])
                        {
                            case AugTier.Uncommon:
                                Player.lifeRegen += (int)(AugPowerArchive.RevitalizeUnc * 2f);
                                break;
                            case AugTier.Rare:
                                Player.lifeRegen += (int)(AugPowerArchive.RevitalizeRar * 2f);
                                break;
                            case AugTier.Epic:
                                Player.lifeRegen += (int)(AugPowerArchive.RevitalizeEpi * 2f);
                                break;
                            case AugTier.Ultimate:
                                Player.lifeRegen += (int)(AugPowerArchive.RevitalizeUlt * 2f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Lightweight:
                        switch (augTiers[i])
                        {
                            case AugTier.Uncommon:
                                moveSpdMult += AugPowerArchive.LightweightUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                moveSpdMult += AugPowerArchive.LightweightRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                moveSpdMult += AugPowerArchive.LightweightEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                moveSpdMult += AugPowerArchive.LightweightUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Uplifting:
                        switch (augTiers[i])
                        {
                            case AugTier.Uncommon:
                                aerialMult += AugPowerArchive.UpliftingUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                aerialMult += AugPowerArchive.UpliftingRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                aerialMult += AugPowerArchive.UpliftingEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                aerialMult += AugPowerArchive.UpliftingUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Sturdy:
                        switch (augTiers[i])
                        {
                            case AugTier.Uncommon:
                                Player.statDefense += (int)AugPowerArchive.SturdyUnc;
                                break;
                            case AugTier.Rare:
                                Player.statDefense += (int)AugPowerArchive.SturdyRar;
                                break;
                            case AugTier.Epic:
                                Player.statDefense += (int)AugPowerArchive.SturdyEpi;
                                break;
                            case AugTier.Ultimate:
                                Player.statDefense += (int)AugPowerArchive.SturdyUlt;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Ward:
                        switch (augTiers[i])
                        {
                            case AugTier.Uncommon:
                                wardDR *= 1f - AugPowerArchive.WardUnc * 0.01f;
                                break;
                            case AugTier.Rare:
                                wardDR *= 1f -AugPowerArchive.WardRar * 0.01f;
                                break;
                            case AugTier.Epic:
                                wardDR *= 1f - AugPowerArchive.WardEpi * 0.01f;
                                break;
                            case AugTier.Ultimate:
                                wardDR *= 1f - AugPowerArchive.WardUlt * 0.01f;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Luck:
                        switch (augTiers[i])
                        {
                            case AugTier.Uncommon:
                                augLuck += AugPowerArchive.LuckUnc;
                                break;
                            case AugTier.Rare:
                                augLuck += AugPowerArchive.LuckRar;
                                break;
                            case AugTier.Epic:
                                augLuck += AugPowerArchive.LuckEpi;
                                break;
                            case AugTier.Ultimate:
                                augLuck += AugPowerArchive.LuckUlt;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Superluck:
                        switch (augTiers[i])
                        {
                            case AugTier.Rare:
                                augSuperluck += AugPowerArchive.SuperluckRar;
                                break;
                            case AugTier.Epic:
                                augSuperluck += AugPowerArchive.SuperluckEpi;
                                break;
                            case AugTier.Ultimate:
                                augSuperluck += AugPowerArchive.SuperluckUlt;
                                break;
                            default:
                                break;
                        }
                        break;
                    case AugType.Heartsurge:
                        usingHeartsurge = true;
                        break;
                    case AugType.Overdrive:
                        if(overheatStacks >= 750)
                        {
                            Player.AddBuff(ModContent.BuffType<Overheat>(), 2);
                        }
                        hasOverdrive = true;
                        break;
                    case AugType.Unleash:
                        hasUnleash = true;
                        break;
                    default:
                        break;
                }
            }
            if (!hasUnleash)
            {
                unleashStacks = 0;
            }
            if (usingHeartsurge && Player.HasBuff(ModContent.BuffType<Heartsurge>()))
            {
                Player.lifeRegen += 4;
            }
            if(wardDR < 1f && Player.HasBuff(ModContent.BuffType<Ward>()))
            {
                Player.endurance = (wardDR * (1f - Player.endurance));
            }
            if (Player.mount.Type == MountID.None)
            {
                if (moveSpdMult > 1f)
                {
                    Player.runAcceleration *= moveSpdMult;
                    Player.maxRunSpeed *= moveSpdMult;
                    Player.accRunSpeed *= moveSpdMult;
                    Player.runSlowdown *= moveSpdMult;
                }
                if (aerialMult > 1f)
                {
                    if (Player.wingTimeMax > 0)
                    {
                        Player.wingTimeMax = (int)(Player.wingTimeMax * aerialMult);
                    }
                    Player.jumpSpeedBoost += Player.jumpSpeed * (aerialMult - 1f);
                }
            }
        }

        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<Resurgence>())) // Zeroes out life regen
            {
                Player.lifeRegen = 0;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (!Player.HasBuff(ModContent.BuffType<ResurgenceCooldown>()) && Player.HeldItem.TryGetGlobalItem(out GlobalAugWep augItem))
            {
                float resurgencePotency = 0;
                for (int i = 0; i < augItem.CheckActiveAugs(Player.HeldItem); i++)
                {
                    if (augItem.Augments[i] == AugType.Resurgence)
                    {
                        resurgencePotency += AugPowerArchive.ResurgenceUlt * 0.01f;
                    }
                }
                if(resurgencePotency > 0)
                {
                    if(Player == Main.LocalPlayer)
                    {
                        Player.statLife = 1;
                        Player.AddBuff(ModContent.BuffType<Resurgence>(), 300);
                        Player.AddBuff(ModContent.BuffType<ResurgenceCooldown>(), 18000);
                    }
                    SoundEngine.PlaySound(SoundID.Item29, Player.Center);
                    for (int i = 0; i < 30; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(12.5f, 6f);
                        speed.Y -= 4f;
                        Dust d = Dust.NewDustPerfect(Player.Center, DustID.RainbowMk2, speed, newColor: Colors.RarityDarkPurple, Scale: 1.2f);
                        d.noGravity = true;
                        d.fadeIn = 1.5f;
                    }
                    return false;
                }
            }
            return true;
        }

        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (Player.HasBuff(ModContent.BuffType<Deathshroud>()) && Player.HeldItem.TryGetGlobalItem(out GlobalAugWep augItem))
            {
                for(int i = 0; i < augItem.CheckActiveAugs(Player.HeldItem); i++)
                {
                    if (augItem.Augments[i] == AugType.Deathshroud)
                    {
                        TriggerDeathshroud(augItem.AugTiers[i]);
                        return true;
                    }
                }
            }
            return false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            momentumStacks = (int)(momentumStacks * 0.5f);
            superchargeStacks -= 5;
            superchargeTimer = 120;
        }

        private void TriggerDeathshroud(AugTier tierForFX = AugTier.None)
        {
            if(tierForFX != AugTier.None)
            {
                Color dustColor = default;
                switch (tierForFX)
                {
                    case AugTier.Uncommon:
                        dustColor = Colors.RarityOrange;
                        break;
                    case AugTier.Rare:
                        dustColor = Colors.RarityLime;
                        break;
                    case AugTier.Epic:
                        dustColor = Colors.RarityCyan;
                        break;
                    case AugTier.Ultimate:
                        dustColor = Colors.RarityDarkPurple;
                        break;
                    default:
                        break;
                }
                if(dustColor != default)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        Vector2 speed = Main.rand.NextVector2Circular(4f, 4f);
                        Dust d = Dust.NewDustPerfect(Player.Center, DustID.RainbowMk2, speed, newColor: dustColor, Scale: 1.2f);
                        d.noGravity = true;
                        d.fadeIn = 1f;
                    }
                }
            }

            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 100 : 60);

            if (Player.whoAmI != Main.myPlayer)
            {
                return;
            }

            Player.ClearBuff(ModContent.BuffType<Deathshroud>());

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                DeathshroudNetcode(Player.whoAmI);
            }
        }

        // Adapted from Example Mod. Handles netcode related to Deathshroud's dodge
        public static void HandleDeathshroudNetcode(BinaryReader reader, int whoAmI)
        {
            int player = reader.ReadByte();
            if (Main.netMode == NetmodeID.Server)
            {
                player = whoAmI;
            }

            Main.player[player].GetModPlayer<GlobalAugPlayer>().TriggerDeathshroud();

            if (Main.netMode == NetmodeID.Server)
            {
                // If the server receives this message, it sends it to all other clients to sync the effects.
                DeathshroudNetcode(player);
            }
        }
        public static void DeathshroudNetcode(int whoAmI)
        {
            ModPacket packet = ModContent.GetInstance<WeaponAugs>().GetPacket();
            packet.Write((byte)WeaponAugs.MessageType.DeathshroudDodge);
            packet.Write((byte)whoAmI);
            packet.Send(ignoreClient: whoAmI);
        }

        public override bool PreModifyLuck(ref float luck)
        {
            if (augSuperluckFlag > 0)
            {
                Player.equipmentBasedLuckBonus += augSuperluckFlag;
            }
            return true;
        }

        public override void ModifyLuck(ref float luck)
        {
            if(augLuckFlag > 0)
            {
                luck += augLuckFlag;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<Overheat>()))
            {
                // Nullify positive regen
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;

                Player.lifeRegen -= 10; // 5 DPS
            }
        }
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponAugs.Common
{
    public class GlobalAugNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        // Cooldown Timers; controls effects' stacks degrading
        public int combobreakCooldown = 60;
        public int committedCooldown = 60;

        // Stack-trackers; tracks stacks/potency of dynamic effects
        public int combobreakStacks = 0;
        public int committedStacks;

        // Debuffs; tracks debuffs from this mod
        public bool runicBlaze;
        public bool runicTaint;
        public int rendTier;
        public int rallyTier;
        public bool powerStolen;

        public int oldHP;

        public override void ResetEffects(NPC npc)
        {
            runicBlaze = false;
            runicTaint = false;
            powerStolen = false;
            rendTier = 0;
            rallyTier = 0;
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            switch (rendTier)
            {
                case 1:
                    modifiers.Defense.Flat -= AugPowerArchive.RendUnc;
                    break;
                case 2:
                    modifiers.Defense.Flat -= AugPowerArchive.RendRar;
                    break;
                case 3:
                    modifiers.Defense.Flat -= AugPowerArchive.RendEpi;
                    break;
                case 4:
                    modifiers.Defense.Flat -= AugPowerArchive.RendUlt;
                    break;
                default:
                    break;
            }
            switch (rallyTier)
            {
                case 1:
                    modifiers.FlatBonusDamage += AugPowerArchive.RallyRar;
                    break;
                case 2:
                    modifiers.FlatBonusDamage += AugPowerArchive.RallyEpi;
                    break;
                case 3:
                    modifiers.FlatBonusDamage += AugPowerArchive.RallyUlt;
                    break;
                default:
                    break;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (runicBlaze)
            {
                npc.lifeRegen -= 72; // 36 DPS
                damage += 36;
                if (damage < 36)
                {
                    damage = 36;
                }
            }
            if (runicTaint)
            {
                npc.lifeRegen -= 12; // 6 DPS
                damage += 6;
                if (damage < 6)
                {
                    damage = 6;
                }
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (powerStolen)
            {
                modifiers.FinalDamage *= 0.9f;
            }
        }

        public override bool PreAI(NPC npc)
        {
            if(npc.life > 0)
            {
                oldHP = npc.life;
            }
            return true;
        }

        public override void AI(NPC npc)
        {
            // Handle timers
            if(committedStacks > 0)
            {
                committedCooldown--;
                if(committedCooldown <= 0)
                {
                    committedStacks = 0;
                }
            }
            else
            {
                committedCooldown = 60;
            }
            if (combobreakStacks > 0)
            {
                combobreakCooldown--;
                if (combobreakCooldown <= 0)
                {
                    combobreakStacks = 0;
                }
            }
            else
            {
                combobreakCooldown = 60;
            }

            // Visual FX
            if (runicBlaze)
            {
                if (Main.rand.NextBool(3))
                {
                    Vector3 rainbowHSL = Main.rgbToHsl(Main.DiscoColor);
                    rainbowHSL.Y = 0.1f;
                    Color dustColor = Main.hslToRgb(rainbowHSL);
                    int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, DustID.RainbowTorch, 0f, 0f, 0, dustColor, Main.rand.NextFloat(0.4f, 1.1f));
                    Main.dust[dustIndex].fadeIn = Main.rand.NextFloat();
                    Main.dust[dustIndex].noGravity = true;
                }
            }
            if (rendTier > 0)
            {
                Color dustColor = default;
                switch (rendTier)
                {
                    case 1:
                        dustColor = Colors.RarityOrange;
                        break;
                    case 2:
                        dustColor = Colors.RarityLime;
                        break;
                    case 3:
                        dustColor = Colors.RarityCyan;
                        break;
                    case 4:
                        dustColor = Colors.RarityDarkPurple;
                        break;
                    default:
                        break;
                }
                if (Main.rand.NextBool(3))
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    Vector2 offset = new Vector2(Main.rand.NextFloat(npc.width), Main.rand.NextFloat(npc.height));
                    Dust dust = Dust.NewDustPerfect(npc.position + offset, DustID.RainbowMk2, speed * 2f, 0, dustColor, Main.rand.NextFloat(0.4f, 1.1f));
                    dust.noGravity = true;
                }
            }
            if (committedStacks > 0)
            {
                if (Main.rand.NextBool(2))
                {
                    Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                    float radius = npc.width > npc.height ? npc.width : npc.height;
                    Dust dust = Dust.NewDustPerfect(npc.Center + speed * radius, DustID.RainbowMk2, speed * -3f, 0, Colors.RarityDarkRed, 0.5f);
                    dust.noGravity = true;
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (runicTaint)
            {
                drawColor.R = (byte)(drawColor.R * 0.85f);
                drawColor.B = (byte)(drawColor.B * 0.85f);
            }
            if (powerStolen)
            {
                drawColor.R = (byte)(drawColor.R * 0.9f);
                drawColor.G = (byte)(drawColor.G * 0.75f);
            }
        }
    }
}
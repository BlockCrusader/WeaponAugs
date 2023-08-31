using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Projectiles
{
	class BlastProj : ModProjectile
	{
        public override void SetDefaults()
		{
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.timeLeft = 3;
            Projectile.tileCollide = false;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }

        public override void Kill(int timeLeft)
        {
            if(Projectile.ai[1] == 0f)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath6 with { Volume = 0.75f }, Projectile.Center);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.Item14 with { Volume = 0.75f }, Projectile.Center);
            }
            Projectile.Damage();
            Color dustColor;
            switch (Projectile.ai[0])
            {
                case 0:
                    dustColor = Colors.RarityOrange;
                    break;
                case 1:
                    dustColor = Colors.RarityLime;
                    break;
                case 2:
                    dustColor = Colors.RarityCyan;
                    break;
                case 3:
                    dustColor = Colors.RarityDarkPurple;
                    break;
                default:
                    return;
            }
            for (int i = 0; i < 40; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center + speed * 30, DustID.RainbowMk2, speed * 5f, newColor: dustColor, Scale: 1f);
                d.noGravity = true;
                Vector2 speed2 = Main.rand.NextVector2Circular(1f, 1f);
                Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.RainbowMk2, speed2 * 10f, newColor: dustColor, Scale: 1f);
                d2.noGravity = true;
            }
        }
    }
}
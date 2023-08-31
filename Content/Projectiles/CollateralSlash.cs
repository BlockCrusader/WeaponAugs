using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Projectiles
{
	public class CollateralSlash : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 2;
			Projectile.timeLeft = 10 * Projectile.MaxUpdates;
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.penetrate = 3;
			Projectile.DamageType = DamageClass.Generic;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
			DrawOffsetX = -33;
			DrawOriginOffsetY = -33;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}

        public override void OnSpawn(IEntitySource source)
        {
			// Sets appearance based on ai
			Projectile.frame = (int)Projectile.ai[0];
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
		}

        public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 75;
			}
			if(Projectile.alpha < 0)
            {
				Projectile.alpha = 0;
            }

			if (Main.rand.NextBool(8))
			{
				Color dustColor;
				switch (Projectile.ai[0])
				{
					case 0:
						dustColor = Colors.RarityCyan;
						break;
					case 1:
						dustColor = Colors.RarityDarkPurple;
						break;
					default:
						return;
				}
				Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.RainbowMk2, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, dustColor, 0.9f);
				dust.noGravity = true;
				dust.position = Projectile.Center;
				dust.velocity = Main.rand.NextVector2Circular(1f, 1f) + Projectile.velocity * 0.5f;
			}
		}

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			modifiers.ScalingArmorPenetration += 1f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Projectile.damage = (int)(Projectile.damage * (2f / 3f));
        }

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255);
		}
	}
}
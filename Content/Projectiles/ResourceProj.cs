using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Projectiles
{
	public class ResourceProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.DamageType = DamageClass.Default;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 4;
		}

        public override void OnSpawn(IEntitySource source)
        {
			Projectile.frame = (int)Projectile.ai[2]; // Sets appearance based on AI
		}

        public override void AI()
        {
			VisualFX();

			Player target = Main.player[(int)Projectile.ai[0]];
			float distToTargetX = target.Center.X - Projectile.Center.X;
			float distToTargetY = target.Center.Y - Projectile.Center.Y;
			float distToTarget = (float)Math.Sqrt((double)(distToTargetX * distToTargetX + distToTargetY * distToTargetY));
            if (Projectile.Hitbox.Intersects(target.Hitbox))
			{
				if (Projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech && Projectile.ai[2] != 0)
				{
					int restoreAmount = (int)Projectile.ai[1];
					target.HealEffect(restoreAmount, broadcast: false);
					target.statLife += restoreAmount;
					if (target.statLife > target.statLifeMax2)
					{
						target.statLife = target.statLifeMax2;
					}
					NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, (int)Projectile.ai[0], restoreAmount);
				}
				else if (Projectile.owner == Main.myPlayer && Projectile.ai[2] == 0)
				{
					int restoreAmount = (int)Projectile.ai[1];
					target.ManaEffect(restoreAmount);
					target.statMana += restoreAmount;
					if (target.statMana > target.statManaMax2)
					{
						target.statMana = target.statManaMax2;
					}
					NetMessage.SendData(MessageID.ManaEffect, -1, -1, null, (int)Projectile.ai[0], restoreAmount);
				}
				Projectile.Kill();
			}
			distToTarget = 4f / distToTarget;
			distToTargetX *= distToTarget;
			distToTargetY *= distToTarget;
			Projectile.velocity.X = (Projectile.velocity.X * 15f + distToTargetX) / 16f;
			Projectile.velocity.Y = (Projectile.velocity.Y * 15f + distToTargetY) / 16f;
		}

		private void VisualFX()
		{
			

			Projectile.rotation += 0.04f * (float)Projectile.direction;

			if (Main.rand.NextBool(5))
			{
				Color dustColor;
				switch (Projectile.ai[2])
				{
					case 0:
						dustColor = new Color(109, 54, 226);
						break;
					case 1:
						dustColor = new Color(226, 54, 90);
						break;
					case 2:
						dustColor = new Color(226, 180, 54);
						break;
					default:
						return;
				}
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
											 DustID.RainbowMk2, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f,
											 newColor: dustColor, Scale: 0.8f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 0.7f;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			// Adapted from Bone Glove's projectiles
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Rectangle rect = texture.Frame(1, 3);
			int height = rect.Height;
			rect.Height -= 2;
			int frame = Projectile.frame;
			rect.Y = height * frame;

			Vector2 drawOrigin = rect.Size() / 2f;

			for (int i = 1; i < 10; i++)
			{
				if (i >= Projectile.oldPos.Length)
				{
					continue;
				}
				Color drawColor = new Color(255, 255, 255, 255) * (1f - i / 10f);
				SpriteEffects fx = ((Projectile.oldSpriteDirection[i] == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);

				if (Projectile.oldPos[i] == Vector2.Zero)
				{
					continue;
				}

				Vector2 drawPos = Projectile.oldPos[i] + Vector2.Zero + Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
				Main.EntitySpriteDraw(texture, drawPos, rect, drawColor, Projectile.oldRot[i] +
					Projectile.rotation * 0.2f * (i - 1) * -fx.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(),
					drawOrigin, MathHelper.Lerp(Projectile.scale, 0.7f, i / 15f), fx);

				Color color = Projectile.GetAlpha(default(Color));
				Main.EntitySpriteDraw(texture, Projectile.Center + Vector2.Zero - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
					rect, color, Projectile.rotation, drawOrigin, Projectile.scale, fx);
			}

			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item4 with { Volume = 0.4f }, Projectile.Center);
			Color dustColor;
			switch (Projectile.ai[2])
			{
				case 0:
					dustColor = new Color(109, 54, 226);
					break;
				case 1:
					dustColor = new Color(226, 54, 90);
					break;
				case 2:
					dustColor = new Color(226, 180, 54);
					break;
				default:
					return;
			}
			for (int i = 0; i < 15; i++)
			{
				Vector2 speed2 = Main.rand.NextVector2Circular(1f, 1f);
				Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.RainbowMk2, speed2 * 5f, newColor: dustColor, Scale: 1f);
				d2.noGravity = true;
			}
		}
	}
}
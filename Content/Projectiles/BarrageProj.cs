using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Projectiles
{
	class BarrageProj : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 5;
        }

        public override string Texture => "WeaponAugs/Content/Projectiles/AttackProj";
        public override void SetDefaults()
		{
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            DrawOffsetX = -2;
            DrawOriginOffsetY = -2;
            Projectile.timeLeft = 45;
            Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = (int)Projectile.ai[0]; // Sets appearance based on AI
        }

        public override void AI()
        {
            VisualFX();
        }

        private void VisualFX()
        {
            Projectile.rotation += 0.2f * (float)Projectile.direction;

            if (Main.rand.NextBool())
            {
                Color dustColor;
                switch (Projectile.ai[0])
                {
                    case 0:
                        dustColor = Colors.RarityNormal;
                        break;
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
                        return;
                }
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                                             DustID.RainbowMk2, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f,
                                             newColor: dustColor, Scale: 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.7f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255) * (1f - (float)Projectile.alpha / 255f);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Adapted from Bone Glove's projectiles
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Rectangle rect = texture.Frame(1, 5);
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

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item10 with { Volume = 0.75f }, Projectile.Center);
            for(int i = 0; i < 10; i++)
            {
                Color dustColor;
                switch (Projectile.ai[0])
                {
                    case 0:
                        dustColor = Colors.RarityNormal;
                        break;
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
                        return;
                }
                Vector2 dustVelocity = Projectile.oldVelocity * Main.rand.NextFloat(-0.3f, 0.6f);
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                                             DustID.RainbowMk2, dustVelocity.X, dustVelocity.Y,
                                             newColor: dustColor, Scale: 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.8f;
            }
        }
    }
}
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Content.Projectiles;

namespace WeaponAugs.Common
{
	// This class is used to sync and apply weapons' augments to their projectiles
	public class GlobalAugProj : GlobalProjectile
	{
        // These manage applied Augments. See GlobalAugWep for more details
        public List<AugType> Augments { get; private set; } = new List<AugType>();
		public List<AugTier> AugTiers { get; private set; } = new List<AugTier>();

		private float hitSpd; // Attack speed impacts power/frequency of various effects, so it is saved on-spawn for that reason
		private int baseDmg; // Much like the exsisting originalDamage variable, but set for all projectiles to scale effects in ModifyHit

		public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
			// A list of projectiles spawned from Rune effects
			// These are excluded to prevent the potential of creating infinite projectile-spawning loops
			int[] excludedProjs = new int[]
			{
				ModContent.ProjectileType<BarrageProj>(),
				ModContent.ProjectileType<BlastProj>(),
				ModContent.ProjectileType<CollateralSlash>(),
				ModContent.ProjectileType<OverflowProj>(),
				ModContent.ProjectileType<ResourceProj>(),
			};
			foreach (int excludedProj in excludedProjs)
			{
				if (excludedProj == entity.type)
				{
					return false;
				}
			}
			return !entity.hostile;
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			if(projectile.owner == 255) // Excludes server-owned projectiles
            {
				return;
            }
			if (source is EntitySource_ItemUse itemSource)
			{
				if (itemSource.Item.TryGetGlobalItem(out GlobalAugWep _))
				{
                    InheritAugs(projectile, itemSource.Item);
					InheritStats(projectile, itemSource.Item);
				}
			}
            else if (source is EntitySource_Parent parent && parent.Entity is Projectile proj) // Adds support for secondary (As well as tertiary and so on) projectiles
            {
				if (proj.TryGetGlobalProjectile(out GlobalAugProj parentProj))
				{
					InheritAugs(projectile, parentProj);
					InheritStats(projectile, parentProj);
				}
            }
        }

		private static bool DoesAugEffectProj(AugType augment, Projectile proj)
        {
			if (augment == AugType.Titanreach && ProjectileID.Sets.IsAWhip[proj.type])
			{
				return true;
			}
			List<AugType> excludedAugs = new List<AugType>()
            {
				AugType.Arcana,
				AugType.Titanreach,
				AugType.Conservation,
				AugType.Lightweight,
				AugType.Revitalize,
				AugType.Sturdy,
				AugType.Barrage,
				AugType.Uplifting,
				AugType.Luck,
				AugType.Superluck,
				AugType.Ultracutter,
			};
			if(proj.minion || proj.sentry)
            {
				excludedAugs.Add(AugType.Precision);
				excludedAugs.Add(AugType.Dire);
				excludedAugs.Add(AugType.Minicrit);
				excludedAugs.Add(AugType.Hypercrit);
				excludedAugs.Add(AugType.Unstable);
				excludedAugs.Add(AugType.Unleash);
				excludedAugs.Add(AugType.Rally);
				excludedAugs.Add(AugType.Vigor);
				excludedAugs.Add(AugType.Determination);
				excludedAugs.Add(AugType.Overdrive);
				excludedAugs.Add(AugType.Committed);
				excludedAugs.Add(AugType.Supercharge);
				excludedAugs.Add(AugType.Runic);
				excludedAugs.Add(AugType.Powertheft);
				excludedAugs.Add(AugType.Battlelust);
				excludedAugs.Add(AugType.Frenzy);
				excludedAugs.Add(AugType.Momentum);
				excludedAugs.Add(AugType.Megastrike);
				excludedAugs.Add(AugType.Wildstrike);
			}
			foreach (AugType excludedAug in excludedAugs)
            {
				if(excludedAug == augment)
                {
					return false;
                }
			}
			return true;
        }

        /// <summary>
        /// Copies the augments of the parent Item over to the projectile.
        /// </summary>
        /// <param name="proj">The projectile receiving the Augments.</param>
		/// <param name="parentItem">The (Augmented) item that spawned the projectile.</param>
        private void InheritAugs(Projectile proj, Item parentItem)
        {
			if(Main.player[proj.owner] == null || !Main.player[proj.owner].active)
				return;
            

			GlobalAugWep parentAugItem = parentItem.GetGlobalItem<GlobalAugWep>();
			for (int i = 0; i < parentAugItem.CheckActiveAugs(parentItem); i++)
            {
                if (DoesAugEffectProj(parentAugItem.Augments[i], proj))
                {
					Augments.Add(parentAugItem.Augments[i]);
					AugTiers.Add(parentAugItem.AugTiers[i]);
				}
			}

			// Since this method is called in the On-Spawn hook, we'll take advantage of the opertunity to go ahead and handle the effects of Titanreach on whips
			HandleWhipTitanreach(proj);
		}
        /// <summary>
        /// Overload of InheritAugs for secondary projectiles. Augments are instead copied from a parent projectile.
        /// </summary>
        /// <param name="proj">The projectile receiving the Augments.</param>
        /// <param name="parentProj">The Augmented projectile that spawned the receiving projectile.</param>
        private void InheritAugs(Projectile proj, GlobalAugProj parentProj)
        {
            if (Main.player[proj.owner] == null || !Main.player[proj.owner].active) 
				return;

            for (int i = 0; i < parentProj.Augments.Count; i++)
            {
                Augments.Add(parentProj.Augments[i]);
                AugTiers.Add(parentProj.AugTiers[i]);
            }
            HandleWhipTitanreach(proj);
        }

        /// <summary>
        /// Records the base damage and attack speed of the parent Item onto the projectile. These stats influence some Augment's behavior/potency.
        /// </summary>
        /// <param name="proj">The projectile receiving the Augments.</param>
        /// <param name="item">The (Augmented) item that spawned the projectile.</param>
        private void InheritStats(Projectile proj, Item item)
        {
			baseDmg = proj.damage; // We don't need the item for this; we only set it here to ensure it is only used in scenarios with augmented weapons
			hitSpd = (float)(item.useTime * Main.player[proj.owner].GetWeaponAttackSpeed(item)); 

			// Yoyos override hitSpd to 15
			if(proj.aiStyle == ProjAIStyleID.Yoyo) 
            {
				hitSpd = 15;
            }
		}
        /// <summary>
        /// Overload of InheritStats for secondary projectiles. hitSpd is instead copied from a parent projectile.
        /// </summary>
        /// <param name="proj">The projectile receiving the stats.</param>
        /// <param name="parentProj">The Augmented projectile that spawned the receiving projectile.</param>
        private void InheritStats(Projectile proj, GlobalAugProj parentProj)
        {
            baseDmg = proj.damage;
            hitSpd = parentProj.hitSpd;

            if (proj.aiStyle == ProjAIStyleID.Yoyo)
            {
                hitSpd = 15;
            }
        }

        /// <summary>
        /// Applies Titanreach to whips, increasing their reach. This is called as part of InheritAugs (and in turn OnSpawn) to avoid repeatedly increasing whip length
        /// </summary>
        private void HandleWhipTitanreach(Projectile proj)
        {
			if (ProjectileID.Sets.IsAWhip[proj.type])
			{
				float sizeScale = 1f;
				for (int i = 0; i < Augments.Count; i++)
				{
					if (Augments[i] == AugType.Titanreach)
					{
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								sizeScale += AugPowerArchive.TitanreachBas * 0.01f;
								break;
							case AugTier.Uncommon:
								sizeScale += AugPowerArchive.TitanreachUnc * 0.01f;
								break;
							case AugTier.Rare:
								sizeScale += AugPowerArchive.TitanreachRar * 0.01f;
								break;
							case AugTier.Epic:
								sizeScale += AugPowerArchive.TitanreachEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								sizeScale += AugPowerArchive.TitanreachUlt * 0.01f;
								break;
							default:
								break;
						}
					}
				}
				if (sizeScale > 1f)
				{
					proj.WhipSettings.RangeMultiplier *= sizeScale;
				}
			}
		}

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
			List<AugType> augments = new();
			List<AugTier> augTiers = new();
			for (int i = 0; i < Augments.Count; i++)
			{
				augments.Add(Augments[i]);
				augTiers.Add(AugTiers[i]);
			}
			AugmentHelpers.CombinedAugModifyHit(Main.player[projectile.owner], target, ref modifiers, baseDmg, (int)hitSpd, augments, augTiers);
		}

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
			List<AugType> augments = new();
			List<AugTier> augTiers = new();
			for (int i = 0; i < Augments.Count; i++)
			{
				augments.Add(Augments[i]);
				augTiers.Add(AugTiers[i]);
			}
			bool persistent = projectile.minion || projectile.sentry || projectile.ContinuouslyUpdateDamageStats;
			AugmentHelpers.CombinedAugOnHit(projectile.GetSource_FromThis(), Main.player[projectile.owner], target, hit, damageDone, (int)hitSpd, augments, augTiers, persistent);
		}

        public override void AI(Projectile projectile)
        {
			AugDust(projectile);
		}

        /// <summary>
        /// Generates dust - based on the tiers of the projectile's Augment(s) - as a visual effect/indicator for Augmented weapons.
        /// </summary>
        private void AugDust(Projectile projectile)
		{
			if (!ModContent.GetInstance<ConfigClient>().AugmentDust || projectile.noEnchantmentVisuals)
			{
				return;
			}
			if (Augments.Count > 0 && !projectile.sentry && !projectile.minion)
			{
				List<Color> dustColors = new();
				for (int i = 0; i < Augments.Count; i++)
				{
					if (DoesAugEffectProj(Augments[i], projectile))
					{
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								dustColors.Add(Colors.RarityNormal);
								break;
							case AugTier.Uncommon:
								dustColors.Add(Colors.RarityOrange);
								break;
							case AugTier.Rare:
								dustColors.Add(Colors.RarityLime);
								break;
							case AugTier.Epic:
								dustColors.Add(Colors.RarityCyan);
								break;
							case AugTier.Ultimate:
								dustColors.Add(Colors.RarityDarkPurple);
								break;
							default:
								break;
						}
					}
				}
				if (dustColors.Count <= 0)
				{
					return;
				}
				Color dustColor = dustColors[Main.rand.Next(dustColors.Count)];
				if (Main.rand.NextBool((int)(4 * (1 + 0.5f * projectile.extraUpdates))))
				{
                    if (ProjectileID.Sets.IsAWhip[projectile.type])
                    {
						projectile.WhipPointsForCollision.Clear();
						Projectile.FillWhipControlPoints(projectile, projectile.WhipPointsForCollision);
						Vector2 vector = projectile.WhipPointsForCollision[projectile.WhipPointsForCollision.Count - 1];

						int dustIndex = Dust.NewDust(vector, 0, 0,
													 DustID.RainbowMk2, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f,
													 newColor: dustColor, Scale: 0.8f);
						Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].noLightEmittence = true;
                        Main.dust[dustIndex].velocity *= 0.7f;
					}
                    else
                    {
						int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height,
													 DustID.RainbowMk2, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f,
													 newColor: dustColor, Scale: 0.8f);
						Main.dust[dustIndex].noGravity = true;
                        Main.dust[dustIndex].noLightEmittence = true;
                        Main.dust[dustIndex].velocity *= 0.7f;
					}
				}
			}
		}
	}
}
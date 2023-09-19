using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System;
using Terraria.ID;
using Terraria.Localization;
using WeaponAugs.Content.Buffs;
using WeaponAugs.Content.Projectiles;
using System.Linq;
using System.IO;

namespace WeaponAugs.Common
{
	public class GlobalAugWep : GlobalItem
	{
		// These lists track what Augment(s) a weapon has
		// For each item added to Augments, a corresponding entry should (ALWAYS) be made in AugTiers (At the same index!)
		public List<AugType> Augments { get; private set; } = new List<AugType>(); // Actual augment
		public List<AugTier> AugTiers { get; private set; } = new List<AugTier>(); // Tier of augments; this determines the effect's strength

		// Whenever the Augments and AugTiers lists above are updated and/or used, their length is checked against this number
		// New augments can't be added if this limit would be exceeded, and if the limit is somehow exceeded, those past the limit don't function
		// Typically this would be a private and/or constant varaible, but it is left public for cross-mod functionalities; particularly with Block's Weapon Leveling
		public int AugLimit = 1;

		private int barrageCounter;

		public override bool InstancePerEntity => true;

		public override bool AppliesToEntity(Item entity, bool lateInstantiation)
		{
			if (entity.damage > 0 && !entity.consumable && entity.ammo == AmmoID.None && !entity.accessory)
			{
				return lateInstantiation;
			}
			else
			{
				return false;
			}
		}

		public override void LoadData(Item item, TagCompound tag)
		{
            if (tag.ContainsKey("augList") && tag.ContainsKey("augTiersList"))
            {
				List<int> AugsLoad = tag.Get<List<int>>("augList");
				List<int> TiersLoad = tag.Get<List<int>>("augTiersList");

				// The Augments lists must be saved as ints, so they are converted back here
				for (int i = 0; i < AugsLoad.Count; i++)
				{
					Augments.Add((AugType)AugsLoad[i]);
					AugTiers.Add((AugTier)TiersLoad[i]);
				}
			}

			UpdateAugLimit(item);
		}

		public override void SaveData(Item item, TagCompound tag)
		{
			// The AugType and AugTier enums must be converted before they can be saved
			UpdateAugLimit(item);
			if (Augments.Count > 0)
            {
				List<int> AugsSave = new List<int>();
				List<int> TiersSave = new List<int>();
				for (int i = 0; i < Augments.Count; i++)
				{
					AugsSave.Add((int)Augments[i]);
					TiersSave.Add((int)AugTiers[i]);
				}
				tag["augList"] = AugsSave;
				tag["augTiersList"] = TiersSave;
			}
		}

		public override void NetSend(Item item, BinaryWriter writer)
		{
			FillLists();

			// Writes out the lists one byte at a time.
			// Using for loops causes problems oddly enough... Requires later investigation
			writer.Write((int)Augments[0]);
			writer.Write((int)AugTiers[0]);
			writer.Write((int)Augments[1]);
			writer.Write((int)AugTiers[1]);
			writer.Write((int)Augments[2]);
			writer.Write((int)AugTiers[2]);
			writer.Write((int)Augments[3]);
			writer.Write((int)AugTiers[3]);
			writer.Write((int)Augments[4]);
			writer.Write((int)AugTiers[4]);
		}

		public override void NetReceive(Item item, BinaryReader reader)
		{
			// First, lists are cleared so they can be refilled with the sent data afterwards
			Augments.Clear();
			AugTiers.Clear();

            // Read the data and put them back into the lists
            // Using for loops causes problems oddly enough... Requires later investigation
            Augments.Add((AugType)reader.ReadInt32());
			AugTiers.Add((AugTier)reader.ReadInt32());
			Augments.Add((AugType)reader.ReadInt32());
			AugTiers.Add((AugTier)reader.ReadInt32());
			Augments.Add((AugType)reader.ReadInt32());
			AugTiers.Add((AugTier)reader.ReadInt32());
			Augments.Add((AugType)reader.ReadInt32());
			AugTiers.Add((AugTier)reader.ReadInt32());
			Augments.Add((AugType)reader.ReadInt32());
			AugTiers.Add((AugTier)reader.ReadInt32());

			CleanLists();
			EnforceCap();
		}

        /// <summary>
        /// Adds 5 'junk' entries to Augments and AugTiers. Used by NetSend in conjunction with EnforceCap to always send 5 bytes of data per list.
        /// </summary>
        private void FillLists()
        {
			for (int i = 0; i < 5; i++)
            {
				Augments.Add(AugType.None);
				AugTiers.Add(AugTier.None);
			}
			EnforceCap();
		}

        /// <summary>
        /// Removes any 'junk' entries from Augments and AugTiers. Used by NetReceive since lists are always sent with 5 entries a piece, often with placeholders.
        /// </summary>
        private void CleanLists()
        {
			Augments.RemoveAll(type => type == AugType.None);
			AugTiers.RemoveAll(tier => tier == AugTier.None);
		}

        /// <summary>
        /// Trims off entries in Augments and AugTiers to ensure they are 5 or less entries long. Important for net-code, which always sends 5 entries per list.
        /// </summary>
        private void EnforceCap()
        {
			while(Augments.Count > 5)
            {
				Augments.RemoveAt(Augments.Count - 1);
            }
			while (AugTiers.Count > 5 || AugTiers.Count > Augments.Count)
			{
				AugTiers.RemoveAt(AugTiers.Count - 1);
			}
		}

		[JITWhenModsEnabled("BSWLmod")]
        /// <summary>
        /// Sets/updates the maximum amount of Augments the item can have. Doesn't remove augments exceeding the limit (Though they will cease granting benefits). Default limit is 1, but it can be increased to 5 via. config. or cross-mod with Block's Weapon Leveling
        /// </summary>
        private void UpdateAugLimit(Item item)
        {
			int baseLimit = ModContent.GetInstance<ConfigServer>().AugmentLimit;
			int crossModLimit = 0;
			if (WeaponAugs.BWLCrossMod.bwlLoaded)
			{
				int wepLevel = CheckBWL(item);
				if(wepLevel >= 100)
                {
					crossModLimit = 5;
				}
				else if(wepLevel >= 75)
                {
					crossModLimit = 4;
				}
				else if(wepLevel >= 50)
                {
					crossModLimit = 3;
				}
				else if(wepLevel >= 25)
                {
					crossModLimit = 2;
				}
			}
			int updatedLimit = Math.Max(baseLimit, crossModLimit);
			
			AugLimit = Math.Min(5, updatedLimit);
			EnforceCap();
		}

		
		[JITWhenModsEnabled("BSWLmod")]
        /// <summary>
        /// Attempts to get the level of the given item in Block's Leveling Mod.
        /// </summary>
        private static int CheckBWL(Item item)
		{
			return BSWLmod.Common.CrossModHelper.GetLevel(item);
		}

        /// <summary>
        /// Effectivley returns the given item's AugLimit. This is used in many places to facilitate preventing Augments exceeding the limit from granting their effects.
        /// </summary>
        public int CheckActiveAugs(Item item)
        {
			UpdateAugLimit(item);
			CleanLists();
			return Math.Min(Augments.Count, AugLimit);
        }

        /// <summary>
        /// Returns if the given item has space for an Augment (I.e. its augment count is less than AugLimit).
        /// </summary>
        public bool CanAddAug(Item item)
        {
			UpdateAugLimit(item);
			return !(Augments.Count + 1 > AugLimit);
		}

        /// <summary>
        /// Attempts to apply an Augment to the item.
        /// </summary>
		/// <param name="augment">The type (AugType) of the Augment.</param>
		/// <param name="tier">The tier (AugTier) of the Augment.</param>
		/// <param name="item">The item getting the Augment. Used to update and check against its AugLimit.</param>
        public void ApplyAugment(AugType augment, AugTier tier, Item item)
        {
			UpdateAugLimit(item);
			if (Augments.Count + 1 > AugLimit)
			{
				return;
			}
			Augments.Add(augment);
			AugTiers.Add(tier);
			UpdateAugLimit(item);
		}

        /// <summary>
        /// Returns if the item has an Augment (For removal).
        /// </summary>
        public bool CanRemoveAug() => Augments.Count > 0;

        /// <summary>
        /// Clears the item's Augments and AugTiers lists, removing all Augments.
        /// </summary>
		/// <param name="removedAugsTiers">The function will fill this list with the tiers of removed Augments. Used by Void Shards to 'salvage' shards from removed Augments.</param>
        public void RemoveAugments(out List<AugTier> removedAugsTiers)
        {
			removedAugsTiers = new List<AugTier>();
			for (int i = 0; i < AugTiers.Count; i++)
			{
				removedAugsTiers.Add(AugTiers[i]);
			}

			if (Augments.Count < 0) 
			{
				return;
			}
			Augments.Clear();
			AugTiers.Clear();
		}

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
			float vigPower = 0;
			float detPower = 0;
			for (int i = 0; i < CheckActiveAugs(item); i++)
            {
                switch (Augments[i])
                {
					case AugType.Might:
                        switch (AugTiers[i])
                        {
							case AugTier.Basic:
								damage += AugPowerArchive.MightBas * 0.01f;
								break;
							case AugTier.Uncommon:
								damage += AugPowerArchive.MightUnc * 0.01f;
								break;
							case AugTier.Rare:
								damage += AugPowerArchive.MightRar * 0.01f;
								break;
							case AugTier.Epic:
								damage += AugPowerArchive.MightEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								damage += AugPowerArchive.MightUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					case AugType.Battlelust:
						if (player.HasBuff(ModContent.BuffType<Battlelust>()))
						{
							switch (AugTiers[i])
							{
								case AugTier.Basic:
									damage += AugPowerArchive.BattlelustBas * 0.01f;
									break;
								case AugTier.Uncommon:
									damage += AugPowerArchive.BattlelustUnc * 0.01f;
									break;
								case AugTier.Rare:
									damage += AugPowerArchive.BattlelustRar * 0.01f;
									break;
								case AugTier.Epic:
									damage += AugPowerArchive.BattlelustEpi * 0.01f;
									break;
								case AugTier.Ultimate:
									damage += AugPowerArchive.BattlelustUlt * 0.01f;
									break;
								default:
									break;
							}
						}
						break;
					case AugType.Megastrike:
						switch (AugTiers[i])
						{
							case AugTier.Uncommon:
								damage += AugPowerArchive.MegastrikeUnc * 0.01f;
								break;
							case AugTier.Rare:
								damage += AugPowerArchive.MegastrikeRar * 0.01f;
								break;
							case AugTier.Epic:
								damage += AugPowerArchive.MegastrikeEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								damage += AugPowerArchive.MegastrikeUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					case AugType.Wildstrike:
						damage *= 0.8f;
						break;
					case AugType.Vigor:
						switch (AugTiers[i])
						{
							case AugTier.Rare:
								vigPower += AugPowerArchive.VigorRar * 0.01f;
								break;
							case AugTier.Epic:
								vigPower += AugPowerArchive.VigorEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								vigPower += AugPowerArchive.VigorUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					case AugType.Determination:
						
						switch (AugTiers[i])
						{
							case AugTier.Rare:
								detPower += AugPowerArchive.DeterminationRar * 0.01f;
								break;
							case AugTier.Epic:
								detPower += AugPowerArchive.DeterminationEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								detPower += AugPowerArchive.DeterminationUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					case AugType.Overdrive:
						switch (AugTiers[i])
						{
							case AugTier.Rare:
								damage += AugPowerArchive.OverdriveEpi * 0.01f;
								break;
							case AugTier.Epic:
								damage += AugPowerArchive.OverdriveRar * 0.01f;
								break;
							case AugTier.Ultimate:
								damage += AugPowerArchive.OverdriveUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					case AugType.Supercharge:
						if (player.HasBuff(ModContent.BuffType<Supercharge>()))
                        {
							switch (AugTiers[i])
							{
								case AugTier.Epic:
									damage += AugPowerArchive.SuperchargeEpi * 0.01f;
									break;
								case AugTier.Ultimate:
									damage += AugPowerArchive.SuperchargeUlt * 0.01f;
									break;
								default:
									break;
							}
						}
						break;
					case AugType.Powertheft:
						if (player.HasBuff(ModContent.BuffType<PowertheftBuff>()))
						{
							damage += 0.2f;
						}
						break;
					default:
						break;
                }
            }
			if (vigPower > 0) // Vigor dmg application
			{
				float hpOffset = player.statLifeMax2 * 0.3f;
				float hpScalar = (player.statLife - hpOffset) / (player.statLifeMax2 - hpOffset);
				hpScalar = Math.Clamp(hpScalar, 0f, 1f);
				if (hpScalar > 0f)
				{
					damage += vigPower * hpScalar;
				}
			}
			if (detPower > 0) // Determination dmg application
			{
				float hpScalar = (float)player.statLife / (float)player.statLifeMax2;
				if (hpScalar < 0.9f)
				{
					if (hpScalar <= 0.2f)
					{
						damage += detPower;
					}
					else
					{
						float remapSclar = Utils.Remap(hpScalar, 0.2f, 0.9f, 0f, 1f);
						damage += detPower * (1f - remapSclar);
					}
				}
			}
		}

        public override float UseSpeedMultiplier(Item item, Player player)
        {
			return AugSpeedMult(item, player); 
		}

        /// <summary>
        /// Extension of UseSpeedMultiplier that handles the effects of Augments.
        /// </summary>
        private float AugSpeedMult(Item item, Player player)
        {
			// Unlike most stats, speed adjustments aren't reflected in the tooltip
			// This hook is independent from UseSpeedMultiplier so it can be called in ModifyTooltips to patch this
			float spdMult = 1f;
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				switch (Augments[i])
				{
					case AugType.Rush:
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								spdMult += AugPowerArchive.RushBas * 0.01f;
								break;
							case AugTier.Uncommon:
								spdMult += AugPowerArchive.RushUnc * 0.01f;
								break;
							case AugTier.Rare:
								spdMult += AugPowerArchive.RushRar * 0.01f;
								break;
							case AugTier.Epic:
								spdMult += AugPowerArchive.RushEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								spdMult += AugPowerArchive.RushUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					case AugType.Frenzy:
						if (player.HasBuff(ModContent.BuffType<Frenzy>()))
						{
							switch (AugTiers[i])
							{
								case AugTier.Basic:
									spdMult += AugPowerArchive.FrenzyBas * 0.01f;
									break;
								case AugTier.Uncommon:
									spdMult += AugPowerArchive.FrenzyUnc * 0.01f;
									break;
								case AugTier.Rare:
									spdMult += AugPowerArchive.FrenzyRar * 0.01f;
									break;
								case AugTier.Epic:
									spdMult += AugPowerArchive.FrenzyEpi * 0.01f;
									break;
								case AugTier.Ultimate:
									spdMult += AugPowerArchive.FrenzyUlt * 0.01f;
									break;
								default:
									break;
							}
						}
						break;
					case AugType.Megastrike:
						spdMult *= 0.8f;
						break;
					case AugType.Wildstrike:
						switch (AugTiers[i])
						{
							case AugTier.Uncommon:
								spdMult += AugPowerArchive.WildstrikeUnc * 0.01f;
								break;
							case AugTier.Rare:
								spdMult += AugPowerArchive.WildstrikeRar * 0.01f;
								break;
							case AugTier.Epic:
								spdMult += AugPowerArchive.WildstrikeEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								spdMult += AugPowerArchive.WildstrikeUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					case AugType.Momentum:
						float maxPow = 0f;
						switch (AugTiers[i])
						{
							case AugTier.Epic:
								maxPow = AugPowerArchive.MomentumEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								maxPow = AugPowerArchive.MomentumUlt * 0.01f;
								break;
							default:
								break;
						}
						GlobalAugPlayer augPlayer = player.GetModPlayer<GlobalAugPlayer>();
						if (augPlayer.momentumStacks > 0)
						{
							spdMult += maxPow * (augPlayer.momentumStacks / 2400f);
						}
						break;
					default:
						break;
				}
			}
			return spdMult;
		}

		public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
		{
			float ogCrit = crit;
			bool voidicCap = false;
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				switch (Augments[i])
				{
					case AugType.Precision:
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								crit += AugPowerArchive.PrecisionBas;
								break;
							case AugTier.Uncommon:
								crit += AugPowerArchive.PrecisionUnc;
								break;
							case AugTier.Rare:
								crit += AugPowerArchive.PrecisionRar;
								break;
							case AugTier.Epic:
								crit += AugPowerArchive.PrecisionEpi;
								break;
							case AugTier.Ultimate:
								crit += AugPowerArchive.PrecisionUlt;
								break;
							default:
								break;
						}
						break;
					case AugType.Unstable:
						switch (AugTiers[i])
						{
							case AugTier.Rare:
								crit += AugPowerArchive.UnstableRar;
								break;
							case AugTier.Epic:
								crit += AugPowerArchive.UnstableEpi;
								break;
							case AugTier.Ultimate:
								crit += AugPowerArchive.UnstableUlt;
								break;
							default:
								break;
						}
						break;
					case AugType.Voidic:
						voidicCap = true;
						break;
					default:
						break;
				}
			}
			if (voidicCap)
			{
				if(crit > 4f)
                {
					crit = 4f;
                }
			}
		}

		public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
        {
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				switch (Augments[i])
				{
					case AugType.Force:
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								knockback += AugPowerArchive.ForceBas * 0.01f;
								break;
							case AugTier.Uncommon:
								knockback += AugPowerArchive.ForceUnc * 0.01f;
								break;
							case AugTier.Rare:
								knockback += AugPowerArchive.ForceRar * 0.01f;
								break;
							case AugTier.Epic:
								knockback += AugPowerArchive.ForceEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								knockback += AugPowerArchive.ForceUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
		}

        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
			mult *= AugManaMult(item);
		}

        /// <summary>
        /// Extension of ModifyManaCost that handles the effects of Augments.
        /// </summary>
        private float AugManaMult(Item item)
        {
			// Like use speed, mana adjustments aren't reflected in the tooltip
			// This hook is independent from ModifyManaCost so it can be called in ModifyTooltips to patch this
			float manaMult = 1f;
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				switch (Augments[i])
				{
					case AugType.Arcana:
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								manaMult *= 1f - AugPowerArchive.ArcanaBas * 0.01f;
								break;
							case AugTier.Uncommon:
								manaMult *= 1f - AugPowerArchive.ArcanaUnc * 0.01f;
								break;
							case AugTier.Rare:
								manaMult *= 1f - AugPowerArchive.ArcanaRar * 0.01f;
								break;
							case AugTier.Epic:
								manaMult *= 1f - AugPowerArchive.ArcanaEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								manaMult *= 1f - AugPowerArchive.ArcanaUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
			if (manaMult <= 0f)
			{
				manaMult = 0.01f;
			}
			return manaMult;
		}

        public override void ModifyItemScale(Item item, Player player, ref float scale)
        {
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				switch (Augments[i])
				{
					case AugType.Titanreach:
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								scale += AugPowerArchive.TitanreachBas * 0.01f;
								break;
							case AugTier.Uncommon:
								scale += AugPowerArchive.TitanreachUnc * 0.01f;
								break;
							case AugTier.Rare:
								scale += AugPowerArchive.TitanreachRar * 0.01f;
								break;
							case AugTier.Epic:
								scale += AugPowerArchive.TitanreachEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								scale += AugPowerArchive.TitanreachUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
		}

        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
			float consumptionChance = 1f;
			for (int i = 0; i < CheckActiveAugs(weapon); i++)
			{
				switch (Augments[i])
				{
					case AugType.Conservation:
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								consumptionChance *= (1f - AugPowerArchive.ConservationBas * 0.01f);
								break;
							case AugTier.Uncommon:
								consumptionChance *= (1f - AugPowerArchive.ConservationUnc * 0.01f);
								break;
							case AugTier.Rare:
								consumptionChance *= (1f - AugPowerArchive.ConservationRar * 0.01f);
								break;
							case AugTier.Epic:
								consumptionChance *= (1f - AugPowerArchive.ConservationEpi * 0.01f);
								break;
							case AugTier.Ultimate:
								consumptionChance *= (1f - AugPowerArchive.ConservationUlt * 0.01f);
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
			return Main.rand.NextFloat() < consumptionChance;
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			float velocityMult = 1f;
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				switch (Augments[i])
				{
					case AugType.Ballistic:
						switch (AugTiers[i])
						{
							case AugTier.Basic:
								velocityMult += AugPowerArchive.BallisticBas * 0.01f;
								break;
							case AugTier.Uncommon:
								velocityMult += AugPowerArchive.BallisticUnc * 0.01f;
								break;
							case AugTier.Rare:
								velocityMult += AugPowerArchive.BallisticRar * 0.01f;
								break;
							case AugTier.Epic:
								velocityMult += AugPowerArchive.BallisticEpi * 0.01f;
								break;
							case AugTier.Ultimate:
								velocityMult += AugPowerArchive.BallisticUlt * 0.01f;
								break;
							default:
								break;
						}
						break;
					default:
						break;
				}
			}
			velocity *= velocityMult;
		}

		public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			List<AugType> augments = new List<AugType>();
			List<AugTier> augTiers = new List<AugTier>();
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				augments.Add(Augments[i]);
				augTiers.Add(AugTiers[i]);
			}
			int dmg = player.GetWeaponDamage(item);
			int atkSpd = (int)(item.useTime * player.GetWeaponAttackSpeed(item));
			AugmentHelpers.CombinedAugModifyHit(player, target, ref modifiers, dmg, atkSpd, augments, augTiers, true);
		}

		public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			List<AugType> augments = new List<AugType>();
			List<AugTier> augTiers = new List<AugTier>();
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				augments.Add(Augments[i]);
				augTiers.Add(AugTiers[i]);
			}
			int atkSpd = (int)(item.useTime * player.GetWeaponAttackSpeed(item));
			AugmentHelpers.CombinedAugOnHit(player.GetSource_ItemUse(item), player, target, hit, damageDone, atkSpd, augments, augTiers);
		}

        public override void UseAnimation(Item item, Player player)
        {
			float overheatCount = (float)(item.useTime * player.GetWeaponAttackSpeed(item));
			bool triggerOverheat = false;
			int barrageDmg = player.GetWeaponDamage(item);
			int ai0 = -1;
			for (int i = 0; i < CheckActiveAugs(item); i++)
			{
				switch (Augments[i])
				{
					case AugType.Barrage:
						switch (AugTiers[i])
						{
							case AugTier.Uncommon:
								barrageDmg = (int)(barrageDmg * AugPowerArchive.BarrageUnc * 0.01f);
								ai0 = 1;
								break;
							case AugTier.Rare:
								barrageDmg = (int)(barrageDmg * AugPowerArchive.BarrageRar * 0.01f);
								ai0 = 2;
								break;
							case AugTier.Epic:
								barrageDmg = (int)(barrageDmg * AugPowerArchive.BarrageEpi * 0.01f);
								ai0 = 3;
								break;
							case AugTier.Ultimate:
								barrageDmg = (int)(barrageDmg * AugPowerArchive.BarrageUlt * 0.01f);
								ai0 = 4;
								break;
							default:
								return;
						}
						break;
					case AugType.Overdrive:
						triggerOverheat = true;
						break;
					default:
						break;
				}
			}
			if (ai0 != -1)
			{
				barrageCounter++;
				if (barrageCounter >= 5 && player == Main.LocalPlayer)
				{
					barrageCounter = 0;
					float kb = player.GetWeaponKnockback(item);
					Vector2 aimVelocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero) * 16f;
					Projectile.NewProjectile(player.GetSource_ItemUse(item), player.Center, aimVelocity, ModContent.ProjectileType<BarrageProj>(),
								barrageDmg, kb, player.whoAmI, ai0);
				}
			}
			if (triggerOverheat)
			{
				GlobalAugPlayer augPlayer = player.GetModPlayer<GlobalAugPlayer>();
				augPlayer.overheatStacks += overheatCount;
			}
		}

        public override void MeleeEffects(Item item, Player player, Rectangle hitbox)
        {
			// Generates dust as a visual effect/indicator for Augmented weapons
			if (!ModContent.GetInstance<ConfigClient>().AugmentDust)
			{
				return;
			}
			if (Augments.Count > 0)
			{
				List<Color> dustColors = new List<Color>();
				for (int i = 0; i < CheckActiveAugs(item); i++)
				{
					if (ShoulAugMakeDust(Augments[i]))
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
				if (Main.rand.NextBool(4))
				{
					int dustIndex = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y),
										 hitbox.Width, hitbox.Height,
										 DustID.RainbowMk2,
										 player.velocity.X * 0.2f, player.velocity.Y * 0.2f,
										 newColor: dustColor,
										 Scale: 0.8f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity *= 0.7f;
                    Main.dust[dustIndex].noLightEmittence = true;
                }
			}
		}

        /// <summary>
        /// Returns if a weapon's item/melee hitbox should create dust when used, based on the given Augment (AugType).
        /// </summary>
        private static bool ShoulAugMakeDust(AugType augment)
		{
			AugType[] excludedAugs = new AugType[]
			{
				AugType.Arcana,
				AugType.Conservation,
				AugType.Lightweight,
				AugType.Revitalize,
				AugType.Sturdy,
				AugType.Barrage,
				AugType.Uplifting,
				AugType.Luck,
				AugType.Superluck,
			};
			foreach (AugType excludedAug in excludedAugs)
			{
				if (excludedAug == augment)
				{
					return false;
				}
			}
			return true;
		}

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if(Augments.Count <= 0)
            {
				return;
            }
			
			// Use speed and mana cost don't properly reflect their values when modified, so it is manually patched here
			if(AugSpeedMult(item, Main.LocalPlayer) != 1f && ModContent.GetInstance<ConfigServer>().PatchTooltips)
            {
				TooltipLine target = tooltips.FirstOrDefault(line => line.Mod == "Terraria" && line.Name == "Speed", default);
				// Make sure line exsists
				if (target.Equals(null) || target.Equals(default(TooltipLine)))
				{
					return;
				}
				int index = tooltips.IndexOf(target);
				if (index == -1)
				{
					return;
				}

				int trueUseTime = (int)(item.useAnimation * (1f / AugSpeedMult(item, Main.LocalPlayer))); // Applies speed mod
				string newLine;
				// Gets the corrected line the same way vanilla does
				if (trueUseTime <= 8)
				{
					newLine = Lang.tip[6].Value;
				}
				else if (trueUseTime <= 20)
				{
					newLine = Lang.tip[7].Value;
				}
				else if (trueUseTime <= 25)
				{
					newLine = Lang.tip[8].Value;
				}
				else if (trueUseTime <= 30)
				{
					newLine = Lang.tip[9].Value;
				}
				else if (trueUseTime <= 35)
				{
					newLine = Lang.tip[10].Value;
				}
				else if (trueUseTime <= 45)
				{
					newLine = Lang.tip[11].Value;
				}
				else if (trueUseTime <= 55)
				{
					newLine = Lang.tip[12].Value;
				}
				else
				{
					newLine = Lang.tip[13].Value;
				}
				tooltips.Insert(index, new TooltipLine(Mod, "patchedSpeed", newLine));
				tooltips.Remove(target); // Removes vanilla line
			}
			if(AugManaMult(item) != 1f && ModContent.GetInstance<ConfigServer>().PatchTooltips)
            {
				TooltipLine target = tooltips.FirstOrDefault(line => line.Mod == "Terraria" && line.Name == "UseMana", default);
				// Make sure line exsists
				if (target.Equals(null) || target.Equals(default(TooltipLine)))
				{
					return;
				}
				int index = tooltips.IndexOf(target);
				if (index == -1)
				{
					return;
				}

				int trueManaCost = (int)(item.mana * Main.player[Main.myPlayer].manaCost * AugManaMult(item));
				string newLine = Language.GetTextValue("CommonItemTooltip.UsesMana", trueManaCost);
				tooltips.Insert(index, new TooltipLine(Mod, "patchedUseMana", newLine));
				tooltips.Remove(target); // Removes vanilla line
			}

			string titleLine = Main.keyState.PressingShift() ? 
				Language.GetTextValue("Mods.WeaponAugs.CommonItemTooltip.AugListTitle") : 
				Language.GetTextValue("Mods.WeaponAugs.CommonItemTooltip.AugListShiftTip");
			tooltips.Add(new TooltipLine(Mod, "AugListTitle", titleLine));
			if(Augments.Count > AugTiers.Count)
            {
				tooltips.Add(new TooltipLine(Mod, "AugListError", 
					Language.GetTextValue("Mods.WeaponAugs.CommonItemTooltip.AugListError")) { OverrideColor = Color.Red });
				return;
			}
			for(int i = 0; i < Augments.Count; i++)
            {
                if (Augments[i] != AugType.None && Augments[i] != AugType.Count)
                {
					if(TryGetAugTooltips(Augments[i], AugTiers[i], out string augName, out string augDesc))
                    {
						Color tooltipColor = GetAugTooltipColor(AugTiers[i]);
						string augmentLine;

						augmentLine = Main.keyState.PressingShift() ? augDesc : augName;

						// Inactive augments
						if (i >= CheckActiveAugs(item))
						{
                            augmentLine = Main.keyState.PressingShift() ?
                                augName + Language.GetTextValue("Mods.WeaponAugs.CommonItemTooltip.AugListDisabledDesc") :
                                augName + Language.GetTextValue("Mods.WeaponAugs.CommonItemTooltip.AugListDisabledTitle");
							tooltipColor = Colors.RarityTrash;
						}

						string lineName = $"Augment{i}";
						tooltips.Add(new TooltipLine(Mod, lineName, augmentLine) { OverrideColor = tooltipColor });
					}
				}
            }
		}

        /// <summary>
        /// Returns a color - for use in tooltips - based on the given Augment Tier (AugTier).
        /// </summary>
        private static Color GetAugTooltipColor(AugTier tier)
		{
            switch (tier)
            {
				case AugTier.Basic:
					return Colors.RarityNormal;
				case AugTier.Uncommon:
					return Colors.RarityOrange;
				case AugTier.Rare:
					return Colors.RarityLime;
				case AugTier.Epic:
					return Colors.RarityCyan;
				case AugTier.Ultimate:
					return Colors.RarityDarkPurple;
				default:
					return Colors.RarityTrash;
			}
        }

        /// <summary>
        /// Fetches augment names and descriptions for tooltips.
        /// </summary>
		/// <param name="augment">The type (AugType) of the Augment.</param>
		/// <param name="tier">The tier (AugTier) of the Augment.</param>
		/// <param name="augTitle">The displayed name of the Augment, with tier.</param>
		/// <param name="augDesc">The displayed description of the Augment, accounting for tier.</param>
        private static bool TryGetAugTooltips(AugType augment, AugTier tier, out string augTitle, out string augDesc)
        {
			string augTier = "";
			string augName = "";
			float placeholderVal = -1;
			string pValOverride = "";
			string localizationPath = "Mods.WeaponAugs.CommonItemTooltip.AugDesc";
			bool noneTypeError = false;
			LocalizedText augLocalDesc;
			augDesc = "";
			augTitle = "";
			// Use a smaller switch for augment tier fitst to slightly reduce clutter in the main switch
			switch (tier)
			{
				case AugTier.Basic:
					augTier = " (Basic)";
					break;
				case AugTier.Uncommon:
					augTier = " (Uncommon)";
					break;
				case AugTier.Rare:
					augTier = " (Rare)";
					break;
				case AugTier.Epic:
					augTier = " (Epic)";
					break;
				case AugTier.Ultimate:
					augTier = " (Ultimate)";
					break;
				default:
					noneTypeError = true;
					break;
			}
			// Massive, nested switch fetches Augment name and description based on the type and tier
            switch (augment)
            {
				// Types 1-20 have all 5 tiers
				case AugType.Might:
					augName = "Might";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.MightBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.MightUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.MightRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.MightEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.MightUlt;
							break;
					}
					break;
				case AugType.Force:
					augName = "Force";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.ForceBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.ForceUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.ForceRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.ForceEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.ForceUlt;
							break;
					}
					break;
				case AugType.Precision:
					augName = "Precision";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.PrecisionBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.PrecisionUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.PrecisionRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.PrecisionEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.PrecisionUlt;
							break;
					}
					break;
				case AugType.Rush:
					augName = "Rush";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.RushBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.RushUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.RushRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.RushEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.RushUlt;
							break;
					}
					break;
				case AugType.Armorbane:
					augName = "Armorbane";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.ArmorbaneBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.ArmorbaneUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.ArmorbaneRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.ArmorbaneEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.ArmorbaneUlt;
							break;
					}
					break;
				case AugType.Titanreach:
					augName = "Titanreach";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.TitanreachBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.TitanreachUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.TitanreachRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.TitanreachEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.TitanreachUlt;
							break;
					}
					break;
				case AugType.Dire:
					augName = "Dire";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.DireBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.DireUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.DireRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.DireEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.DireUlt;
							break;
					}
					break;
				case AugType.Heartsurge:
					augName = "Heartsurge";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.HeartsurgeBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.HeartsurgeUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.HeartsurgeRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.HeartsurgeEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.HeartsurgeUlt;
							break;
					}
					break;
				case AugType.Arcana:
					augName = "Arcana";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.ArcanaBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.ArcanaUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.ArcanaRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.ArcanaEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.ArcanaUlt;
							break;
					}
					break;
				case AugType.Frenzy:
					augName = "Frenzy";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.FrenzyBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.FrenzyUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.FrenzyRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.FrenzyEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.FrenzyUlt;
							break;
					}
					break;
				case AugType.Battlelust:
					augName = "Battlelust";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.BattlelustBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.BattlelustUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.BattlelustRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.BattlelustEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.BattlelustUlt;
							break;
					}
					break;
				case AugType.Conservation:
					augName = "Conservation";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.ConservationBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.ConservationUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.ConservationRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.ConservationEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.ConservationUlt;
							break;
					}
					break;
				case AugType.Overflow:
					augName = "Overflow";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.OverflowBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.OverflowUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.OverflowRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.OverflowEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.OverflowUlt;
							break;
					}
					break;
				case AugType.Fortune:
					augName = "Fortune";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.FortuneBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.FortuneUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.FortuneRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.FortuneEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.FortuneUlt;
							break;
					}
					break;
				case AugType.Ignite:
					augName = "Ignite";
					switch (tier)
					{
						default:
							pValOverride = Language.GetTextValue("BuffName.OnFire");
							break;
						case AugTier.Uncommon:
							pValOverride = Language.GetTextValue("BuffName.Frostburn");
							break;
						case AugTier.Rare:
							pValOverride = Language.GetTextValue("BuffName.Shadowflame");
							break;
						case AugTier.Epic:
							pValOverride = Language.GetTextValue("BuffName.CursedInferno");
							break;
						case AugTier.Ultimate:
							pValOverride = Language.GetTextValue("Mods.WeaponAugs.Buffs.RunicBlaze.DisplayName");
							break;
					}
					break;
				case AugType.Taint:
					augName = "Taint";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.TaintBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.TaintUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.TaintRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.TaintEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.TaintUlt;
							break;
					}
					break;
				case AugType.Ballistic:
					augName = "Ballistic";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.BallisticBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.BallisticUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.BallisticRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.BallisticEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.BallisticUlt;
							break;
					}
					break;
				case AugType.Finisher:
					augName = "Finisher";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.FinisherBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.FinisherUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.FinisherRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.FinisherEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.FinisherUlt;
							break;
					}
					break;
				case AugType.Unleash:
					augName = "Unleash";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.UnleashBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.UnleashUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.UnleashRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.UnleashEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.UnleashUlt;
							break;
					}
					break;
				case AugType.Combobreak:
					augName = "Combobreak";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.CombobreakBas;
							break;
						case AugTier.Uncommon:
							placeholderVal = AugPowerArchive.CombobreakUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.CombobreakRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.CombobreakEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.CombobreakUlt;
							break;
					}
					break;
				// 21-36 have all tiers but Basic
				case AugType.Deathecho:
					augName = "Deathecho";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.DeathechoUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.DeathechoRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.DeathechoEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.DeathechoUlt;
							break;
					}
					break;
				case AugType.Lightweight:
					augName = "Lightweight";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.LightweightUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.LightweightRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.LightweightEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.LightweightUlt;
							break;
					}
					break;
				case AugType.Revitalize:
					augName = "Revitalize";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.RevitalizeUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.RevitalizeRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.RevitalizeEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.RevitalizeUlt;
							break;
					}
					break;
				case AugType.Megastrike:
					augName = "Megastrike";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.MegastrikeUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.MegastrikeRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.MegastrikeEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.MegastrikeUlt;
							break;
					}
					break;
				case AugType.Sturdy:
					augName = "Sturdy";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.SturdyUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.SturdyRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.SturdyEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.SturdyUlt;
							break;
					}
					break;
				case AugType.Barrage:
					augName = "Barrage";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.BarrageUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.BarrageRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.BarrageEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.BarrageUlt;
							break;
					}
					break;
				case AugType.Diversion:
					augName = "Diversion";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.DiversionUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.DiversionRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.DiversionEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.DiversionUlt;
							break;
					}
					break;
				case AugType.Wildstrike:
					augName = "Wildstrike";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.WildstrikeUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.WildstrikeRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.WildstrikeEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.WildstrikeUlt;
							break;
					}
					break;
				case AugType.Uplifting:
					augName = "Uplifting";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.UpliftingUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.UpliftingRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.UpliftingEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.UpliftingUlt;
							break;
					}
					break;
				case AugType.Luck:
					augName = "Luck";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.LuckUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.LuckRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.LuckEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.LuckUlt;
							break;
					}
					break;
				case AugType.Rend:
					augName = "Rend";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.RendUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.RendRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.RendEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.RendUlt;
							break;
					}
					break;
				case AugType.Deathshroud:
					augName = "Deathshroud";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.DeathshroudUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.DeathshroudRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.DeathshroudEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.DeathshroudUlt;
							break;
					}
					break;
				case AugType.Ward:
					augName = "Ward";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.WardUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.WardRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.WardEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.WardUlt;
							break;
					}
					break;
				case AugType.Minicrit:
					augName = "Minicrit";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.MinicritUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.MinicritRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.MinicritEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.MinicritUlt;
							break;
					}
					break;
				case AugType.Siphon:
					augName = "Siphon";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.SiphonUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.SiphonRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.SiphonEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.SiphonUlt;
							break;
					}
					break;
				case AugType.Paincycle:
					augName = "Paincycle";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.PaincycleUnc;
							break;
						case AugTier.Rare:
							placeholderVal = AugPowerArchive.PaincycleRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.PaincycleEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.PaincycleUlt;
							break;
					}
					break;
				// 37-48 have Rare, Epic, and Ultimate tiers
				case AugType.Rally:
					augName = "Rally";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.RallyRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.RallyEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.RallyUlt;
							break;
					}
					break;
				case AugType.Blast:
					augName = "Blast";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.BlastEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.BlastUlt;
							break;
					}
					break;
				case AugType.Superluck:
					augName = "Superluck";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.SuperluckRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.SuperluckEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.SuperluckUlt;
							break;
					}
					break;
				case AugType.Kingslayer:
					augName = "Kingslayer";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.KingslayerRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.KingslayerEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.KingslayerUlt;
							break;
					}
					break;
				case AugType.Hypercrit:
					augName = "Hypercrit";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.HypercritRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.HypercritEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.HypercritUlt;
							break;
					}
					break;
				case AugType.Unstable:
					augName = "Unstable";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.UnstableRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.UnstableEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.UnstableUlt;
							break;
					}
					break;
				case AugType.Vigor:
					augName = "Vigor";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.VigorRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.VigorEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.VigorUlt;
							break;
					}
					break;
				case AugType.Determination:
					augName = "Determination";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.DeterminationRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.DeterminationEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.DeterminationUlt;
							break;
					}
					break;
				case AugType.Breaker:
					augName = "Breaker";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.BreakerRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.BreakerEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.BreakerUlt;
							break;
					}
					break;
				case AugType.Reaping:
					augName = "Reaping";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.ReapingRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.ReapingEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.ReapingUlt;
							break;
					}
					break;
				case AugType.Overdrive:
					augName = "Overdrive";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.OverdriveRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.OverdriveEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.OverdriveUlt;
							break;
					}
					break;
				case AugType.Ultracutter:
					augName = "Ultracutter";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.UltracutterRar;
							break;
						case AugTier.Epic:
							placeholderVal = AugPowerArchive.UltracutterEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.UltracutterUlt;
							break;
					}
					break;
				// 49-56 have only Epic and Ultimate tiers
				case AugType.Voidic:
					augName = "Voidic";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.VoidicEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.VoidicUlt;
							break;
					}
					break;
				case AugType.Committed:
					augName = "Committed";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.CommittedEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.CommittedUlt;
							break;
					}
					break;
				case AugType.Lifeleech:
					augName = "Lifeleech";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.LifeleechEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.LifeleechUlt;
							break;
					}
					break;
				case AugType.Radiance:
					augName = "Radiance";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.RadianceEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.RadianceUlt;
							break;
					}
					break;
				case AugType.Relentless:
					augName = "Relentless";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.RelentlessEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.RelentlessUlt;
							break;
					}
					break;
				case AugType.Momentum:
					augName = "Momentum";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.MomentumEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.MomentumUlt;
							break;
					}
					break;
				case AugType.Collateral:
					augName = "Collateral";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.CollateralEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.CollateralUlt;
							break;
					}
					break;
				case AugType.Supercharge:
					augName = "Supercharge";
					switch (tier)
					{
						default:
							placeholderVal = AugPowerArchive.SuperchargeEpi;
							break;
						case AugTier.Ultimate:
							placeholderVal = AugPowerArchive.SuperchargeUlt;
							break;
					}
					break;
				// 57-60 are Ultimate exclusive
				case AugType.Execution:
					augName = "Execution";
					placeholderVal = AugPowerArchive.ExecutionUlt;
					break;
				case AugType.Resurgence:
					augName = "Resurgence";
					placeholderVal = AugPowerArchive.ResurgenceUlt;
					break;
				case AugType.Runic:
					augName = "Runic";
					placeholderVal = AugPowerArchive.RunicUlt;
					break;
				case AugType.Powertheft:
					augName = "Powertheft";
					placeholderVal = AugPowerArchive.PowertheftUlt;
					break;
				case AugType.None:
					noneTypeError = true;
					break;
				default:
					return false;
			}
            if (noneTypeError)
            {
				augTitle = "None?";
				augDesc = "None: An error seems to have occured; this Augment slot is filled with junk data?";
				return true;
            }

			augTitle = augName + augTier;
			localizationPath += augName;
			if (localizationPath == "Mods.WeaponAugs.CommonItemTooltip.AugDesc")
            {
				return false;
            }
			augLocalDesc = Language.GetOrRegister(localizationPath);
			if(pValOverride != "")
            {
				augDesc = augLocalDesc.Format(augTitle, pValOverride);
				return true;
			}
			augDesc = augLocalDesc.Format(augTitle, placeholderVal);
			return true;
		}
    }
}
 
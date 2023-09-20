using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.AugJewels
{
	// All Augment Jewels inherit from this class, greatly reducing boilerplate code per ModItem class
	// Heavily commented for future reference and example purposes
	public abstract class AugmentJewel : ModItem
	{
		// The Augment type and tier the Augment Jewel will apply
		public abstract AugType Augment { get; }
		public abstract AugTier Tier { get; }

		// Determines if the Augment Jewel can be applied
		public sealed override bool CanRightClick()
		{
			Item wep = Main.LocalPlayer.HeldItem; // Get the currently held item
			if (wep != null) // Is the player holding something?
			{
				if (wep.damage > 0 && wep.consumable == false && wep.ammo == AmmoID.None && wep.accessory == false && CheckRestrictions(Augment, wep)) // Is the item a non-consumable weapon and not ammo?
				{
					if(wep.TryGetGlobalItem(out GlobalAugWep globalWep)) // Try to get the associated global item to send the augment to
                    {
						return globalWep.CanAddAug(wep);
                    }
				}
			}
			return false;
		}

		// Applies the Augment
		public override void RightClick(Player player)
		{
			Item wep = Main.LocalPlayer.HeldItem; // Get the currently held item
			if (wep.TryGetGlobalItem(out GlobalAugWep globalWep))
			{
				globalWep.ApplyAugment(Augment, Tier, wep);
				SoundEngine.PlaySound(SoundID.Item4, player.position);
			}
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255);
        }

        // Adds an extra tooltip to convey the gear that will receive Augmentation
        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Item heldItem = Main.LocalPlayer.HeldItem;
			if (heldItem != null)
			{
				if (CanRightClick())
				{
					Color textColor = GetRarityColor(heldItem);
					tooltips.Add(new TooltipLine(Mod, "EffectTarget", Language.GetTextValue("Mods.WeaponAugs.CommonItemTooltip.Selected") +
								$"{heldItem.HoverName}")
					{ OverrideColor = textColor });
				}
			}
		}

		// Used to color the selected gear tooltip line added above
		public Color GetRarityColor(Item item)
		{
			Color color;
			switch (item.rare)
			{
				case -11:
					color = Colors.RarityAmber;
					break;
				case -1:
					color = Colors.RarityTrash;
					break;
				case 0:
					color = Colors.RarityNormal;
					break;
				case 1:
					color = Colors.RarityBlue;
					break;
				case 2:
					color = Colors.RarityGreen;
					break;
				case 3:
					color = Colors.RarityOrange;
					break;
				case 4:
					color = Colors.RarityRed;
					break;
				case 5:
					color = Colors.RarityPink;
					break;
				case 6:
					color = Colors.RarityPurple;
					break;
				case 7:
					color = Colors.RarityLime;
					break;
				case 8:
					color = Colors.RarityYellow;
					break;
				case 9:
					color = Colors.RarityCyan;
					break;
				case 10:
					color = Colors.RarityDarkRed;
					break;
				case 11:
					color = Colors.RarityDarkPurple;
					break;
				default:
					color = Colors.RarityNormal;
					break;
			}
			if (item.master)
			{
				color = new Color(255, (byte)(Main.masterColor * 200f), 0, Main.mouseTextColor);
			}
			else if (item.expert)
			{
				color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
			}
			return color;
		}

		public bool CheckRestrictions(AugType augment, Item item)
        {
			Projectile projMimic;

			// Many Augments have restrictions on which weapons they work with, as managed here
            // Each set of cases is labeled with the respective restriction(s)
			switch (augment)
            {
				case AugType.Rush:
					// Can modify attack speed? 
					// {$CommonItemTooltip.RestrictionAtkSpd}
					if (!item.CanApplyPrefix(PrefixID.Sluggish))
					{
						return false;
					}
					break;
				case AugType.Titanreach:
					// Can modify scale? (Whips and melee) 
					// {$CommonItemTooltip.RestrictionScale}
					if (!item.CanApplyPrefix(PrefixID.Massive))
					{
						return false;
					}
					break;
				case AugType.Arcana:
				case AugType.Siphon:
					// Magic damage? 
					// {$CommonItemTooltip.RestrictionMagic}
					if (item.DamageType != DamageClass.Magic)
                    {
						return false;
                    }
					break;
				case AugType.Ballistic:
					// Ranged damage? 
					// {$CommonItemTooltip.RestrictionRanged}
					if (item.DamageType != DamageClass.Ranged)
					{
						return false;
					}
					break;
				case AugType.Conservation:
					// Uses ammo? 
					// {$CommonItemTooltip.RestrictionAmmo}
					if (item.useAmmo == AmmoID.None)
					{
						return false;
					}
					break;
				case AugType.Force:
					// Has knockback? 
					// {$CommonItemTooltip.RestrictionKB}
					if (item.knockBack == 0)
                    {
						return false;
                    }
					break;
				case AugType.Ultracutter:
					// True Melee hitbox/attack? 
					// {$CommonItemTooltip.RestrictionTrueMelee}
					if (item.noMelee)
					{
						return false;
					}
					break;
				case AugType.Momentum:
				case AugType.Megastrike:
				case AugType.Wildstrike:
				case AugType.Frenzy:
					// Can modify attack speed? 
					// Is NOT a minion or sentry staff?
					// {$CommonItemTooltip.RestrictionAtkSpd}
					// {$CommonItemTooltip.RestrictionNoMinion}

					// Attack speed check
					if (!item.CanApplyPrefix(PrefixID.Sluggish))
					{
						return false;
					}

					// Minion/sentry check
					projMimic = new Projectile();
					projMimic.SetDefaults(item.shoot);
					if (projMimic.minion || projMimic.sentry)
					{
						return false;
					}
					break;
				case AugType.Precision:
				case AugType.Dire:
				case AugType.Minicrit:
				case AugType.Hypercrit:
				case AugType.Unstable:
					// Is NOT summon damage? 
					// {$CommonItemTooltip.RestrictionNoSummon}
					if (item.DamageType == DamageClass.Summon || item.DamageType == DamageClass.SummonMeleeSpeed)
					{
						return false;
					}
					break;
				case AugType.Unleash:
				case AugType.Rally:
				case AugType.Vigor:
				case AugType.Determination:
				case AugType.Overdrive:
				case AugType.Committed:
				case AugType.Supercharge:
				case AugType.Runic:
				case AugType.Powertheft:
				case AugType.Battlelust:
					// Is NOT a minion or sentry staff? 
					// {$CommonItemTooltip.RestrictionNoMinion}
					projMimic = new Projectile();
					projMimic.SetDefaults(item.shoot);
					if (projMimic.minion || projMimic.sentry)
					{
						return false;
					}
					break;
				default:
					// Other types have no restriction
					break;
            }
			return true;
        }
	}
}
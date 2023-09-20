using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.Others
{
	public class VoidShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.ShardVoid;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerShardBas>());
			Item.rare = ItemRarityID.LightPurple;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override bool CanRightClick()
		{
			Item wep = Main.LocalPlayer.HeldItem; // Get the currently held item
			if (wep != null) // Is the player holding something?
			{
				if (wep.damage > 0 && wep.consumable == false && wep.ammo == AmmoID.None && wep.accessory == false) // Is the item a non-consumable weapon and not ammo?
				{
					if (wep.TryGetGlobalItem(out GlobalAugWep globalWep))
					{
						return globalWep.CanRemoveAug();
					}
				}
			}
			return false;
		}

		public override void RightClick(Player player)
		{
			Item wep = Main.LocalPlayer.HeldItem; // Get the currently held item
			if (wep.TryGetGlobalItem(out GlobalAugWep globalWep))
			{
				// Clears the augment list while bringing back the tiers of the discards Augments
				globalWep.RemoveAugments(out List<AugTier> salvageTiers);
				// See what tiers were removed, returning 0-3 Shard of Power of the same tier for each Augment
				for (int i = 0; i < salvageTiers.Count; i++)
                {
					int itemType = -1;
					switch (salvageTiers[i])
                    {
						case AugTier.Basic:
							itemType = ModContent.ItemType<PowerShardBas>();
							break;
						case AugTier.Uncommon:
							itemType = ModContent.ItemType<PowerShardUnc>();
							break;
						case AugTier.Rare:
							itemType = ModContent.ItemType<PowerShardRar>();
							break;
						case AugTier.Epic:
							itemType = ModContent.ItemType<PowerShardEpi>();
							break;
						case AugTier.Ultimate:
							itemType = ModContent.ItemType<PowerShardUlt>();
							break;
						default:
							break;
					}
					if(itemType != -1)
                    {
						int dropNum = Main.rand.Next(4);
						if(dropNum > 0)
                        {
							player.QuickSpawnItem(player.GetSource_ItemUse(Item), itemType, dropNum);
						}
                    }
                }
				SoundEngine.PlaySound(SoundID.Item113, player.position);
			}
		}

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
	}
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerShardEpi : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.ShardEpi;
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PowerShardRar>();
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerShardBas>());
			Item.rare = ItemRarityID.Cyan;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PowerShardRar>(3)
				.AddTile(TileID.AdamantiteForge)
				.Register();
		}
	}
}
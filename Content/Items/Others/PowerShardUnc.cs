using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerShardUnc : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.ShardUnc;
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PowerShardBas>();
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerShardBas>());
			Item.rare = ItemRarityID.Orange;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PowerShardBas>(3)
				.AddTile(TileID.Furnaces)
				.Register();
		}
	}
}
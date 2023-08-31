using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerShardRar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.ShardRar;
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PowerShardUnc>();
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerShardBas>());
			Item.rare = ItemRarityID.Lime;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PowerShardUnc>(3)
				.AddTile(TileID.Hellforge)
				.Register();
		}
	}
}
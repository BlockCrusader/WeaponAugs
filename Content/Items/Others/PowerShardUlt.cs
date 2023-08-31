using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Items.Others
{
	public class PowerShardUlt : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = JourneyUnlocksArchive.ShardUlt;
			ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<PowerShardEpi>();
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ModContent.ItemType<PowerShardBas>());
			Item.rare = ItemRarityID.Purple;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255, 255, 255, 255);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<PowerShardEpi>(3)
				.AddTile(TileID.AdamantiteForge)
				.Register();
		}
	}
}
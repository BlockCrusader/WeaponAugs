using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using WeaponAugs.Common;
using WeaponAugs.Content.Buffs;
using Terraria.Audio;

namespace WeaponAugs.Content.Items.AugJewels.Ultimate
{
	public class RunicRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 0;
			ItemID.Sets.ItemIconPulse[Item.type] = true; 
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.rare = 0;
			Item.value = Item.buyPrice(0, 0, 0, 0);
			Item.noGrabDelay = 0;
		}

		private int lifeTime;
        public override void Update(ref float gravity, ref float maxFallSpeed) // Kills the item after it spends 6 seconds alive
        {
			lifeTime++;
			if(lifeTime > 360)
            {
				Item.active = false;
				Item.TurnToAir();
            }
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
			grabRange = 320; // Manually set base range to ~20 tiles
            if (player.treasureMagnet) // +10 tiles
            {
				grabRange += 160;
            }
		}

        public override bool OnPickup(Player player)
        {
			int buffChoice = Main.rand.Next(4);
            switch (buffChoice)
            {
				case 0:
					player.AddBuff(ModContent.BuffType<RunicForce>(), 600);
					break;
				case 1:
					player.AddBuff(ModContent.BuffType<RunicMight>(), 600);
					break;
				case 2:
					player.AddBuff(ModContent.BuffType<RunicPrecision>(), 600);
					break;
				case 3:
					player.AddBuff(ModContent.BuffType<RunicRush>(), 600);
					break;
				default:
					break;
			}
			SoundEngine.PlaySound(SoundID.Item4, player.Center);
            return false;
        }
	}
}

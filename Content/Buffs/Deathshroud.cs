using Terraria;
using Terraria.ModLoader;
using WeaponAugs.Common;

namespace WeaponAugs.Content.Buffs
{
	public class Deathshroud : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            GlobalAugPlayer augPlayer = player.GetModPlayer<GlobalAugPlayer>();
            augPlayer.deathshroudTimer = Main.rand.Next(40, 120);
        }
    }
}
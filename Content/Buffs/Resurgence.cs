using Terraria;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Buffs
{
	public class Resurgence : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
            player.immuneNoBlink = true;
            player.SetImmuneTimeForAllTypes(2);
        }
    }
}
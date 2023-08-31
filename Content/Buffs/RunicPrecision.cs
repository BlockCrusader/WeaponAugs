using Terraria;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Buffs
{
	public class RunicPrecision : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetCritChance(DamageClass.Generic) += 7.5f;
		}
    }
}
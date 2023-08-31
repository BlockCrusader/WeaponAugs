using Terraria;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Buffs
{
	public class RunicRush : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetAttackSpeed(DamageClass.Generic) += 0.075f;
		}
    }
}
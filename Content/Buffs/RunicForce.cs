using Terraria;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Buffs
{
	public class RunicForce : ModBuff
	{
        public override void Update(Player player, ref int buffIndex)
        {
			player.GetKnockback(DamageClass.Generic) += 0.15f;
		}
    }
}
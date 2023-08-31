using Terraria;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Buffs
{
	public class Unleash : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
		}
    }
}
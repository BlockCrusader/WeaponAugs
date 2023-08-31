using Terraria;
using Terraria.ModLoader;

namespace WeaponAugs.Content.Buffs
{
	public class Momentum : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
		}
        // Most buff effects in this mod are handled by GlobalPlayer and/or GlobalNPC instances. Hence why it appears to do nothing
    }
}